using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    bool _isJump = true;

    Animator _anim;
    bool _isGrounded = true;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    void Update()
    {

        var horizontal = Input.GetAxisRaw("Horizontal");
        
        _anim.SetFloat("Speed", Mathf.Abs(horizontal));
        //着地時にアニメーション開始
        if(_isJump && !_isGrounded)
        {
            _anim.SetBool("Landing", _isGrounded);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            _isJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            _isJump = false;
            _isGrounded = false;
        }
    }
    /// <summary>
    /// 着地アニメーション終了時に呼び出される関数
    /// </summary>
    public void EndAnim()
    {
        _isGrounded = true;
        _anim.SetBool("Landing", _isGrounded);
    }
}
