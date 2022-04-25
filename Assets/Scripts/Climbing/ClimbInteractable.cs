using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRGrabInteractable
{
    // 해당 Script를 컴포넌트로 가지는 GameObject를 Controller의 Grip 버튼을 이용해 잡으면 호출되는 함수
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactor;
        base.OnSelectEntered(args);

        // Grip 버튼을 이용한 interactor가 XRDirectInteractor인지 확인
        if (interactor is XRDirectInteractor)
        {
            // 잡은 interactor의 이름을 확인하여 오른손, 왼손을 각각 할당함.
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

    // Grip 버튼을 놓을 때 호출되는 함수
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
