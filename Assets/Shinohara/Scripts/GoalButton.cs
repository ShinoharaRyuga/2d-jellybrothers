using UnityEngine;
using Photon.Pun;

/// <summary>�S�[�����O�ɑ��݂�������J���邽�߂̃N���X </summary>
public class GoalButton : MonoBehaviour
{
    [SerializeField, Header("�{�^���������ꂽ���ɊJ���h�A")] GoalWall _goalGimmick = default;
    [SerializeField, Header("��������v���C���[")] Player _player = default;
    [SerializeField, Tooltip("�����ɊJ���ׂ̃{�^��")] GoalButton _partnerButton = default;
    /// <summary>�v���C���[���{�^���ɐG��Ă��邩�ǂ��� </summary>
    bool _isHit = false;
    PhotonView _view => GetComponent<PhotonView>();
    public PhotonView View { get => _view; }
    /// <summary>�v���C���[���{�^���ɐG��Ă��邩�ǂ��� </summary>
    public bool IsHit { get => _isHit; set => _isHit = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[���{�^����������
        if (collision.gameObject.name == _player.ToString())
        {
            _view.RPC("SetIsHitTrue", RpcTarget.All);

            if (_partnerButton.IsHit && _isHit) //�����Ƀ{�^����������Ă���������J����
            {
                _goalGimmick.View.RPC("SetActiveFalse", RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�v���C���[���{�^�����痣�ꂽ
        if (collision.gameObject.name == _player.ToString())
        {
            _view.RPC("SetIsHitFalse", RpcTarget.All);
        }
    }

    /// <summary>goal�̔����J���� </summary>
    [PunRPC]
    void SetIsHitTrue()
    {
        _isHit = true;
    }

    [PunRPC]
    void SetIsHitFalse()
    {
        _isHit = false;
    }
}
