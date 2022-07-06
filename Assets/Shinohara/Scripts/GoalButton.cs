using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GoalButton : MonoBehaviour
{
    [SerializeField, Tooltip("�����ɊJ���ׂ̃{�^��")] GoalButton goalButton = default;
    [SerializeField, Tooltip("�{�^���������ꂽ���ɊJ���h�A")] GoalWall _goalGimmick = default;
    [SerializeField, Tooltip("��������v���C���[")] Player _player = default;

    PhotonView _view => GetComponent<PhotonView>();
    /// <summary>�v���C���[���{�^���ɐG��Ă��邩�ǂ��� </summary>
    bool _isHit = false;
    public bool IsHit { get => _isHit; set => _isHit = value; }

    public PhotonView View { get => _view; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == _player.ToString())
        {
            _view.RPC("SetIsHitTrue", RpcTarget.All);

            if (goalButton.IsHit && _isHit)
            {
                Debug.Log("�N���A");
                _goalGimmick.View.RPC("SetActiveFalse", RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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
