using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionStatus : MonoBehaviour
{
    private int collectCount = 0;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = this.transform.Find("CollectCounter").GetComponent<TextMeshProUGUI>();
    }

    // 현재 플레이어가 수집한 아이템의 개수 카운트 및 개수 출력
    public void AddCount()
    {
        collectCount++;
        text.text = string.Format("{0}/MAX", collectCount);
    }

}
