using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRClimber : MonoBehaviour
{
    private CharacterController character;
    public static XRController rightClimbingHand;
    public static XRController leftClimbingHand;
    public static bool isTriggered;
    public GameObject Avatar;
    private ContinuousMovement continuousMovement;

    void Start()
    {
        character = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ContinuousMovement>();
    }

    private void Update()
    {
        if (rightClimbingHand || leftClimbingHand)
        {
            Avatar.SetActive(false);
        }
        else
        {
            Avatar.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if ((rightClimbingHand || leftClimbingHand) && isTriggered)
        {
            if (rightClimbingHand && (rightClimbingHand.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightValue) && rightValue))
            {
                continuousMovement.enabled = false;
                //Haptic(rightClimbingHand);
                Climb(rightClimbingHand);
            }
            if (leftClimbingHand && (leftClimbingHand.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftValue) && leftValue))
            {
                continuousMovement.enabled = false;
                //Haptic(leftClimbingHand);
                Climb(leftClimbingHand);
            }
        }
        else
        {
            continuousMovement.enabled = true;
        }
    }

    void Climb(XRController climbingHand)
    {
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }

    void Haptic(XRController controller)
    {
        controller.SendHapticImpulse(0.5f, 0.5f);
    }
}
