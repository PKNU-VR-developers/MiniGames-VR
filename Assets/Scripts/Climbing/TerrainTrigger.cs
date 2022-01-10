using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTrigger : MonoBehaviour
{
    private List<Transform> axes = new List<Transform>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Axe"))
        //XRClimber.isTriggered = true;
        {
            if (!XRClimber.isTriggered)
                XRClimber.isTriggered = true;
            axes.Add(other.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //XRClimber.isTriggered = false;
        if (other.CompareTag("Axe"))
        {
            axes.Remove(other.gameObject.transform);
            if (axes.Count == 0)
            {
                XRClimber.isTriggered = false;
                Debug.Log("Empty");
            }
        }
    }
}
