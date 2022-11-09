using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>ゴール直前に存在する扉を開けるためのクラス </summary>
public class GoalButton : MonoBehaviour
{
    /// <summary>ステージ選択シーン名 </summary>
    const string STAGE_SELECT_SCENE_NAME = "StageSelectScene";

    [SerializeField, Header("ステージ選択シーンに遷移するまで時間")] float _transitionTime = 3f; 
    [SerializeField, Header("反応するプレイヤー")] Player _player = default;
    [SerializeField, Tooltip("同時に開く為のボタン")] GoalButton _partnerButton = default;

    /// <summary>プレイヤーがボタンに触れているかどうか </summary>
    bool _isHit = false;

    event Action _stageClear = default;

    SpriteRenderer _renderer => GetComponent<SpriteRenderer>();
    SetGoalWall _goalGimmick => transform.parent.GetComponent<SetGoalWall>();
    PhotonView _view => GetComponent<PhotonView>();
    /// <summary>プレイヤーがボタンに触れているかどうか </summary>
    public bool IsHit { get => _isHit; set => _isHit = value; }

    public event Action StageClear
    {
        add { _stageClear += value; }
        remove { _stageClear -= value; }
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

                StartCoroutine(TransitionStageSelectScene());
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
        _stageClear.Invoke();
    }

    /// <summary>一定時間後にステージ選択シーンに遷移する </summary>
    IEnumerator TransitionStageSelectScene()
    {
        yield return new WaitForSeconds(_transitionTime);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(STAGE_SELECT_SCENE_NAME);
        }
    }
}
