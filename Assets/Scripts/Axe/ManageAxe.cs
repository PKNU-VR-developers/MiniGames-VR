using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAxe : MonoBehaviour
{
    public GameObject prefabAxe;
    private GameObject axe;

    public void Create()
    {
        axe = Instantiate(prefabAxe, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void Destroy()
    {
        Destroy(axe);
    }


}
