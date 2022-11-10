using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>�l�b�g���[�N���Ǘ����� </summary>
public class NetworkManager : MonoBehaviourPunCallbacks
{
    /// <summary>�X�e�[�W�I���V�[���� </summary>
    const string STAGE_SELECT_SCENE_NAME = "StageSelectScene";
    /// <summary>�t�F�[�h�C�����J�n����܂ł̑҂����� </summary>
    const float FADEIN_OBJECT_WAIT_TIME = 3f;

    [SerializeField, HideInInspector]
    string _player1PrefabName = "Player1";
    [SerializeField, HideInInspector]
    string _player2PrefabName = "Player2";

    [SerializeField, Tooltip("�v���C���[�ɏ���`����ׂ̃e�L�X�g")]
    TMP_Text _informText = default;
    [PlayerNameArrayAttribute(new string[] { "Player1", "Player2" })]
    [SerializeField, Header("�Q�[���J�n���̃X�^�[�g�ʒu"), Tooltip("�Y���� 0=Player1 1=Player2")]
    Transform[] _startSpwanPoint = new Transform[2];

    [Tooltip("�`�F�b�N����Ȃ��Ǝ����I�ɃV�[���J�ڂ��܂�")]
    [SerializeField, Header("�f�o�b�O���̓`�F�b�N����ĉ�����")]
    bool _debugMode = false;

    [SerializeField, Tooltip("�t�F�[�h�C�����s���v���n�u")]
    FadeIn _fadeInPrefab = default;

    PlayerController _playerController = default;
    public PlayerController PlayerController { get => _playerController; }

    /// <summary>PlayerInstantiate()�Ŏg���ׂ̕ϐ� </summary>
    public static string _player1Name = default;
    /// <summary>PlayerInstantiate()�Ŏg���ׂ̕ϐ� </summary>
    public static string _player2Name = default;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        _player1Name = _player1PrefabName;
        _player2Name = _player2PrefabName;
    }

    void Start()
    {
        Connect();
    }

    /// <summary>
    /// <para>�v���C���[�𐶐�����</para>
    /// <para>�g�p��@�v���C���[���S�����X�|�[���|�C���g�Ńv���C���[�𐶐�����</para>
    /// </summary>
    /// <param name="playerNumber">�ǂ̃v���C���[�𐶐����邩���ʂ���ׂ̕ύX</param>
    /// <param name="spawnPoint">���X�|�[���|�C���g</param>
    /// <returns></returns>
    public static PlayerController PlayerInstantiate(int playerNumber, Vector2 spawnPoint)
    {
        if (playerNumber == 0)
        {
            var player1 = PhotonNetwork.Instantiate(_player1Name, spawnPoint, Quaternion.identity).GetComponent<PlayerController>();
            player1.name = _player1Name;
            player1.PlayerNumber = playerNumber;
            return player1;
        }
        else if (playerNumber == 1)
        {
            var player2 = PhotonNetwork.Instantiate(_player2Name, spawnPoint, Quaternion.identity).GetComponent<PlayerController>();
            player2.name = _player2Name;
            player2.PlayerNumber = playerNumber;
            return player2;
        }

        return null;
    }

    /// <summary>�S�v���C���[�𑼂̃V�[���ɑJ�ڂ����� </summary>
    /// <param name="sceneName">�J�ڐ�V�[����</param>
    public static void SceneTransition(string sceneName = STAGE_SELECT_SCENE_NAME)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(sceneName);
        }
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

    /// <summary>
    /// ���[���ɎQ�����Ƀv���C���[�𐶐����� 
    /// �ҋ@�V�[���Ŏg�p
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("���[���ɓ���");

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

            StartCoroutine(CreateFadeInObj());
            _informText.text = "�v���C���[���W�܂�܂���";
        }

        PlayerData.Instance.PlayerController = _playerController;
    }

    /// <summary>�Q���ł��郋�[�����Ȃ���ΐV�������[�����쐬���� </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
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

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("���̃v���C���[���Q�����܂����B");

        if (!_debugMode)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber >= PhotonNetwork.CurrentRoom.MaxPlayers - 1 && PhotonNetwork.IsMasterClient)
            {
                _informText.text = "�v���C���[���W�܂�܂���";
                StartCoroutine(CreateFadeInObj());
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    /// <summary> �T�[�o�[�ɐڑ�����</summary>
    void Connect()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            Debug.Log("�T�[�o�[�ɐڑ�");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /// <summary>��莞�Ԍ�Ƀt�F�[�h�C�����J�n����</summary>
    IEnumerator CreateFadeInObj()
    {
        yield return new WaitForSeconds(FADEIN_OBJECT_WAIT_TIME);

        var fadeInObj = Instantiate(_fadeInPrefab);
        fadeInObj.SceneName = STAGE_SELECT_SCENE_NAME;
    }
}
