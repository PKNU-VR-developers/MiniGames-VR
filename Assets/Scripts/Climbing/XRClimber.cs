using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRClimber : MonoBehaviour
{
    private CharacterController character;

    // 외부 Script에서 접근이 가능하도록 public static으로 선언함.
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
        // 오른손, 왼손 둘 중 하나라도 Axe를 잡으면 캐릭터의 Avatar를 비활성화 하고 Controller에 할당된 Hand Prefab을 활성화 함.
        if (rightClimbingHand || leftClimbingHand)
        {
            Avatar.SetActive(false);
            HandPresence.canHandModelInstantiate = true;
        }

        // 양 손 모두 Axe를 놓을 경우 Avatar 활성화, Hand Prefab 비활성화.
        else
        {
            Avatar.SetActive(true);
            HandPresence.canHandModelInstantiate = false;
        }
    }

    void FixedUpdate()
    {
        // 오른손, 왼손 둘 중 하나라도 Axe를 잡고
        // Axe가 Terrain(terrainTrigger Script를 컴포넌트로 가지는 GameObject)과 닿았다면 True
        if ((rightClimbingHand || leftClimbingHand) && (isRightAxeTriggered || isLeftAxeTriggered))
        {
            //rightEffectPos = GameObject.Find("")
            // 오른손으로 Axe를 잡은 상태일 때, 오른손 Controller의 Trigger 버튼을 누르고 있으면 True
            if (rightClimbingHand && (rightClimbingHand.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightValue) && rightValue) && isRightAxeTriggered)
            {
                // climbing 상태일 때 조이스틱을 이용한 캐릭터 이동 및 중력 작용을 막음.
                continuousMovement.enabled = false;
                //Haptic(rightClimbingHand);
                Climb(rightClimbingHand);
                //이펙트 및 효과음
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
        // climbingHand로 전달받은 Device의 속도를 velocity 변수로 반환함.
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }

    // Controller의 진동 기능.
    void Haptic(XRController controller)
    {
        controller.SendHapticImpulse(0.5f, 0.5f);
    }

    void PlaySound()
    {
        AudioSource.PlayClipAtPoint(climbingSFX, transform.position);
    }
}
