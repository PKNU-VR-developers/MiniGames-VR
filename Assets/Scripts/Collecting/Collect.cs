using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보석 아이템 등의 수집을 위한 스크립트
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
                // 플레이어가 해당 Script를 컴포넌트로 가지는 Object와 접촉하면
                // CollectionStatus Script에 있는 현재 아이템 수집 개수를 1 증가시키고
                // 해당 Object를 파괴함.
                status.AddCount();
                Destroy(gameObject);
            }
        }
    }
}
