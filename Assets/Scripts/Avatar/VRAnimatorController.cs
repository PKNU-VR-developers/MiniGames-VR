using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
    public float speedTreshold = 0.1f;
    [Range(0,1)]public float smoothing = 1; 
    private Animator animator;
    private Vector3 previousPos;
    private float previousRot;
    private VRRig vrRig;

    void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = GetComponent<VRRig>();
        previousPos = vrRig.head.vrTarget.position;
        previousRot = transform.InverseTransformDirection(vrRig.head.vrTarget.rotation.eulerAngles).y;
    }

    void Update()
    {
        //Compute the speed
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;
        //Local Speed
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = vrRig.head.vrTarget.position;

        //Local Rotation을 받아와야 애니메이션이 정상 작동함.
        float headsetRotationSpeed = (transform.InverseTransformDirection(vrRig.head.vrTarget.rotation.eulerAngles).y - previousRot) / Time.deltaTime;
        previousRot = transform.InverseTransformDirection(vrRig.head.vrTarget.rotation.eulerAngles).y;

        //Set Animator Values
        float previousDirectionX = animator.GetFloat("DirectionX");
        float previousDirectionY = animator.GetFloat("DirectionY");

        animator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedTreshold);
        animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
        animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));

        if(headsetLocalSpeed == Vector3.zero) //캐릭터가 이동 없이 방향만 회전할 때
        {
            animator.SetBool("isMoving", Mathf.Abs(headsetRotationSpeed) > speedTreshold);
            animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetRotationSpeed, -1, 1), smoothing));
        }
    }
}
