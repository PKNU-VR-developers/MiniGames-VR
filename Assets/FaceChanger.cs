using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChanger : MonoBehaviour
{
    public Material[] face;
    private int num = 0;
    private new SkinnedMeshRenderer renderer;

    private void Awake()
    {
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeFace(num);
        }
        if (num == 6)
            num = 0;
    }
    void ChangeFace(int idx)
    {
        Debug.Log(idx);
        renderer.material = face[idx];
        num++;
    }
}
