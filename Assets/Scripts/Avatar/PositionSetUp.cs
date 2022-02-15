using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSetUp : MonoBehaviour
{
    public Transform sourceObject;
    public Transform targetObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetObject.position = sourceObject.position;
    }
}
