using Photon.Pun;
using UnityEngine;

/// <summary>Lift�𓮂����ׂ̃{�^���̃N���X </summary>
[RequireComponent(typeof(PhotonView))]
public class LiftButton : MonoBehaviour
{
    [SerializeField, Header("���삳���郊�t�g")] LiftGimmick _liftGimmick = default;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _liftGimmick.View.RPC(nameof(LiftGimmick.LiftMove), RpcTarget.All, Time.deltaTime);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _liftGimmick.View.RPC("ChangeIsArrivel", RpcTarget.All);
        }
    }
}
