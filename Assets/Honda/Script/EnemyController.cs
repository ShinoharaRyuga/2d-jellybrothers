using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Tooltip("エネミー自身の番号")] Enemy _enemy = default;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_enemy == Enemy.Enemy1)
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
        else if(_enemy == Enemy.Enemy2)
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
    }

    public enum Player
    {
        Player1,
        Player2,
    }

    public enum Enemy
    {
        Enemy1,
        Enemy2,
    }

}
