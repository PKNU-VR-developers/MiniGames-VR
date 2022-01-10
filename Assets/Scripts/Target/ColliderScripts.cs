using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScripts : MonoBehaviour
{
    [SerializeField]GameObject bulletImpact;
    private GameObject bulletImpactCopy;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Bullet"))
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(bulletImpact, pos, rot).transform.parent = transform;
            //transform.Rotate(-90, 0, 0, Space.World);
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
    }
    private void OnTriggerExit(Collider other)

    {
    }

}
