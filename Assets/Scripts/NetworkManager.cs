using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>ネットワークを管理する </summary>
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string _player1PrefabName = "Player1";
    [SerializeField] string _player2PrefabName = "Player2";
    [SerializeField] Transform[] _spwanPoint = new Transform[2];

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
        Debug.Log("ロビーに参加");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーに接続");
        PhotonNetwork.JoinLobby();
    }

    /// <summary>ルームに参加時にプレイヤーを生成する </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームに入室");

        if (PhotonNetwork.LocalPlayer.ActorNumber > PhotonNetwork.CurrentRoom.MaxPlayers - 1)
        {
            Debug.Log("人数が集まりました　ルームを閉じます");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

        var index = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        PlayerController player = null;
     
        if (index == 0)
        {
            player = PhotonNetwork.Instantiate(_player1PrefabName, _spwanPoint[index].position, Quaternion.identity).GetComponent<PlayerController>();
            player.name = "Player1";
        }
        else if (index == 1)
        {
            player = PhotonNetwork.Instantiate(_player2PrefabName, _spwanPoint[index].position, Quaternion.identity).GetComponent<PlayerController>();
            player.name = "Player2";
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("ルームに入室出来ませんでした ルームを新しく作成します");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnConnected()
    {
        Debug.Log("サーバーに接続成功");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("ルームを作成");
    }

    void Connect()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            Debug.Log("サーバーに接続");
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
