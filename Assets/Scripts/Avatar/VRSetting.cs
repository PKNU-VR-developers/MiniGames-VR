using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSetting : MonoBehaviour
{
    public Transform cameraOffset;
    public Transform Head;
    public Transform rightHandOffset;
    public Transform rightHand;
    public Transform leftHandOffset;
    public Transform leftHand;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        cameraOffset.position = Head.position;
        rightHandOffset.position = rightHand.position;
        leftHandOffset.position = leftHand.position;
    }
}
