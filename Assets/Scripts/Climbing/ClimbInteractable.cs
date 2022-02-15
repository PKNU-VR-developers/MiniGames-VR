using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactor;
        base.OnSelectEntered(args);

        if (interactor is XRDirectInteractor)
        {
            if (interactor.name.Equals("Right Hand"))
            {
                XRClimber.rightClimbingHand = interactor.GetComponent<XRController>();
            }
            else
            {
                XRClimber.leftClimbingHand = interactor.GetComponent<XRController>();
            }
        }
            

    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        XRBaseInteractor interactor = args.interactor;
        base.OnSelectExited(args);

        if (XRClimber.rightClimbingHand && XRClimber.rightClimbingHand.name == interactor.name)
            XRClimber.rightClimbingHand = null;

        if (XRClimber.leftClimbingHand && XRClimber.leftClimbingHand.name == interactor.name)
            XRClimber.leftClimbingHand = null;
    }
}
