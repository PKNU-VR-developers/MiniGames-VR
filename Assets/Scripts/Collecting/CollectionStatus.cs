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

    public void AddCount()
    {
        collectCount++;
        text.text = string.Format("{0}/MAX", collectCount);
    }

}
