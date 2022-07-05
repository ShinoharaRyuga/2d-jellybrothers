using System;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField, Tooltip("�v���C���[�̃��X�|�[���|�C���g")] List<InspectorRespawnPoints> _respawnPoints = new List<InspectorRespawnPoints>();
    [SerializeField, Tooltip("�Q�[���J�n���̃v���C���[�ʒu")] Transform[] _startPoints = new Transform[2];
    [SerializeField] NetworkManager _networkManager;
    int _currentRespawnIndex = 0;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        var points = _respawnPoints[0].GetRespawnPoints();
        _networkManager.PlayerController.transform.position = points.Item1;
        
    }

    void SetRespawnPoint()
    {
        _currentRespawnIndex++;
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

    /// <summary>���X�|�[���|�C���g��Vector2�ɕϊ����ĕԂ� </summary>
    /// <returns>Item1=Player1 Item2=Player2</returns>
    public ValueTuple<Vector2, Vector2> GetRespawnPoints()
    {
        ValueTuple<Vector2, Vector2> result = new ValueTuple<Vector2, Vector2>();
        result.Item1 = _respawnPoints[0].position;
        result.Item2 = _respawnPoints[1].position;
        return result;
    }
}
