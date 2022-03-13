using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAnimation : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
       
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            // 마우스 오른쪽 버튼을 누르면

        }
    }
    public void Shoot()
    {
        Debug.Log("Shoot");
        anim.SetBool("Shooting", true);
    }
}
