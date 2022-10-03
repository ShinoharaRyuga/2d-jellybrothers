using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb2D;
    [SerializeField] float _jumpPower;
    bool _isGrounded = true;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
    }
    public void Jump()
    {
        _rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        _isGrounded = false;
    }

    public void Landing()
    {
        _anim.SetBool("Landing", _isGrounded);
    }

    public void EndAnim()
    {
        if (_anim)
        {
            _isGrounded = true;
            _anim.SetBool("Landing", _isGrounded);
        }
    }

}
