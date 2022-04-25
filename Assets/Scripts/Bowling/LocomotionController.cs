using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Ư�� ��ư�� ������ �� Teleport�� ���Ǵ� Raycast�� Ȱ��ȭ �ǵ��� �ϴ� Script.
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
        // ����ڰ� ������ Controller�� teleportActivationButton�� �����ٸ� isActivated ������ true�� ��ȯ��.
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated);
        return isActivated;
    }
}
