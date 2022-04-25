using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFootIK : MonoBehaviour
{
    private Animator animator;
    public Vector3 footOffset;
    [Range(0, 1)] public float rightFootPosWeight = 1f;
    [Range(0, 1)] public float rightFootRotWeight = 1f;
    [Range(0, 1)] public float leftFootPosWeight = 1f;
    [Range(0, 1)] public float leftFootRotWeight = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot); //유니티에서 자체적으로 설정한 오른발 위치
        RaycastHit hit;

        bool hasHit = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hit);
        if (hasHit)
        {
            /* 애니메이션과 IK 설정간의 가중치.
             * rightFootPosWeight 0으로 하면 애니메이션이 그대로 나타나고 IK 설정이 안 된 것처럼 보임.
             * 1로 하면 IK를 적용시킨 상태로 애니메이션이 동작하는 것 같음.
             * IK 적용의 수치를 조절하는 느낌 */
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight); 
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + footOffset); //실제 게임 플레이 상에서 발의 위치를 정확하게 정해줌

            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, footRotation);
        }
        else
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);

        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

        hasHit = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit);
        if (hasHit)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + footOffset);

            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, footRotation);
        }
        else
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);

    }
}
