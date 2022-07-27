using UnityEngine;
using Photon.Pun;

/// <summary>�v���C���[�����������烊�X�|�[���|�C���g���X�V���� </summary>
[RequireComponent(typeof(PhotonView))]
public class UpdateSpawnPoint : MonoBehaviour
{
    [SerializeField, Header("�X�^�[�g���牽�Ԗڂ̃|�C���g")] int _pointNumber = 0;
    [SerializeField, Header("�V�[���ɑ��݂�����̂��A�^�b�`����")] RespawnManager _respawnManager = default;
    /// <summary>��x�����v���C���[��������悤�ɂ��� </summary>
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
