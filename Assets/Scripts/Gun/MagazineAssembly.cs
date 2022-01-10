using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineAssembly: MonoBehaviour 
{
    public GameObject magazine;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magazine"))
        {
            magazine.transform.position = transform.position;
            magazine.transform.rotation = transform.rotation;
            magazine.transform.parent = transform;
            Debug.Log("working");
        }
    }
}
