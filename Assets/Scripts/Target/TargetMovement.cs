using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    bool isGoingTowardRight = true;

    // 과녁을 좌우로 움직임
    void Update()
    {
        if(isGoingTowardRight)
        MoveTowardRight();
        else
        MoveTowardLeft();

    }
    //충돌하는 것이 총알이 아닌경우 반대 방향으로 움직이게 함
    void OnTriggerEnter(Collider col) {
        if (!col.CompareTag("Bullet"))
            isGoingTowardRight = !isGoingTowardRight;
        Debug.Log("toward against");
    }
    void OnTriggerStay(Collider col) { }
    void OnTriggerExit(Collider col) { }


    //오른쪽으로 이동
    public void MoveTowardRight()
    {
        transform.position += Vector3.right * 4 * Time.deltaTime;
    }

    //왼쪽으로 이동
    public void MoveTowardLeft()
    {
        transform.position += Vector3.left * 4 * Time.deltaTime;
    }
}
