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
            Debug.Log("サーバーに接続");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("ルームに入室出来ませんでした");
    }

    public override void OnConnected()
    {
        Debug.Log("サーバーに接続成功");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("ルームを作成");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("ロビーに参加");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        PhotonNetwork.JoinRandomOrCreateRoom();
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
