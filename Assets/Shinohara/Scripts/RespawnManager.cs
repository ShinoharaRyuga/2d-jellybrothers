using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/// <summary>リスポーンに関しての処理が書かれているクラス </summary>
public class RespawnManager : MonoBehaviour
{
    [SerializeField, Header("ゲーム開始時のプレイヤー位置")] Transform[] _startPoints = new Transform[2];
    [SerializeField, Header("プレイヤーのリスポーンポイント")] List<InspectorRespawnPoints> _respawnPoints = new List<InspectorRespawnPoints>();

    /// <summary>最新のリスポーンポイントの添え字 </summary>
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
    /// プレイヤー死亡時に呼び出されるRaiseEvent
    /// 全プレイヤーを最新のリスポーンポイントに移動させる
    /// </summary>
    public void Respawn()
    {
        var respawnPoints = new Vector3[2];     //各プレイヤーのリスポーン位置　添え字0=player1 1=player2

        if (_currentRespawnIndex < 0)   //プレイヤーが最初のリスポーンポイントに到達していなければスタートポイントからリスタートさせる
        {
            respawnPoints = new Vector3[2] { _startPoints[0].position, _startPoints[1].position };
        }
        else
        {
            respawnPoints = _respawnPoints[_currentRespawnIndex].GetRespawnPoints();
        }

        //オプションを設定してイベントを起こす
        byte eventCode = 0;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All, };
        SendOptions sendOptions = new SendOptions();
        PhotonNetwork.RaiseEvent(eventCode, respawnPoints, raiseEventOptions, sendOptions);
    }

    /// <summary>リスポーンポイントを更新し同期する </summary>
    public void SyncRespawnPoint(int respawnPointNumber)
    {
        _view.RPC("UpdateRespawnPoint", RpcTarget.All, respawnPointNumber);
    }

    /// <summary>リスポーンポイントを更新する</summary>
    [PunRPC]
    public void UpdateRespawnPoint(int respawnPointNumber)
    {
        _currentRespawnIndex = respawnPointNumber;
    }
}

/// <summary>インスペクターに配列型のリストを表示する為のクラス </summary>
[Serializable]
public class InspectorRespawnPoints
{
    /// <summary>0=Player1 2=Player2 のリスポーンポイント </summary>
    public Transform[] _respawnPoints = new Transform[2];

    public InspectorRespawnPoints(Transform[] respawnPoints)
    {
        _respawnPoints = respawnPoints;
    }

    /// <summary>リスポーンポイントをVector3[]に変換して返す </summary>
    /// <returns>Item1=Player1 Item2=Player2</returns>
    public Vector3[] GetRespawnPoints()
    {
        var points = new Vector3[2] { _respawnPoints[0].position, _respawnPoints[1].position };
        return points;
    }
}
