using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using System;
using UnityEngine.UI;
#if(UNITY_2018_3_OR_NEWER)
using UnityEngine.Android;
#endif
using Photon.Pun;

public class VoiceChatManager : MonoBehaviourPunCallbacks
{
    private readonly string appID = "2f7b4d8d3d824f018bb3ede7f3ad73f7"; //아고라 사이트에서 만든 App ID

    public static VoiceChatManager instance;

    public GameObject micVol;
    public GameObject otherUserVol;
    private bool isMuted = true;

    IRtcEngine rtcEngine;
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    void Start()
    {

#if (UNITY_2018_3_OR_NEWER)
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {

        }
        else
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
        rtcEngine = IRtcEngine.GetEngine(appID);
        rtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess;
        rtcEngine.OnLeaveChannel += OnLeaveChannelSuccess;
        rtcEngine.OnError += OnError;
        rtcEngine.OnUserMutedAudio += (uint uid, bool muted) =>
        {
            string userMutedMessage = string.Format("onUserMuted callback uid {0} {1}", uid, muted);
            Debug.Log(userMutedMessage);
        };
    }

    public void MicVolumeManager(float vol)
    {
        int volume = (int)vol;
        Debug.Log(volume);
        if ((volume >= 0) && (volume <= 100))
        {
            //마이크 볼륨 조절
            rtcEngine.AdjustRecordingSignalVolume(volume);
            if (volume == 0)
            {
                rtcEngine.AdjustAudioMixingVolume(volume);
            }
        }
    }

    public void OtherUsersVolumeManager(float vol)
    {
        int volume = (int)vol;
        if ((volume >= 0) && (volume <= 100))
        {
            // 다른 사람 목소리 조절
            rtcEngine.AdjustPlaybackSignalVolume(volume);
            if (volume == 0)
            {
                rtcEngine.AdjustAudioMixingVolume(volume);
            }
        }
    }

    public void MuteBtnOnclick()
    {
        isMuted = !isMuted;
        rtcEngine.EnableLocalAudio(isMuted);
        Debug.Log("muted");
    }

    void OnError(int error, string msg)
    {
        Debug.LogError("Error: " + error + "/" + msg);
    }

    void OnLeaveChannelSuccess(RtcStats stats)
    {
        Debug.Log("Left channel with stats: " + stats);
    }

    void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("Joined channel " + channelName);
    }

    public override void OnJoinedRoom()
    {
        rtcEngine.JoinChannel(PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnLeftRoom()
    {
        rtcEngine.LeaveChannel();
    }
    void OnDestroy()
    {
        IRtcEngine.Destroy();
    }
    public void ApplicationQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //play모드를 false로.
#elif UNITY_WEBPLAYER
                Application.OpenURL("http://google.com"); //구글웹으로 전환
#else
                Application.Quit(); //어플리케이션 종료
#endif
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();//방떠나기 포톤 네트워크 기능
    }
}
