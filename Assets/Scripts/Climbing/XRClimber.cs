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
    private bool rightAxeFX = false;
    private bool leftAxeFX = false;

    public AudioClip climbingSFX;
    private bool rightAxeSFX = false;
    private bool leftAxeSFX = false;


    public GameObject Avatar;
    private ContinuousMovement continuousMovement;

    void Start()
    {
        character = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ContinuousMovement>();
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
                    PlaySoundEffect();
                    rightAxeSFX = true;
                }
                if (!rightAxeFX)
                {
                    Transform effectPos = GameObject.FindGameObjectWithTag("Right Axe").transform.Find("EffectPosition").transform;
                    Instantiate(climbingFX, effectPos.position, effectPos.rotation);
                    rightAxeFX = true;
                }
            }

            else
            {
                rightAxeSFX = false;
                rightAxeFX = false;
            }

            if (leftClimbingHand && (leftClimbingHand.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftValue) && leftValue) && isLeftAxeTriggered)
            {
                continuousMovement.enabled = false;
                //Haptic(leftClimbingHand);
                Climb(leftClimbingHand);

                if (!leftAxeSFX)
                {
                    PlaySoundEffect();
                    leftAxeSFX = true;
                }
                if (!leftAxeFX)
                {
                    Transform effectPos = GameObject.FindGameObjectWithTag("Left Axe").transform.Find("EffectPosition").transform;
                    Instantiate(climbingFX, effectPos.position, effectPos.rotation);
                    leftAxeFX = true;
                }
            }

            else
            {
                leftAxeSFX = false;
                leftAxeFX = false;
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

    void PlaySoundEffect()
    {
        AudioSource.PlayClipAtPoint(climbingSFX, transform.position);
    }
}
