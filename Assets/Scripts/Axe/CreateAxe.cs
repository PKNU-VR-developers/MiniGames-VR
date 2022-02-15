using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CreateAxe : MonoBehaviour
{
    private ManageAxe manageAxe_1;
    private ManageAxe manageAxe_2;
    private XRController xrController;
    private bool isPressed = false;
    private bool wasPressed = false;
    private bool isActived = false;

    void Start()
    {
        manageAxe_1 = GameObject.Find("AxeManager_1").GetComponent<ManageAxe>();
        manageAxe_2 = GameObject.Find("AxeManager_2").GetComponent<ManageAxe>();
        xrController = gameObject.GetComponent<XRController>();
    }

    void Update()
    {
        xrController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed);

        if(isPressed && !wasPressed)
        {
            if (!isActived)
            {
                manageAxe_1.Create();
                manageAxe_2.Create();
                isActived = true;
            }
            else
            {
                manageAxe_1.Destroy();
                manageAxe_2.Destroy();
                isActived = false;
            }
        }

        wasPressed = isPressed;
    }
}
