//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TerrainTrigger : MonoBehaviour
//{
//    //// axe �ΰ� �� 1���� Terrain�� ����ִٸ� isTriggered�� true�� �������ֱ� ���� ����Ʈ�� axe�� �޾ƿ�.
//    //private List<Transform> axes = new List<Transform>();

//    private void OnTriggerEnter(Collider other)
//    {
//        // �ش� Script�� ������Ʈ�� ������ GameObject�� Trigger �浹�� ��ü�� Tag�� Axe���� Ȯ����
//        if(other.CompareTag("Right Axe"))
//        {
//            if (!XRClimber.isRightAxeTriggered)
//                XRClimber.isRightAxeTriggered = true;
//        }
//        if (other.CompareTag("Left Axe"))
//        {
//            if (!XRClimber.isLeftAxeTriggered)
//                XRClimber.isLeftAxeTriggered = true;
//        }
//        //if (other.CompareTag("Axe"))
//        //{
//        //    if (!XRClimber.isTriggered)
//        //        XRClimber.isTriggered = true;
//        //    axes.Add(other.gameObject.transform);
//        //}
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Right Axe"))
//        {
//            if (XRClimber.isRightAxeTriggered)
//                XRClimber.isRightAxeTriggered = false;
//        }
//        if (other.CompareTag("Left Axe"))
//        {
//            if (XRClimber.isLeftAxeTriggered)
//                XRClimber.isLeftAxeTriggered = false;
//        }
//        //if (other.CompareTag("Axe"))
//        //{
//        //    axes.Remove(other.gameObject.transform);

//        //    // ���� GameObject�� ����ִ� Axe�� �ϳ��� ���ٸ� isTriggered�� false�� ������.
//        //    if (axes.Count == 0)
//        //    {
//        //        XRClimber.isTriggered = false;
//        //    }
//        //}
//    }
//}
