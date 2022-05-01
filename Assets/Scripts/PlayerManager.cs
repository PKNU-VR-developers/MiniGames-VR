using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.Collections;
using Photon.Pun.Demo.PunBasics;

namespace Gambo
{
    /// <summary>
    /// Player manager.
    /// </summary>
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable 
    {

        #region IPunObservable implementation


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // 이 플레이어는 데이터를 others에게 보냅니다.
                //stream.SendNext(IsFiring);
                
            }
            else
            {
                // 네트워크 플레이어들은 데이터를 받습니다. 
                //this.IsFiring = (bool)stream.ReceiveNext();
                
            }
        }


        #endregion

        #region Private Fields


        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField]
        private GameObject playerUiPrefab;
        private bool leavingRoom = true;
        #endregion



        #region Public Fields
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;


        #endregion

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {

            // # 중요
            // 게임메니저 파일에서 사용됨. 레벨이 동기화될 때 인스턴스화 되는 것을 방지하기 위해 로컬 플레이어 인스턴스를 계속 트레킹 할 것임
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // 인스턴스가 레벨 동기화에서 살아남을 수 있도록 플래그로 표시합니다. 이 과정은 레벨이 로드될 때 원활한 경험을 제공합니다.
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {

          

            #if UNITY_5_4_OR_NEWER
            // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadingMode) =>
            {
                if (this != null)
                this.CalledOnLevelWasLoaded(scene.buildIndex);
            };
            #endif

            if (playerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(playerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            else
            {
                Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
            }
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// </summary>
        void Update()
        {
         

        }
        public override void OnLeftRoom()
        {
            this.leavingRoom = false;
        }
        

        #if !UNITY_5_4_OR_NEWER
        /// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4./// </summary>
        
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
        #endif


        void CalledOnLevelWasLoaded(int level)
        {
            // 만약 밖에 있다면 안전한 위치에서 새로 스폰한다.
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 3f, 0f);
            }
          
                GameObject _uiGo = Instantiate(this.playerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        #endregion
    }
}