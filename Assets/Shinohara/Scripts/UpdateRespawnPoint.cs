using UnityEngine;
using Photon.Pun;

/// <summary>�v���C���[�����������烊�X�|�[���|�C���g���X�V���� </summary>
[RequireComponent(typeof(PhotonView))]
public class UpdateRespawnPoint : MonoBehaviour
{
    [SerializeField, Header("�X�^�[�g���牽�Ԗڂ̃|�C���g")] int _pointNumber = 0;
    [SerializeField, Header("�V�[���ɑ��݂�����̂��A�^�b�`����")] RespawnManager _respawnManager = default;
    [SerializeField, Tooltip("���X�|�[���|�C���g�X�V��̃e�N�X�`��")] Sprite _updateSprite = null;

    /// <summary>��x�����v���C���[��������悤�ɂ��� </summary>
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
