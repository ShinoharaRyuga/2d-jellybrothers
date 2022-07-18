using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Tooltip("エネミー自身の色")] EnemyColor _enemy = default;
    [SerializeField, Tooltip("エネミーの移動速度")] float _speed = 2f;
    /// <summary>エネミーの進む方向</summary>
    Vector2 direction;
    /// <summary>エネミーの向いている向き </summary>
    Vector2 scale;
    protected Rigidbody2D _rb2D = default;

    protected virtual void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        direction = new Vector2(-1, 0);
        scale = transform.localScale;
    }

    protected virtual void Update()
    {
        var moveDirction = direction.normalized * _speed;
        float verticalVelocity = _rb2D.velocity.y;
        _rb2D.velocity = moveDirction + Vector2.up * verticalVelocity;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (_enemy == EnemyColor.RedEnemy)
        {
            if (collision.gameObject.name == Player.Player1.ToString())
            {
                Destroy(gameObject);
            }
            else if(collision.gameObject.name == Player.Player2.ToString())
            {
                Destroy(collision.gameObject);
            }
        }
        else if(_enemy == EnemyColor.BlueEnemy)
        {
            if (collision.gameObject.name == Player.Player2.ToString())
            {
                Destroy(gameObject);
            }
            else if(collision.gameObject.name == Player.Player1.ToString())
            {
                Destroy(collision.gameObject);
            }
        }

        if(collision.gameObject.name == "Bottom")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Wall")
        {
            scale.x *= -1;
            direction *= -1;
            transform.localScale = scale;
        }
    }

    public enum Player
    {
        Player1,
        Player2,
    }

    public enum EnemyColor
    {
        RedEnemy,
        BlueEnemy,
    }


}
