using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] RespawnManager _respawnManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _respawnManager.Respawn();
        }
    }
}
