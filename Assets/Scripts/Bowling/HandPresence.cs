using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public static bool canHandModelInstantiate = false;
    private bool isHandModelActivated = false;

    private InputDevice targetDevice;

    public GameObject handModelPrefabs;
    private GameObject spawnedHandModel;

    private Animator handAnimator;

    void TryInitialize()
    {
        // 입력장치들 중 controllerCharacteristics에 해당되는 입력장치를 찾아 devices 리스트에 추가함.
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log("item name : " + item.name + ", item characteristics : " + item.characteristics);
        }
        if (devices.Count > 0)
        {
            targetDevice = devices[0];

            spawnedHandModel = Instantiate(handModelPrefabs, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();

            isHandModelActivated = true;
        }
    }

    public void DestroyHandModel()
    {
        Destroy(spawnedHandModel);
        isHandModelActivated = false;
    }

    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }

    }

    void Update()
    {
        if (canHandModelInstantiate && !isHandModelActivated)
            TryInitialize();

        if (!canHandModelInstantiate && isHandModelActivated)
            DestroyHandModel();
        
        if (isHandModelActivated)
            UpdateHandAnimation();
    }
}
