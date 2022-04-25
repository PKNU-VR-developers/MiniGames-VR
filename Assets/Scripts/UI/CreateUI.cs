using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CreateUI : MonoBehaviour
{
    private XRController rightController;
    private float currentTime = 0;
    private bool isPressed = false;
    private bool wasPressed = false;

    public Transform vrCamera;
    public Transform target;
    public GameObject settingTabPanel;
    public GameObject settingLoadingUI;
    public GameObject rightRay;
    public GameObject leftRay;
    public float pressTime = 1f;
    public float offset = 3f;

    void Start()
    {
        rightController = GetComponent<XRController>();
    }

    // Update is called once per frame
    void Update()
    {
        // ������ Controller�� PrimaryButton�� ������ �ִ� ������ �ð� ����
        if (rightController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed) && isPressed)
        {
            currentTime += Time.deltaTime;
            settingLoadingUI.transform.position = target.transform.position;
            settingLoadingUI.transform.LookAt(vrCamera);
            settingLoadingUI.SetActive(true);
            Debug.Log(currentTime);
        }

        else
        {

            if (settingLoadingUI.activeSelf == true)
            {
                settingLoadingUI.SetActive(false);
            }

            if (currentTime >= pressTime)
            {
                // ��ư�� ���� ����
                if (!isPressed && wasPressed)
                {
                    // UI�� ��Ȱ��ȭ ���¶�� UI�� Ȱ��ȭ, �� �ݴ��� UI�� ��Ȱ��ȭ ��.
                    // UI�� �Բ� �� ���� Raycast ���� �Բ� Ȱ��ȭ or ��Ȱ��ȭ ��.
                    if (settingTabPanel.activeSelf == false)
                    {
                        // UI�� VR Camera�� �ٶ󺸴� ���⿡ �����ǵ��� �ϱ� ���� �ڵ�
                        settingTabPanel.transform.position = new Vector3(target.position.x, target.position.y, target.position.z);

                        // UI�� VR Camera�� �ٶ󺸵��� ��.
                        settingTabPanel.transform.LookAt(vrCamera);
                        settingTabPanel.SetActive(true);
                        rightRay.SetActive(true);
                        leftRay.SetActive(true);
                    }
                    else
                    {
                        settingTabPanel.SetActive(false);
                        rightRay.SetActive(false);
                        leftRay.SetActive(false);
                    }
                }
            }
            currentTime = 0;
        }

        wasPressed = isPressed;
    }
}
