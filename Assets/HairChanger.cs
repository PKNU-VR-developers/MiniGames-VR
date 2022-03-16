using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairChanger : MonoBehaviour
{
    public GameObject[] hair;
    private int num = 0;
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.A)){
            ChangeHair(num);
                num++;
        }
        if (num == 5)
            num = 0;
    }
    void ChangeHair(int idx)
    {
        for (int i = 0; i < hair.Length; i++) {
            hair[i].SetActive(false);
            Debug.Log(hair[i].name);
        }
            hair[idx].SetActive(true);
    }
}
