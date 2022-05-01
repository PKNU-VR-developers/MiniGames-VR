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
                // �� �÷��̾�� �����͸� others���� �����ϴ�.
                //stream.SendNext(IsFiring);
                
            }
            else
            {
                // ��Ʈ��ũ �÷��̾���� �����͸� �޽��ϴ�. 
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

            // # �߿�
            // ���Ӹ޴��� ���Ͽ��� ����. ������ ����ȭ�� �� �ν��Ͻ�ȭ �Ǵ� ���� �����ϱ� ���� ���� �÷��̾� �ν��Ͻ��� ��� Ʈ��ŷ �� ����
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // �ν��Ͻ��� ���� ����ȭ���� ��Ƴ��� �� �ֵ��� �÷��׷� ǥ���մϴ�. �� ������ ������ �ε�� �� ��Ȱ�� ������ �����մϴ�.
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
            // ���� �ۿ� �ִٸ� ������ ��ġ���� ���� �����Ѵ�.
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