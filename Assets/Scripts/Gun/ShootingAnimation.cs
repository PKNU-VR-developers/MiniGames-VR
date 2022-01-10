using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAnimation : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
       
    }
    public void Shoot()
    {
        Debug.Log("Shoot");
        anim.SetBool("Shooting", true);
    }
}
