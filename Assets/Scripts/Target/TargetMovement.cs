using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    bool isGoingTowardRight = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isGoingTowardRight)
        MoveTowardRight();
        else
        MoveTowardLeft();

    }
    void OnTriggerEnter(Collider col) {
        if (!col.CompareTag("Bullet"))
            isGoingTowardRight = !isGoingTowardRight;
        Debug.Log("toward against");
    }
    void OnTriggerStay(Collider col) { }
    void OnTriggerExit(Collider col) { }



    public void MoveTowardRight()
    {
        transform.position += Vector3.right * 4 * Time.deltaTime;
    }
    public void MoveTowardLeft()
    {
        transform.position += Vector3.left * 4 * Time.deltaTime;
    }
}
