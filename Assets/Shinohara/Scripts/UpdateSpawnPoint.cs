using UnityEngine;
using Photon.Pun;

/// <summary>プレイヤーが当たったらリスポーンポイントを更新する </summary>
[RequireComponent(typeof(PhotonView))]
public class UpdateSpawnPoint : MonoBehaviour
{
    [SerializeField, Header("スタートから何番目のポイント")] int _pointNumber = 0;
    [SerializeField, Header("シーンに存在するものをアタッチする")] RespawnManager _respawnManager = default;
    /// <summary>一度だけプレイヤーが当たるようにする </summary>
    bool _isContact = false;
    PhotonView _view => GetComponent<PhotonView>();

    SpriteRenderer _sr => GetComponent<SpriteRenderer>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isContact)
        {
            _respawnManager.SyncRespawnPoint(_pointNumber);
            _view.RPC("PlayerContacted", RpcTarget.All);
        }
    }

    [PunRPC]
    void PlayerContacted()
    {
        _isContact = true;
        _sr.color = Color.red;
    }
}
