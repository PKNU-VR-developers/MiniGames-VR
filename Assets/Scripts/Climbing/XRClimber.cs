using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRClimber : MonoBehaviour
{
    private CharacterController character;
    //public static XRController rightClimbingHand;
    //public static XRController leftClimbingHand;
    //public static List<XRController> climbingHand;
    public static XRController climbingHand;
    public static bool isTriggered;
    private GameObject axes;
    private ContinuousMovement continuousMovement;

    void Start()
    {
        character = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ContinuousMovement>();
    }

    void FixedUpdate()
    {
        if (isTriggered)
        {
            if (climbingHand)
            {
                if (climbingHand.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool value) && value)
                {
                    continuousMovement.enabled = false;
                    Climb();
                    Debug.Log(climbingHand);
                }
            }
        }
        else
        {
            continuousMovement.enabled = true;
        }
    }

    void Climb()
    {
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }
}
