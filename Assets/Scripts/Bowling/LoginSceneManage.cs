using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneManage : MonoBehaviour{
    public void SceneChange()
    {
        SceneManager.LoadScene("VR Bowling");
    }
}
