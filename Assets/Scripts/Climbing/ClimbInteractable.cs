using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRGrabInteractable
{
    // �ش� Script�� ������Ʈ�� ������ GameObject�� Controller�� Grip ��ư�� �̿��� ������ ȣ��Ǵ� �Լ�
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactor;
        base.OnSelectEntered(args);

        // Grip ��ư�� �̿��� interactor�� XRDirectInteractor���� Ȯ��
        if (interactor is XRDirectInteractor)
        {
            // ���� interactor�� �̸��� Ȯ���Ͽ� ������, �޼��� ���� �Ҵ���.
            if (interactor.name.Equals("Right Hand"))
            {
                XRClimber.rightClimbingHand = interactor.GetComponent<XRController>();
                gameObject.tag = "Right Axe";
            }
            else
            {
                XRClimber.leftClimbingHand = interactor.GetComponent<XRController>();
                gameObject.tag = "Left Axe";
            }
        }
            

    }

    // Grip ��ư�� ���� �� ȣ��Ǵ� �Լ�
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        XRBaseInteractor interactor = args.interactor;
        base.OnSelectExited(args);

        if (XRClimber.rightClimbingHand && XRClimber.rightClimbingHand.name == interactor.name)
        {
            XRClimber.rightClimbingHand = null;
            if(gameObject.CompareTag("Right Axe"))
            {
                gameObject.tag = "Axe";
            }
        }
            

        if (XRClimber.leftClimbingHand && XRClimber.leftClimbingHand.name == interactor.name)
        {
            XRClimber.leftClimbingHand = null;
            if (gameObject.CompareTag("Left Axe"))
            {
                gameObject.tag = "Axe";
            }
        }
    }
}
