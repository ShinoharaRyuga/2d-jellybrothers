using UnityEngine;

/// <summary>�v���C���[�����S������ </summary>
public class DeadZone : MonoBehaviour
{
    [SerializeField, Tooltip("���X�|�[��������ׂɎ擾")] RespawnManager _respawnManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _respawnManager.Respawn();
        }
    }
}
