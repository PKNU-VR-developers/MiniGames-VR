using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    public Transform avatar;

    // Update is called once per frame
    void Update()
    {
        //�θ� ���� �� ������ ���
        /*
        Transform parent = transform.parent;
        transform.parent = null; 
        transform.localScale = new Vector3(1f, 2f, 3f); 
        transform.parent = parent;
        */

        //�θ� ���� �� ������ ���
        transform.localScale = avatar.localScale;

    }
}
