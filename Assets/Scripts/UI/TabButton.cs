using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 현재 활성화 되어있는 패널에 해당되는 탭 버튼의 색을 변경함.
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
