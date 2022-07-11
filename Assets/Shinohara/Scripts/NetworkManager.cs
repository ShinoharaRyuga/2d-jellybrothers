using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>�l�b�g���[�N���Ǘ����� </summary>
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string _player1PrefabName = "Player1";
    [SerializeField] string _player2PrefabName = "Player2";
    [SerializeField, Header("�Q�[���J�n���̃X�^�[�g�ʒu"), Tooltip("�Y���� 0=Player1 1=Player2")] Transform[] _startSpwanPoint = new Transform[2];
    PlayerController _playerController = default;
    public PlayerController PlayerController { get => _playerController; }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    void Start()
    {
        Connect();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("���r�[�ɎQ��");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("�}�X�^�[�T�[�o�[�ɐڑ�");
        PhotonNetwork.JoinLobby();
    }

    /// <summary>���[���ɎQ�����Ƀv���C���[�𐶐����� </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("���[���ɓ���");

        if (PhotonNetwork.LocalPlayer.ActorNumber > PhotonNetwork.CurrentRoom.MaxPlayers - 1)
        {
            Debug.Log("�l�����W�܂�܂����@���[������܂�");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

        var index = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        if (index == 0)
        {
            _playerController = PhotonNetwork.Instantiate(_player1PrefabName, _startSpwanPoint[index].position, Quaternion.identity).GetComponent<PlayerController>();
            _playerController.name = "Player1";
            _playerController.PlayerNumber = index;
        }
        else if (index == 1)
        {
            _playerController = PhotonNetwork.Instantiate(_player2PrefabName, _startSpwanPoint[index].position, Quaternion.identity).GetComponent<PlayerController>();
            _playerController.name = "Player2";
            _playerController.PlayerNumber = index;
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("���[���ɓ����o���܂���ł��� ���[����V�����쐬���܂�");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnConnected()
    {
        Debug.Log("�T�[�o�[�ɐڑ�����");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("���[�����쐬");
    }

    void Connect()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            Debug.Log("�T�[�o�[�ɐڑ�");
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}