using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 탭 버튼을 누름에 따라 해당된 탭 패널이 활성화 되도록 하는 Script
public class TabPanel : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public List<GameObject> contentsPanels;
    public int selected = 0;
    private void Start()
    {
        // UI를 처음 시작할 때 selected에 할당된 패널을 활성화 함.
        ClickTab(selected);
    }

    public void ClickTab(int id)
    {
        for(int i = 0; i<contentsPanels.Count; i++)
        {
            if(i == id)
            {
                contentsPanels[i].SetActive(true);
                tabButtons[i].Selected();
            }
            else
            {
                contentsPanels[i].SetActive(false);
                tabButtons[i].DeSelected();
            }
        }
    }
}
