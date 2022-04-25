using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    public Transform avatar;

    // Update is called once per frame
    void Update()
    {
        //부모가 있을 때 스케일 축소
        /*
        Transform parent = transform.parent;
        transform.parent = null; 
        transform.localScale = new Vector3(1f, 2f, 3f); 
        transform.parent = parent;
        */

        //부모가 없을 때 스케일 축소
        transform.localScale = avatar.localScale;

    }
}
