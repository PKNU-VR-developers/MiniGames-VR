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
        // 오른손 Controller의 PrimaryButton을 누르고 있는 동안의 시간 측정
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
                // 버튼을 놓는 순간
                if (!isPressed && wasPressed)
                {
                    // UI가 비활성화 상태라면 UI를 활성화, 그 반대라면 UI를 비활성화 함.
                    // UI와 함께 양 손의 Raycast 또한 함께 활성화 or 비활성화 함.
                    if (settingTabPanel.activeSelf == false)
                    {
                        // UI를 VR Camera가 바라보는 방향에 생성되도록 하기 위한 코드
                        settingTabPanel.transform.position = new Vector3(target.position.x, target.position.y, target.position.z);

                        // UI가 VR Camera를 바라보도록 함.
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
