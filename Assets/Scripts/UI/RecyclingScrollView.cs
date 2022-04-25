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
		// dataList에 이미 할당된 내용이 있다면 모두 초기화
		dataList.Clear();

		// ContentWidth의 값을 결정함.
		for (int i = 0; i < 100; i++)
		{
			dataList.Add(i);
		}
		CreateItem();
		SetContentWidth();
	}

	private void CreateItem() // item 생성 함수
	{
		RectTransform scrollRect = _scroll.GetComponent<RectTransform>(); 
		itemList = new List<GameObject>();

		int orgItemPrefabCount = orgItemPrefab.Count;

		/* Content 내에 생성돼야 하는 item의 개수
		 * 실제로 눈에 보이는 것 외에 보이지 않는 곳에도 여분을 두고 생성함.
		 * 최우측의 item이 절반만 보이는 등 겹칠 수 있으므로 +1
		 * 좌측, 우측으로 보이지 않는 곳에 여분 각 1개씩 +2 */
		itemCount = (int)(scrollRect.rect.width / itemWidth) + 1 + 2;
		
		for (; itemList.Count < itemCount;)
		{
			// 특정 Item만 여러번 생성되어 자주 노출되는 것을 막기 위해 Item List 전체를 생성함.
			for (int i = 0; i < orgItemPrefabCount; i++)
			{
				// Item을 Content의 자식 Object로 생성함.
				GameObject item = Instantiate(orgItemPrefab[i], _scroll.content);
				itemList.Add(item);

				// Item이 배치될 x좌표를 설정해줌
				item.transform.localPosition = new Vector3((itemList.Count - 1) * itemWidth, 0);

				// Content의 범위 밖에서는 Item이 재생성 되지 않도록 하는 함수
				CheckRange(item, (itemList.Count - 1));
			}
		}

		// 특정 범위를 벗어나면 Item을 다른 위치에 재생성 해야함.
		// 이 때, 생성된 ItemList의 개수가 ItemCount보다 많다면 특정 범위를 좀 더 넓게 설정해주기 위한 코드.
		if (itemList.Count > itemCount)
        {
			listAndCountDiff = (float)(itemList.Count - itemCount) / 2;
			extraOffset = (int)(listAndCountDiff + 0.5);
		}

		// 현재 생성되어 있는 Item들의 총 길이
		offset = itemList.Count * itemWidth;
		
	}

	private void SetContentWidth()
	{
		// 총 재생성 될 Item의 개수와 Item 하나의 가로폭을 곱하여 ContentWidth를 결정함.
		_scroll.content.sizeDelta = new Vector2(dataList.Count * itemWidth, _scroll.content.sizeDelta.y);
	}

	// Item을 다른 위치에 재생성 해야하는 범위와 재생성 시 생성되어야 할 위치를 지정해주는 함수
    private bool RelocationItem(GameObject item, float contentX, float scrollWidth)
    {
		// 스크롤뷰를 움직일 때 Item이 아니라 Content 자체가 움직이는 것이므로
		// item의 로컬좌표 x와 Content의 x좌표를 더한 위치를 기준으로 특정 범위를 카운트한다.
		// item이 왼쪽 방향으로 특정 범위 이상 이동했을 경우 오른쪽 위치에 재생성 함.
		if (item.transform.localPosition.x + contentX < -itemWidth * (2 + extraOffset))
        {
			item.transform.localPosition += new Vector3(offset, 0);
			return true;
        }

		// item이 오른쪽 방향으로 특정 범위 이상 이동했을 경우 왼쪽 위치에 재생성 함.
        else if (item.transform.localPosition.x + contentX > scrollWidth + (itemWidth * (1 + extraOffset)))
        {
			item.transform.localPosition -= new Vector3(offset, 0);
			return true;
		}

		return false;
    }

	// Content 범위 밖에서 item이 재생성 되지 않도록 함.
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

			// CreateItem 함수에서 item의 local X좌표를 구했던 방식을 이용해 Item의 Index를 구함.
			int idx = (int)(item.transform.localPosition.x / itemWidth);
			if (isChanged) CheckRange(item, idx);
        }
    }

}