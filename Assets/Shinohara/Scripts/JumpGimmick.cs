using UnityEngine;

/// <summary>�v���C���[�������W�����v������M�~�b�N </summary>
public class JumpGimmick : MonoBehaviour
{
    [Tooltip("�v���C���[�̃W�����v�͂��l������������ƃW�����v�͂�������܂�")]
    [SerializeField , Header("�M�~�b�N�쓮���̃W�����v��")] float _jumpPower = 20f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
}
