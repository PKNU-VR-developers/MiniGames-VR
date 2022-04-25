using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class RecyclingScrollView : MonoBehaviour
{
	public List<GameObject> orgItemPrefab = new List<GameObject>();
	public float itemWidth = 200.0f;
	public List<int> dataList;

	private ScrollRect _scroll;
	private List<GameObject> itemList;
	private float offset;

	private int itemCount;
	private float listAndCountDiff;
	private int extraOffset;

	private void Awake()
	{
		_scroll = GetComponent<ScrollRect>();
	}

	void Start()
	{
		// dataList�� �̹� �Ҵ�� ������ �ִٸ� ��� �ʱ�ȭ
		dataList.Clear();

		// ContentWidth�� ���� ������.
		for (int i = 0; i < 100; i++)
		{
			dataList.Add(i);
		}
		CreateItem();
		SetContentWidth();
	}

	private void CreateItem() // item ���� �Լ�
	{
		RectTransform scrollRect = _scroll.GetComponent<RectTransform>(); 
		itemList = new List<GameObject>();

		int orgItemPrefabCount = orgItemPrefab.Count;

		/* Content ���� �����ž� �ϴ� item�� ����
		 * ������ ���� ���̴� �� �ܿ� ������ �ʴ� ������ ������ �ΰ� ������.
		 * �ֿ����� item�� ���ݸ� ���̴� �� ��ĥ �� �����Ƿ� +1
		 * ����, �������� ������ �ʴ� ���� ���� �� 1���� +2 */
		itemCount = (int)(scrollRect.rect.width / itemWidth) + 1 + 2;
		
		for (; itemList.Count < itemCount;)
		{
			// Ư�� Item�� ������ �����Ǿ� ���� ����Ǵ� ���� ���� ���� Item List ��ü�� ������.
			for (int i = 0; i < orgItemPrefabCount; i++)
			{
				// Item�� Content�� �ڽ� Object�� ������.
				GameObject item = Instantiate(orgItemPrefab[i], _scroll.content);
				itemList.Add(item);

				// Item�� ��ġ�� x��ǥ�� ��������
				item.transform.localPosition = new Vector3((itemList.Count - 1) * itemWidth, 0);

				// Content�� ���� �ۿ����� Item�� ����� ���� �ʵ��� �ϴ� �Լ�
				CheckRange(item, (itemList.Count - 1));
			}
		}

		// Ư�� ������ ����� Item�� �ٸ� ��ġ�� ����� �ؾ���.
		// �� ��, ������ ItemList�� ������ ItemCount���� ���ٸ� Ư�� ������ �� �� �а� �������ֱ� ���� �ڵ�.
		if (itemList.Count > itemCount)
        {
			listAndCountDiff = (float)(itemList.Count - itemCount) / 2;
			extraOffset = (int)(listAndCountDiff + 0.5);
		}

		// ���� �����Ǿ� �ִ� Item���� �� ����
		offset = itemList.Count * itemWidth;
		
	}

	private void SetContentWidth()
	{
		// �� ����� �� Item�� ������ Item �ϳ��� �������� ���Ͽ� ContentWidth�� ������.
		_scroll.content.sizeDelta = new Vector2(dataList.Count * itemWidth, _scroll.content.sizeDelta.y);
	}

	// Item�� �ٸ� ��ġ�� ����� �ؾ��ϴ� ������ ����� �� �����Ǿ�� �� ��ġ�� �������ִ� �Լ�
    private bool RelocationItem(GameObject item, float contentX, float scrollWidth)
    {
		// ��ũ�Ѻ並 ������ �� Item�� �ƴ϶� Content ��ü�� �����̴� ���̹Ƿ�
		// item�� ������ǥ x�� Content�� x��ǥ�� ���� ��ġ�� �������� Ư�� ������ ī��Ʈ�Ѵ�.
		// item�� ���� �������� Ư�� ���� �̻� �̵����� ��� ������ ��ġ�� ����� ��.
		if (item.transform.localPosition.x + contentX < -itemWidth * (2 + extraOffset))
        {
			item.transform.localPosition += new Vector3(offset, 0);
			return true;
        }

		// item�� ������ �������� Ư�� ���� �̻� �̵����� ��� ���� ��ġ�� ����� ��.
        else if (item.transform.localPosition.x + contentX > scrollWidth + (itemWidth * (1 + extraOffset)))
        {
			item.transform.localPosition -= new Vector3(offset, 0);
			return true;
		}

		return false;
    }

	// Content ���� �ۿ��� item�� ����� ���� �ʵ��� ��.
	private void CheckRange(GameObject item, int idx)
    {
		if (idx < 0 || idx >= dataList.Count) item.gameObject.SetActive(false);
		else item.gameObject.SetActive(true);
    }

    void Update()
    {
        RectTransform scrollRect = _scroll.GetComponent<RectTransform>();
        float scrollWidth = scrollRect.rect.width;
        float contentX = _scroll.content.anchoredPosition.x;
        foreach (GameObject item in itemList)
        {
            bool isChanged = RelocationItem(item, contentX, scrollWidth);

			// CreateItem �Լ����� item�� local X��ǥ�� ���ߴ� ����� �̿��� Item�� Index�� ����.
			int idx = (int)(item.transform.localPosition.x / itemWidth);
			if (isChanged) CheckRange(item, idx);
        }
    }

}