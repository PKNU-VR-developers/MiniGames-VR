using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRClimber : MonoBehaviour
{
    private CharacterController character;

    // �ܺ� Script���� ������ �����ϵ��� public static���� ������.
    public static XRController rightClimbingHand;
    public static XRController leftClimbingHand;
    public static bool isRightAxeTriggered;
    public static bool isLeftAxeTriggered;

    public ParticleSystem climbingFX;

    public AudioClip climbingSFX;
    private bool rightAxeSFX = false;
    private bool leftAxeSFX = false;


    public GameObject Avatar;
    private ContinuousMovement continuousMovement;
    private XRController rightController;
    private XRController leftController;

    void Start()
    {
        character = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ContinuousMovement>();
        rightController = transform.Find("Camera Offset").Find("Right Hand").GetComponent<XRController>();
        leftController = transform.Find("Camera Offset").Find("Left Hand").GetComponent<XRController>();

    }

    private void Update()
    {
        // ������, �޼� �� �� �ϳ��� Axe�� ������ ĳ������ Avatar�� ��Ȱ��ȭ �ϰ� Controller�� �Ҵ�� Hand Prefab�� Ȱ��ȭ ��.
        if (rightClimbingHand || leftClimbingHand)
        {
            Avatar.SetActive(false);
            HandPresence.canHandModelInstantiate = true;
        }

        // �� �� ��� Axe�� ���� ��� Avatar Ȱ��ȭ, Hand Prefab ��Ȱ��ȭ.
        else
        {
            Avatar.SetActive(true);
            HandPresence.canHandModelInstantiate = false;
        }
    }

    void FixedUpdate()
    {
        // ������, �޼� �� �� �ϳ��� Axe�� ���
        // Axe�� Terrain(terrainTrigger Script�� ������Ʈ�� ������ GameObject)�� ��Ҵٸ� True
        if ((rightClimbingHand || leftClimbingHand) && (isRightAxeTriggered || isLeftAxeTriggered))
        {
            //rightEffectPos = GameObject.Find("")
            // ���������� Axe�� ���� ������ ��, ������ Controller�� Trigger ��ư�� ������ ������ True
            if (rightClimbingHand && (rightClimbingHand.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightValue) && rightValue) && isRightAxeTriggered)
            {
                // climbing ������ �� ���̽�ƽ�� �̿��� ĳ���� �̵� �� �߷� �ۿ��� ����.
                continuousMovement.enabled = false;
                //Haptic(rightClimbingHand);
                Climb(rightClimbingHand);
                //����Ʈ �� ȿ����
                if (!rightAxeSFX)
                {
                    PlaySound();
                    rightAxeSFX = true;
                }
            }

            else
            {
                rightAxeSFX = false;
            }

            if (leftClimbingHand && (leftClimbingHand.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftValue) && leftValue) && isLeftAxeTriggered)
            {
                continuousMovement.enabled = false;
                //Haptic(leftClimbingHand);
                Climb(leftClimbingHand);

                if (!leftAxeSFX)
                {
                    PlaySound();
                    leftAxeSFX = true;
                }
            }

            else
            {
                leftAxeSFX = false;
            }
        }
        else
        {
            continuousMovement.enabled = true;
        }
    }

    void Climb(XRController climbingHand)
    {
        // climbingHand�� ���޹��� Device�� �ӵ��� velocity ������ ��ȯ��.
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }

    // Controller�� ���� ���.
    void Haptic(XRController controller)
    {
        controller.SendHapticImpulse(0.5f, 0.5f);
    }

    void PlaySound()
    {
        AudioSource.PlayClipAtPoint(climbingSFX, transform.position);
    }
}
