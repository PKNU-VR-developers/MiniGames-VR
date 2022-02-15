using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                status.AddCount();
                Destroy(gameObject);
            }
        }
    }
}
