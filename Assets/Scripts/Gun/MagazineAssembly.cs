using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineAssembly: MonoBehaviour 
{
    public GameObject magazine;
  
    private void OnTriggerEnter(Collider other)
    {
        //Magazine�̶�� �±׸� ���� ��ü�� �ε����� GameObject magazine�� transform�� �� ��ũ��Ʈ�� �ִ� ������Ʈ�� �ٿ���
        if (other.CompareTag("Magazine"))
        {
            magazine.transform.position = transform.position;
            magazine.transform.rotation = transform.rotation;
            magazine.transform.parent = transform;
            Debug.Log("working");
        }
    }
}
