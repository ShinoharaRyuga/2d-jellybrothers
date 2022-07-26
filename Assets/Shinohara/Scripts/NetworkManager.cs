using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>ネットワークを管理する </summary>
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string _player1PrefabName = "Player1";
    [SerializeField] string _player2PrefabName = "Player2";
    [SerializeField, Header("ゲーム開始時のスタート位置"), Tooltip("添え字 0=Player1 1=Player2")] Transform[] _startSpwanPoint = new Transform[2];
    PlayerController _playerController = default;
    public PlayerController PlayerController { get => _playerController; }

    /// <summary>PlayerInstantiate()で使う為の変数 </summary>
    public static string _player1Name = default;
    /// <summary>PlayerInstantiate()で使う為の変数 </summary>
    public static string _player2Name = default;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        _player1Name = _player1PrefabName;
        _player2Name = _player2PrefabName;
    }

    void Start()
    {
        Connect();
    }

    /// <summary>
    /// <para>プレイヤーを生成する</para>
    /// <para>使用例　プレイヤー死亡時リスポーンポイントでプレイヤーを生成する</para>
    /// </summary>
    /// <param name="playerNumber">どのプレイヤーを生成するか判別する為の変更</param>
    /// <param name="spawnPoint">リスポーンポイント</param>
    /// <returns></returns>
    public static PlayerController PlayerInstantiate(int playerNumber, Vector2 spawnPoint)
    {
        if (playerNumber == 0)
        {
            var player1 = PhotonNetwork.Instantiate(_player1Name, spawnPoint, Quaternion.identity).GetComponent<PlayerController>();
            player1.name = "Player1";
            player1.PlayerNumber = playerNumber;
            return player1;
        }
        else if (playerNumber == 1)
        {
            var player2 = PhotonNetwork.Instantiate(_player2Name, spawnPoint, Quaternion.identity).GetComponent<PlayerController>();
            player2.name = "Player2";
            player2.PlayerNumber = playerNumber;
            return player2;
        }

        return null;
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

    /// <summary> サーバーに接続する</summary>
    void Connect()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            Debug.Log("サーバーに接続");
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
