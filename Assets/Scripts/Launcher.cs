using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame //���ӽ����̽��� ������ �� �Ʒ��� �ִ� �͵��� �տ� 
                               //Com.MyCompany.MyGame�� �����Ǿ� ����
                               //���ӽ����̽��� �ϳ��� ������Ʈ�� �ϳ��� ������ �� ����
{   /// <summary>
    /// MonoBehaviorPunCallbacks�� MonoBehavior�� ��ɾ���� ����ϴ� ��ſ� ���� ������Ƽ�� virtual methods�� �����Ѵ�.
    /// ���δ� �������̵嵵 �����Ѵ�. 
    /// </summary>
    public class Launcher : MonoBehaviourPunCallbacks //MonoBehaviour�� ����� => ��� ��ũ��Ʈ�� ��ӹ޴� �⺻ Ŭ�����̴�.
                                                      //����ڰ� Unity ������ �۵� ����� �������� ���ϴ��� �ڵ带
                                                      //�ۼ��� �� �ֵ��� �̹� �������(built-in) Behaviour Ŭ����, ��, 
                                                      //����ڰ� ���� ȣ���� �� �ִ� ��ũ��Ʈ ���(scripting instruction)���� ������ �����ϴ� ��. 
    {                                                 //Awake, Update, Reset ���� �ִ�.
        #region Private Serializable Fields 

        /// <summary>
        /// �ִ� �ο����� ��Ÿ����. ���� ���� ����á�ٸ� ���ο� �÷��̾ ���� �� ���� ���ο� ���� �����.
        /// </summary>
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        #endregion


        #region Private Fields
        private bool isConnecting;


        /// <summary>
        /// �� Ŭ���̾�Ʈ�� ���� ������ ��Ÿ����. �̷� ���� Ŭ���̾�Ʈ���� �и� �� �� �ִ�. 
        /// </summary>
        private string gameVersion = "1";


        #endregion

        #region Public Fields

        [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;

        #endregion

        #region MonoBehaviour CallBacks



        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #Critical
            // �츮�� ������ Ŭ���̾�Ʈ���� PhotnNetwork.LoadLeve()�� ����� �� �ְ����ְ� 
            // ���� �濡 �����ϴ� ��� �ٸ� Ŭ���̾�Ʈ�� ���� ������ ������ �ڵ������� ����ȭ �����ش�. 
            PhotonNetwork.AutomaticallySyncScene = true;
        }



        #endregion
        
        #region MonoBehaviourPunCallbacks Callbacks



        public override void OnConnectedToMaster()
        {
           
            // �濡 ������ ������ ������ �ƹ��͵� ���� �ʴ´�.
            // isConnecting�� fasle�� ���� ������ �����ų� �Ҿ���� ���̴�. �� ������ �ε�Ǹ� OnConnectedToMaster�� �ε�ǰ�
            // �� ��� �츮�� �ƹ��͵� ���� �ʱ⸦ ���Ѵ�.
            if (isConnecting)
            {
                // #Critical: ó���� ���� �����Ѵٸ� �����Ѵ�. ���ٸ� OnJoinRandomFailed()�� ȣ���Ѵ�.
                PhotonNetwork.JoinRandomRoom();
            }

        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            if (this != null)
            {
                progressLabel.SetActive(false);
                controlPanel.SetActive(true);
                Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: ���� ������ ���ο� ���� �ϳ� �����Ѵ�. null ���� �� �̸��̴�. 
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

            // #Critical:ù��° �÷��̾��϶��� �θ��� �׷��� ������ �ν��Ͻ� ���� ��ũ�ϱ� ���ؼ� PhtonNetwork.AutomaticallySyncScene�� �����մϴ�.
            //GetScene() == shooting -> ���� �������� �� ���� �����̸�
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("We load the VR Shooting");


                // #Critical
                // Load the Room Level.
                PhotonNetwork.LoadLevel("VR Shooting");
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
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            // ���� ���� ����Ǿ��ٸ� Join�ϰ� �׷��� �ʴٸ� ������ �����ϴ� ���� �����Ѵ�.
            if (PhotonNetwork.IsConnected)
            {
                // #Critical �������� �濡 �����ϴ� ���� �õ��Ѵ�. ���� �����Ѵٸ� OnJoinRandomFailed�� �˸��� �׸��� ���ο�� �����.
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // #Critical, �켱 Photon Online Server�� �����ؾ� �մϴ�.
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }


        #endregion


    }
}