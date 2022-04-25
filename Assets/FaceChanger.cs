using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChanger : MonoBehaviour
{
    public Material[] face;
    private int num = 0;
    private new SkinnedMeshRenderer renderer;

    //SkinnedMeshRenderer�� �ڽ����� �ٿ���
    private void Awake()
    {
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    private void Update()
    {   //�Է°��� ������ �� ǥ�� ����
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeFace(num);
        }
        //�ִ� �ε��� ���� �Ǹ� �ʱ�ȭ
        if (num == 6)
            num = 0;
    }

    //�� ǥ�� ���� ����
    void ChangeFace(int idx)
    {   //ǥ���� ���� material�� �����Ŵ
        renderer.material = face[idx];
        num++;
    }
}
