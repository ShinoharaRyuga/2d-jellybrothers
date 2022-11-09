using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>�S�[�����O�ɑ��݂�������J���邽�߂̃N���X </summary>
public class GoalButton : MonoBehaviour
{
    /// <summary>�X�e�[�W�I���V�[���� </summary>
    const string STAGE_SELECT_SCENE_NAME = "StageSelectScene";

    [SerializeField, Header("�X�e�[�W�I���V�[���ɑJ�ڂ���܂Ŏ���")] float _transitionTime = 3f; 
    [SerializeField, Header("��������v���C���[")] Player _player = default;
    [SerializeField, Tooltip("�����ɊJ���ׂ̃{�^��")] GoalButton _partnerButton = default;

    /// <summary>�v���C���[���{�^���ɐG��Ă��邩�ǂ��� </summary>
    bool _isHit = false;

    event Action _stageClear = default;

    SpriteRenderer _renderer => GetComponent<SpriteRenderer>();
    SetGoalWall _goalGimmick => transform.parent.GetComponent<SetGoalWall>();
    PhotonView _view => GetComponent<PhotonView>();
    /// <summary>�v���C���[���{�^���ɐG��Ă��邩�ǂ��� </summary>
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
        //�v���C���[���{�^����������
        if (collision.gameObject.name == _player.ToString())
        {
            _view.RPC(nameof(SetIsHitTrue), RpcTarget.All);

            if (_partnerButton.IsHit && _isHit) //�����Ƀ{�^����������Ă���������J����
            {
                _view.RPC(nameof(ArrivalGoal), RpcTarget.All);

                StartCoroutine(TransitionStageSelectScene());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�v���C���[���{�^�����痣�ꂽ
        if (collision.gameObject.name == _player.ToString())
        {
            _view.RPC(nameof(SetIsHitFalse), RpcTarget.All);
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

    /// <summary>�S�[�����ɌĂяo�����</summary>
    [PunRPC]
    void ArrivalGoal()
    {
        _stageClear.Invoke();
    }

    /// <summary>��莞�Ԍ�ɃX�e�[�W�I���V�[���ɑJ�ڂ��� </summary>
    IEnumerator TransitionStageSelectScene()
    {
        yield return new WaitForSeconds(_transitionTime);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(STAGE_SELECT_SCENE_NAME);
        }
    }
}
