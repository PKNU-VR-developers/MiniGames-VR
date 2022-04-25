using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� Ȱ��ȭ �Ǿ��ִ� �гο� �ش�Ǵ� �� ��ư�� ���� ������.
public class TabButton : MonoBehaviour
{
    Image background;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    public void Selected()
    {
        background.color = new Color(180 / 255f, 180 / 255f, 180 / 255f);
    }

    public void DeSelected()
    {
        background.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
    }
}
