using UnityEngine;
using Photon.Pun;

/// <summary>ゴール直前に存在する扉を開けるためのクラス </summary>
public class GoalButton : MonoBehaviour
{
    [SerializeField, Header("反応するプレイヤー")] Player _player = default;
    [SerializeField, Tooltip("同時に開く為のボタン")] GoalButton _partnerButton = default;
    /// <summary>プレイヤーがボタンに触れているかどうか </summary>
    bool _isHit = false;

    SpriteRenderer _renderer => GetComponent<SpriteRenderer>();
    SetGoalWall _goalGimmick => transform.parent.GetComponent<SetGoalWall>();
    PhotonView _view => GetComponent<PhotonView>();
    public PhotonView View { get => _view; }
    /// <summary>プレイヤーがボタンに触れているかどうか </summary>
    public bool IsHit { get => _isHit; set => _isHit = value; }

    private void OnValidate()
    {
        if (_player == Player.Player1)
        {
            _renderer.color = Color.red;
        }
        else if(_player == Player.Player2)
        {
            _renderer.color = Color.blue;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーがボタンを押した
        if (collision.gameObject.name == _player.ToString())
        {
            _view.RPC("SetIsHitTrue", RpcTarget.All);

            if (_partnerButton.IsHit && _isHit) //同時にボタンが押されていたら扉を開ける
            {
                _goalGimmick.GoalWall.View.RPC("SetActiveFalse", RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //プレイヤーがボタンから離れた
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
