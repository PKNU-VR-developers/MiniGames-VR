using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAxe : MonoBehaviour
{
    public GameObject prefabAxe;
    private GameObject axe;

    public void Create()
    {
        // 해당 Script가 붙어있는 GameObject의 Position, Rotation 값을 그대로 받아와서 axe를 생성함.
        axe = Instantiate(prefabAxe, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void Destroy()
    {
        Destroy(axe);
    }
}
