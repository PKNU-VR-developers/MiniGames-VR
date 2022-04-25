//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TerrainTrigger : MonoBehaviour
//{
//    //// axe 두개 중 1개라도 Terrain에 닿아있다면 isTriggered를 true로 설정해주기 위해 리스트로 axe를 받아옴.
//    //private List<Transform> axes = new List<Transform>();

//    private void OnTriggerEnter(Collider other)
//    {
//        // 해당 Script를 컴포넌트로 가지는 GameObject와 Trigger 충돌한 물체의 Tag가 Axe인지 확인함
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

//        //    // 현재 GameObject에 닿아있는 Axe가 하나도 없다면 isTriggered를 false로 변경함.
//        //    if (axes.Count == 0)
//        //    {
//        //        XRClimber.isTriggered = false;
//        //    }
//        //}
//    }
//}
