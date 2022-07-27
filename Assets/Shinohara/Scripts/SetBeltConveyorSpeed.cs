using UnityEngine;

/// <summary>ベルトコンベアの移動速度を決める </summary>
public class SetBeltConveyorSpeed : MonoBehaviour
{
    float _speed = 1f;

    public float Speed
    {
        get { return _speed; }
        set
        {
            if (0 < value)
            {
                _speed = value;
            }
        }
    }

    Animator _anim => GetComponent<Animator>();

    private void Update()
    {
        _anim.speed = _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 50, ForceMode2D.Impulse);
        }
    }
}
