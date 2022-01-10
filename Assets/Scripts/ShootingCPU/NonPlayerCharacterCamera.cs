using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacterCamera : MonoBehaviour
{

    public Camera firstPersonCamera;
    public Camera overheadCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShowOverheadView();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowFirstPersonView();
        }
    }

    public void ShowOverheadView()
    {
        firstPersonCamera.enabled = false;
        overheadCamera.enabled = true;
    }

    public void ShowFirstPersonView()
    {
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
    }
}
