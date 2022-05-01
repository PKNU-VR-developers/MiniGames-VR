using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChanger : MonoBehaviour
{
    public Material[] face;
    private int num = 0;
    private new SkinnedMeshRenderer renderer;

    //SkinnedMeshRenderer를 자식으로 붙여줌
    private void Awake()
    {
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    private void Update()
    {   //입력값을 받으면 얼굴 표정 변경
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeFace(num);
        }
        //최대 인덱스 값이 되면 초기화
        if (num == 6)
            num = 0;
    }

    //얼굴 표정 변경 구현
    void ChangeFace(int idx)
    {   //표정이 들어가는 material을 변경시킴
        renderer.material = face[idx];
        num++;
    }
}
