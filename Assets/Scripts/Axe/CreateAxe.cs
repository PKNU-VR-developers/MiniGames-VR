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
    private float timer = 0;
    private CreateUI createUI;

    void Start()
    {
        manageAxe_1 = GameObject.Find("AxeManager_1").GetComponent<ManageAxe>();
        manageAxe_2 = GameObject.Find("AxeManager_2").GetComponent<ManageAxe>();
        xrController = GetComponent<XRController>();
        createUI = GetComponent<CreateUI>();
    }

    void Update()
    {
        // Controller�� PrimaryButton�� �� �� ���� ������ �־����� ����
        if (xrController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed) && isPressed)
        {
            timer += Time.deltaTime;
        }

        else
        {
            if (timer < createUI.pressTime)
            {
                // Controller�� PrimaryButton�� ������ �ִٰ� ���� ����
                if (!isPressed && wasPressed)
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
            }
            timer = 0;
        }
        wasPressed = isPressed;
    }
}
