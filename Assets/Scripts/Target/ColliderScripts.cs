using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScripts : MonoBehaviour
{
    [SerializeField]GameObject bulletImpact;
    private GameObject bulletImpactCopy;

    //충돌이 시작하는 순간
    void OnCollisionEnter(Collision collision)
    {
        //충돌한 물체의 태그가 Bullet이면 충돌한 후 총알 오브젝트를 지우고 충돌한 위치에 총알 자국을 복사해줌    
        if (collision.collider.CompareTag("Bullet"))
        {
            ContactPoint contact = collision.contacts[0]; //접점
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);//Vector3.up 에서 contact.normal 방향으로 회전함
            Vector3 pos = contact.point;
            Instantiate(bulletImpact, pos, rot).transform.parent = transform; //총알자국을 생성하고 자식으로 생성하여 과녁과 함께 움직이게 함
            Destroy(collision.gameObject);//총알 삭제
        }
    }
    //충돌하여 닿아있는 순간
    private void OnTriggerStay(Collider other)
    {
    }
    //충돌이 끝나는 순간
    private void OnTriggerExit(Collider other)

    {
    }

}
