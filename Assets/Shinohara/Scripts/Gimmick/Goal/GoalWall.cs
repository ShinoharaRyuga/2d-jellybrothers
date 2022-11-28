using Photon.Pun;
using UnityEngine;

/// <summary>ゴール直前にある扉　2人同時にボタンを押さなければならない </summary>
[RequireComponent(typeof(PhotonView))]
public class GoalWall : MonoBehaviour
{
    public PhotonView View => GetComponent<PhotonView>();

    /// <summary>扉を開ける </summary>
    [PunRPC]
    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

    [PunRPC]
    public void SetActiveTrue()
    {
        gameObject.SetActive(true);
    }
}
