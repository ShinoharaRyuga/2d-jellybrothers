using Photon.Pun;
using UnityEngine;

public abstract class EnvironmentGimmickBase : MonoBehaviour
{
    PhotonView _view => GetComponent<PhotonView>();

    public PhotonView View { get => _view; }

    public abstract void SetActiveFalse();
    public abstract void SetActiveTrue();
}
