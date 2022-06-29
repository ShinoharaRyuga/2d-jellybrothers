using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string _prefabName = "";
    [SerializeField] Transform[] _spwanPoint = new Transform[2];

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    void Start()
    {
        Connect();
    }

    void Update()
    {
       
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            Debug.Log("�T�[�o�[�ɐڑ�");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("���[���ɓ����o���܂���ł���");
    }

    public override void OnConnected()
    {
        Debug.Log("�T�[�o�[�ɐڑ�����");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("���[�����쐬");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("���r�[�ɎQ��");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        PhotonNetwork.JoinRandomOrCreateRoom();
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
        var index = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var player = PhotonNetwork.Instantiate(_prefabName, _spwanPoint[index].position, Quaternion.identity).GetComponent<PlayerController>();
        
        if (index == 0)
        {
            player.SetColor(Color.red);
        }
        else if (index == 1)
        {
            player.SetColor(Color.blue);
        }
        
    }
}
