using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairChanger : MonoBehaviour
{
    public GameObject[] hair;
    private int num = 0;
    private void Update()
    {
       //무언가 입력을 받으면 머리를 순서대로 바꿈
       if(Input.GetKeyDown(KeyCode.A)){
            ChangeHair(num);
                num++;
        }
        if (num == 5)
            num = 0;
    }
    //머리를 바꾸는 로직 구현. 한 번 전부 비활성화 하고, idx 값에 해당하는 머리만 활성화
    void ChangeHair(int idx)
    {
        for (int i = 0; i < hair.Length; i++) {
            hair[i].SetActive(false);
            Debug.Log(hair[i].name);
        }
            hair[idx].SetActive(true);
    }
}
