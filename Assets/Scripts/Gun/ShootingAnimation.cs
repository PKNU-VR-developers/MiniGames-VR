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
            // ���콺 ������ ��ư�� ������

        }
    }
    public void Shoot()
    {
        Debug.Log("Shoot");
        anim.SetBool("Shooting", true);
    }
}
