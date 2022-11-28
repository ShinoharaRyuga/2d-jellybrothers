using Photon.Pun;
using UnityEngine;

/// <summary>�S�[�����O�ɂ�����@2�l�����Ƀ{�^���������Ȃ���΂Ȃ�Ȃ� </summary>
[RequireComponent(typeof(PhotonView))]
public class GoalWall : MonoBehaviour
{
    public PhotonView View => GetComponent<PhotonView>();

    /// <summary>�����J���� </summary>
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
