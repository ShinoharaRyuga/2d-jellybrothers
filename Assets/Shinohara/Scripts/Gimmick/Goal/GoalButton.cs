using System;
using UnityEngine;
using Photon.Pun;

/// <summary>ゴール直前に存在する扉を開けるためのクラス </summary>
[RequireComponent(typeof(SpriteRenderer), typeof(PhotonView))]
public class GoalButton : MonoBehaviour
{
    [SerializeField, Header("ステージ選択シーンに遷移するまで時間")] float _transitionTime = 3f; 
    [SerializeField, Header("反応するプレイヤー")] Player _player = default;
    [SerializeField, Tooltip("同時に開く為のボタン")] GoalButton _partnerButton = default;

    /// <summary>プレイヤーがボタンに触れているかどうか </summary>
    bool _isHit = false;
    /// <summary>ステージクリア時の処理 </summary>
    event Action _delStageClear = default;

    SpriteRenderer _renderer => GetComponent<SpriteRenderer>();
    SetGoalWall _goalGimmick => transform.parent.GetComponent<SetGoalWall>();
    PhotonView _view => GetComponent<PhotonView>();
    /// <summary>プレイヤーがボタンに触れているかどうか </summary>
    public bool IsHit { get => _isHit; set => _isHit = value; }

    /// <summary>ステージクリア時の処理 </summary>
    public event Action DelStageClear
    {
        add { _delStageClear += value; }
        remove { _delStageClear -= value; }
    }

    private void OnValidate()
    {
        if (_player == Player.Player1)
        {
            _renderer.color = Color.red;
        }
        else if (_player == Player.Player2)
        {
            _renderer.color = Color.blue;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーがボタンを押した
        if (collision.gameObject.name == _player.ToString())
        {
            _view.RPC(nameof(SetIsHitTrue), RpcTarget.All);

            if (_partnerButton.IsHit && _isHit) //同時にボタンが押されていたら扉を開ける
            {
                _view.RPC(nameof(ArrivalGoal), RpcTarget.All);
                GetComponent<BoxCollider2D>().enabled = false;  //クリア後にボタンを押せないようにする為

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //プレイヤーがボタンから離れた
        if (collision.gameObject.name == _player.ToString())
        {
            _view.RPC(nameof(SetIsHitFalse), RpcTarget.All);
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

    /// <summary>ゴール時に呼び出される</summary>
    [PunRPC]
    void ArrivalGoal()
    {
        _delStageClear.Invoke();
    }
}
