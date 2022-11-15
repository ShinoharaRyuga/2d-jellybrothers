using UnityEngine;

/// <summary>プレイヤーを死亡させる </summary>
public class DeadZone : MonoBehaviour
{
    [SerializeField, Tooltip("リスポーンさせる為に取得")] RespawnManager _respawnManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _respawnManager.Respawn();
        }
    }
}
