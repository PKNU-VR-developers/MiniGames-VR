using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Gambo //네임스페이스를 가져옴 이 아래에 있는 것들은 앞에 
                               //GamBo가 생략되어 있음
                               //네임스페이스는 하나의 프로젝트에 하나만 존재할 수 있음
{   /// <summary>
    /// MonoBehaviorPunCallbacks는 MonoBehavior의 명령어들을 사용하는 대신에 여러 프로퍼티나 virtual methods를 제공한다.
    /// 때로는 오버라이드도 제공한다. 
    /// </summary>
    public class Launcher : MonoBehaviourPunCallbacks //MonoBehaviour를 사용함 => 모든 스크립트가 상속받는 기본 클래스이다.
                                                      //사용자가 Unity 엔진의 작동 방식을 이해하지 못하더라도 코드를
                                                      //작성할 수 있도록 이미 만들어진(built-in) Behaviour 클래스, 즉, 
                                                      //사용자가 쉽게 호출할 수 있는 스크립트 명령(scripting instruction)들의 집합을 제공하는 것. 
    {                                                 //Awake, Update, Reset 등이 있다.
        #region Private Serializable Fields 

        /// <summary>
        /// 최대 인원수를 나타낸다. 만약 방이 가득찼다면 새로운 플레이어를 받을 수 없고 새로운 방을 만든다.
        /// </summary>
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        #endregion


        #region Private Fields
        private bool isConnecting;


        /// <summary>
        /// 이 클라이언트의 게임 버전을 나타낸다. 이로 인해 클라이언트들을 분리 할 수 있다. 
        /// </summary>
        private string gameVersion = "1";


        #endregion

        #region Public Fields

        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;

        [Tooltip("The Login set")]
        [SerializeField]
        private GameObject LoginSet;
        #endregion

        #region MonoBehaviour CallBacks



        void Start()
        {
            progressLabel.SetActive(false);
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #Critical
            // 우리가 마스터 클라이언트에서 PhotnNetwork.LoadLeve()을 사용할 수 있게해주고 
            // 같은 방에 존재하는 모든 다른 클라이언트의 방을 동일한 레벨로 자동적으로 동기화 시켜준다. 
            PhotonNetwork.AutomaticallySyncScene = true;
        }



        #endregion
        
        #region MonoBehaviourPunCallbacks Callbacks



        public override void OnConnectedToMaster()
        {
           
            // 방에 참가할 생각이 없으면 아무것도 하지 않는다.
            // isConnecting이 fasle인 경우는 게임을 나가거나 잃어버린 것이다. 이 레벨이 로드되면 OnConnectedToMaster가 로드되고
            // 그 경우 우리는 아무것도 하지 않기를 원한다.
            if (isConnecting)
            {
                // #Critical: 처음에 방이 존재한다면 참가한다. 없다면 OnJoinRandomFailed()를 호출한다.
                PhotonNetwork.JoinRandomRoom();
            }

        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            if (this != null)
            {
                progressLabel.SetActive(false);
                LoginSet.SetActive(true);
                Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: 방이 없으니 새로운 방을 하나 생성한다. null 값은 방 이름이다. 
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

            // #Critical:첫번째 플레이어일때만 부르고 그렇지 않으면 인스턴스 신을 싱크하기 위해서 PhtonNetwork.AutomaticallySyncScene을 적용합니다.
            //GetScene() == shooting -> 씬을 가져오고 그 씬이 슈팅이면
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("We load the Lobby");


                // #Critical
                // Load the Room Level.
                PhotonNetwork.LoadLevel("Lobby");
            }

        }
        #endregion

        #region Public Methods


        /// <summary>
        /// Start the connection process.
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect()
        {
            isConnecting = true;
            LoginSet.SetActive(false);
            progressLabel.SetActive(true);
            // 만약 아직 연결되었다면 Join하고 그렇지 않다면 서버에 연결하는 것을 시작한다.
            if (PhotonNetwork.IsConnected)
            {
                // #Critical 랜덤으로 방에 참가하는 것을 시도한다. 만약 실패한다면 OnJoinRandomFailed에 알리고 그리고 새로운걸 만든다.
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // #Critical, 우선 Photon Online Server에 연결해야 합니다.
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }


        #endregion


    }
}