using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ���� ������ ���� ��ũ��Ʈ
public class Collect : MonoBehaviour
{
    private CollectionStatus status;

    private void Start()
    {
        status = GameObject.Find("Canvas").GetComponent<CollectionStatus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (status != null)
        {
            if (other.CompareTag("Player"))
            {
                // �÷��̾ �ش� Script�� ������Ʈ�� ������ Object�� �����ϸ�
                // CollectionStatus Script�� �ִ� ���� ������ ���� ������ 1 ������Ű��
                // �ش� Object�� �ı���.
                status.AddCount();
                Destroy(gameObject);
            }
        }
    }
}
