using Photon.Pun;
using UnityEngine;

/// <summary>�e�M�~�b�N�̊��N���X </summary>
public abstract class EnvironmentGimmickBase : MonoBehaviour
{
    PhotonView _view => GetComponent<PhotonView>();

    public PhotonView View { get => _view; }

    /// <summary>�I�u�W�F�N�g������ </summary>
    public abstract void SetActiveFalse();

    /// <summary>�I�u�W�F�N�g���o�� </summary>
    public abstract void SetActiveTrue();
}
