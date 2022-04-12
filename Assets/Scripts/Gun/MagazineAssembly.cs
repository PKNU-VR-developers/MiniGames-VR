using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineAssembly: MonoBehaviour 
{
    public GameObject magazine;
  
    private void OnTriggerEnter(Collider other)
    {
        //Magazine이라는 태그를 가진 물체랑 부딪히면 GameObject magazine의 transform을 이 스크립트가 있는 오브젝트에 붙여줌
        if (other.CompareTag("Magazine"))
        {
            magazine.transform.position = transform.position;
            magazine.transform.rotation = transform.rotation;
            magazine.transform.parent = transform;
            Debug.Log("working");
        }
    }
}
