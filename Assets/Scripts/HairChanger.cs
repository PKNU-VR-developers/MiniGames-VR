using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairChanger : MonoBehaviour
{
    public GameObject[] hair;
    private int num = 0;
    private void Update()
    {
       //���� �Է��� ������ �Ӹ��� ������� �ٲ�
       if(Input.GetKeyDown(KeyCode.A)){
            ChangeHair(num);
                num++;
        }
        if (num == 5)
            num = 0;
    }
    //�Ӹ��� �ٲٴ� ���� ����. �� �� ���� ��Ȱ��ȭ �ϰ�, idx ���� �ش��ϴ� �Ӹ��� Ȱ��ȭ
    void ChangeHair(int idx)
    {
        for (int i = 0; i < hair.Length; i++) {
            hair[i].SetActive(false);
            Debug.Log(hair[i].name);
        }
            hair[idx].SetActive(true);
    }
}
