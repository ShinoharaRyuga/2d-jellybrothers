using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GoalButton : MonoBehaviour
{
    [SerializeField, Tooltip("同時に開く為のボタン")] GoalButton goalButton = default;
    [SerializeField, Tooltip("ボタンが押された時に開くドア")] GoalWall _goalGimmick = default;
    [SerializeField, Tooltip("反応するプレイヤー")] Player _player = default;

    PhotonView _view => GetComponent<PhotonView>();
    /// <summary>プレイヤーがボタンに触れているかどうか </summary>
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
                Debug.Log("クリア");
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

    /// <summary>goalの扉を開ける </summary>
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
