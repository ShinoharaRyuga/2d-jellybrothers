using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/// <summary>���X�|�[���Ɋւ��Ă̏�����������Ă���N���X </summary>
public class RespawnManager : MonoBehaviour
{
    [SerializeField, Header("�Q�[���J�n���̃v���C���[�ʒu")] Transform[] _startPoints = new Transform[2];
    [SerializeField, Header("�v���C���[�̃��X�|�[���|�C���g")] List<InspectorRespawnPoints> _respawnPoints = new List<InspectorRespawnPoints>();

    /// <summary>�ŐV�̃��X�|�[���|�C���g�̓Y���� </summary>
    int _currentRespawnIndex = -1;
    PhotonView _view => GetComponent<PhotonView>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log(_currentRespawnIndex);
        }
    }

    /// <summary>
    /// �v���C���[���S���ɌĂяo�����RaiseEvent
    /// �S�v���C���[���ŐV�̃��X�|�[���|�C���g�Ɉړ�������
    /// </summary>
    public void Respawn()
    {
        var respawnPoints = new Vector3[2];     //�e�v���C���[�̃��X�|�[���ʒu�@�Y����0=player1 1=player2

        if (_currentRespawnIndex < 0)   //�v���C���[���ŏ��̃��X�|�[���|�C���g�ɓ��B���Ă��Ȃ���΃X�^�[�g�|�C���g���烊�X�^�[�g������
        {
            respawnPoints = new Vector3[2] { _startPoints[0].position, _startPoints[1].position };
        }
        else
        {
            respawnPoints = _respawnPoints[_currentRespawnIndex].GetRespawnPoints();
        }

        //�I�v�V������ݒ肵�ăC�x���g���N����
        byte eventCode = 0;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All, };
        SendOptions sendOptions = new SendOptions();
        PhotonNetwork.RaiseEvent(eventCode, respawnPoints, raiseEventOptions, sendOptions);
    }

    /// <summary>���X�|�[���|�C���g���X�V���������� </summary>
    public void SyncRespawnPoint(int respawnPointNumber)
    {
        _view.RPC("UpdateRespawnPoint", RpcTarget.All, respawnPointNumber);
    }

    /// <summary>���X�|�[���|�C���g���X�V����</summary>
    [PunRPC]
    public void UpdateRespawnPoint(int respawnPointNumber)
    {
        _currentRespawnIndex = respawnPointNumber;
    }
}

/// <summary>�C���X�y�N�^�[�ɔz��^�̃��X�g��\������ׂ̃N���X </summary>
[Serializable]
public class InspectorRespawnPoints
{
    /// <summary>0=Player1 2=Player2 �̃��X�|�[���|�C���g </summary>
    public Transform[] _respawnPoints = new Transform[2];

    public InspectorRespawnPoints(Transform[] respawnPoints)
    {
        _respawnPoints = respawnPoints;
    }

    /// <summary>���X�|�[���|�C���g��Vector3[]�ɕϊ����ĕԂ� </summary>
    /// <returns>Item1=Player1 Item2=Player2</returns>
    public Vector3[] GetRespawnPoints()
    {
        var points = new Vector3[2] { _respawnPoints[0].position, _respawnPoints[1].position };
        return points;
    }
}
