using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// 특정 버튼을 눌렀을 때 Teleport에 사용되는 Raycast가 활성화 되도록 하는 Script.
public class LocomotionController : MonoBehaviour
{
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;

    void Update()
    {
        if (rightTeleportRay)
        {
            rightTeleportRay.gameObject.SetActive(CheckIfActivated(rightTeleportRay));
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        // 사용자가 설정한 Controller의 teleportActivationButton을 눌렀다면 isActivated 변수에 true를 반환함.
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated);
        return isActivated;
    }
}
