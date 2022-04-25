using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScripts : MonoBehaviour
{
    [SerializeField]GameObject bulletImpact;
    private GameObject bulletImpactCopy;

    //�浹�� �����ϴ� ����
    void OnCollisionEnter(Collision collision)
    {
        //�浹�� ��ü�� �±װ� Bullet�̸� �浹�� �� �Ѿ� ������Ʈ�� ����� �浹�� ��ġ�� �Ѿ� �ڱ��� ��������    
        if (collision.collider.CompareTag("Bullet"))
        {
            ContactPoint contact = collision.contacts[0]; //����
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);//Vector3.up ���� contact.normal �������� ȸ����
            Vector3 pos = contact.point;
            Instantiate(bulletImpact, pos, rot).transform.parent = transform; //�Ѿ��ڱ��� �����ϰ� �ڽ����� �����Ͽ� ����� �Բ� �����̰� ��
            Destroy(collision.gameObject);//�Ѿ� ����
        }
    }
    //�浹�Ͽ� ����ִ� ����
    private void OnTriggerStay(Collider other)
    {
    }
    //�浹�� ������ ����
    private void OnTriggerExit(Collider other)

    {
    }

}
