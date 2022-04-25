using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ư�� ������ ���� �ش�� �� �г��� Ȱ��ȭ �ǵ��� �ϴ� Script
public class TabPanel : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public List<GameObject> contentsPanels;
    public int selected = 0;
    private void Start()
    {
        // UI�� ó�� ������ �� selected�� �Ҵ�� �г��� Ȱ��ȭ ��.
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
