using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButton : MonoBehaviour
{
    [SerializeField] DoubleButton button = default;
    [SerializeField] FloorGimmick gimmick = default;
    [SerializeField, Tooltip("îΩâûÇ∑ÇÈÉvÉåÉCÉÑÅ[")] Player _player = default;
    bool _isHit = false;

    public bool IsHit { get => _isHit; set => _isHit = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == _player.ToString())
        {
            _isHit = true;

            if (button.IsHit && _isHit)
            {
                Debug.Log("ê¨å˜");
                gimmick.View.RPC("SetActiveTrue", Photon.Pun.RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == _player.ToString())
        {
            _isHit = false;
            gimmick.View.RPC("SetActiveFalse", Photon.Pun.RpcTarget.All);
        }
    }
}
