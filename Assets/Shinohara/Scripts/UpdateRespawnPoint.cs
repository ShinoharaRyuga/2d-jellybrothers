using UnityEngine;
using Photon.Pun;

/// <summary>プレイヤーが当たったらリスポーンポイントを更新する </summary>
[RequireComponent(typeof(PhotonView))]
public class UpdateRespawnPoint : MonoBehaviour
{
    [SerializeField, Header("スタートから何番目のポイント")] int _pointNumber = 0;
    [SerializeField, Header("シーンに存在するものをアタッチする")] RespawnManager _respawnManager = default;
    [SerializeField, Tooltip("リスポーンポイント更新後のテクスチャ")] Sprite _updateSprite = null;

    /// <summary>一度だけプレイヤーが当たるようにする </summary>
    bool _isContact = false;
    PhotonView _view => GetComponent<PhotonView>();
    SpriteRenderer _sr => GetComponent<SpriteRenderer>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isContact)
        {
            _sr.sprite = _updateSprite;
            _respawnManager.SyncRespawnPoint(_pointNumber);
            _view.RPC(nameof(PlayerContacted), RpcTarget.All);
        }
    }

    [PunRPC]
    void PlayerContacted()
    {
        _isContact = true;
    }
}
