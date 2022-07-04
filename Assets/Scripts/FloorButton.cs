using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    [SerializeField, Tooltip("�{�^���������ꂽ���ɊJ���h�A")] FloorGimmick _floorGimmick = default;
    [SerializeField, Tooltip("��������v���C���[")] Player _player = default;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == _player.ToString())
        {
            _floorGimmick.View.RPC("SetActiveTrue", Photon.Pun.RpcTarget.All);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == _player.ToString())
        {
            _floorGimmick.View.RPC("SetActiveFalse", Photon.Pun.RpcTarget.All);
        }
    }
}
