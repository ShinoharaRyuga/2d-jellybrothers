using Photon.Pun;
using UnityEngine;

/// <summary>各ギミックの基底クラス </summary>
public abstract class EnvironmentGimmickBase : MonoBehaviour
{
    PhotonView _view => GetComponent<PhotonView>();

    public PhotonView View { get => _view; }

    /// <summary>オブジェクトを消す </summary>
    public abstract void SetActiveFalse();

    /// <summary>オブジェクトを出す </summary>
    public abstract void SetActiveTrue();
}
