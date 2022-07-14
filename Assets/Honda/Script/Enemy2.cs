using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyController
{
    [SerializeField, Tooltip("ƒWƒƒƒ“ƒv—Í")] float _jumpPower = 5;
    bool isGrounded = false;
    float time = 0;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        if(isGrounded == true)
        {
            _rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
