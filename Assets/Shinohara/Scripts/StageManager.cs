using UnityEngine;

/// <summary>�Q�[���V�[���̊Ǘ��N���X </summary>
public class StageManager : MonoBehaviour
{
    [PlayerNameArrayAttribute(new string[] { "Player1", "Player2" })]
    [SerializeField, Header("�Q�[���J�n���̃X�^�[�g�ʒu"), Tooltip("�Y���� 0=Player1 1=Player2")] Transform[] _startSpwanPoint = new Transform[2];
    [SerializeField, Tooltip("�t�F�[�h�A�E�g����v���n�u")] Fade _fadeOut = default;
    PlayerController _player = default;
    CameraTarget _cameraTarget => GetComponent<CameraTarget>();

    private void Start()
    {
        Instantiate(_fadeOut);
        var playerNumber = PlayerData.Instance.PlayerController.PlayerNumber;
        _player = NetworkManager.PlayerInstantiate(playerNumber, _startSpwanPoint[playerNumber].position).GetComponent<PlayerController>();
        _player.IsMove = false;
    }

    /// <summary>�J�����̃^�[�Q�b�g(Player)���擾���� </summary>
    public void GetCameraTarget()
    {
        _cameraTarget.GetTarget();
    }

    /// <summary>�v���C���[�̈ړ��𐧌��E�\�ɂ���  </summary>
    public void PlayerMove(bool isMove)
    {
        _player.IsMove = isMove;
    }
}
