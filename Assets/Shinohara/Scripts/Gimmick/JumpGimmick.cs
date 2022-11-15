using UnityEngine;

/// <summary>プレイヤーを高くジャンプさせるギミック </summary>
public class JumpGimmick : MonoBehaviour
{
    [Tooltip("プレイヤーのジャンプ力より値を小さくするとジャンプ力が下がります")]
    [SerializeField , Header("ギミック作動時のジャンプ力")] float _jumpPower = 20f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
}
