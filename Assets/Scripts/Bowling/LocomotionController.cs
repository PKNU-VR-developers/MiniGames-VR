using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    //public float activationThreshold = 0.1f;

    //public XRRayInteractor rightInteractorRay;

    // Update is called once per frame
    void Update()
    {
        //Vector3 pos = new Vector3();
        //Vector3 norm = new Vector3();
        //int index = 0;
        //bool validTarget = false;

        if (rightTeleportRay)
        {
            //bool isRightInteractorRayHovering = rightInteractorRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
            rightTeleportRay.gameObject.SetActive(CheckIfActivated(rightTeleportRay));
            
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated);
        return isActivated;
    }
}
