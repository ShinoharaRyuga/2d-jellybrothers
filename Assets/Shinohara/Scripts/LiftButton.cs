using Photon.Pun;
using UnityEngine;

/// <summary>Liftを動かす為のボタンのクラス </summary>
[RequireComponent(typeof(PhotonView))]
public class LiftButton : MonoBehaviour
{
    [SerializeField, Header("動作させるリフト")] LiftGimmick _liftGimmick = default;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _liftGimmick.View.RPC(nameof(_liftGimmick.LiftMove), RpcTarget.All, Time.deltaTime);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _liftGimmick.View.RPC(nameof(_liftGimmick.ChangeIsArrivel), RpcTarget.All);
        }
    }
}
