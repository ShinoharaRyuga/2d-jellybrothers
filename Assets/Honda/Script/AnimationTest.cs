using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Rigidbody2D _rb2D;
    float _speed = 3;
    [SerializeField]float _jumpPower = 3;
    Shape _currentShape = Shape.Cube;
    bool _isJump = true;

    Animator anim;
    bool _isGrounded = true;

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        var horizontal = Input.GetAxisRaw("Horizontal");
        var moveDirction = new Vector2(horizontal, 0).normalized * _speed;
        float verticalVelocity = _rb2D.velocity.y;
        _rb2D.velocity = moveDirction + Vector2.up * verticalVelocity;

        if (Input.GetButtonDown("Jump") && _isJump)
        {
            _rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        if(_isJump && !_isGrounded)
        {
            anim.SetBool("Landing", _isGrounded);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            _isJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            _isJump = false;
            _isGrounded = false;
        }
    }

    /// <summary>自機の形を変更する </summary>
    void ChangeShape()
    {
        //次の形を決める
        var nextShapeInt = (int)_currentShape + 1;
        var nextShape = nextShapeInt % Enum.GetValues(typeof(Shape)).Length;
        _currentShape = (Shape)nextShape;

        switch (_currentShape)
        {
            case Shape.Cube:
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case Shape.Vertical:
                transform.localScale = new Vector3(1, 3, 1);
                break;
            case Shape.Horizontal:
                transform.localScale = new Vector3(3, 1, 1);
                break;
        }
    }

    enum Shape
    {
        Cube = 0,
        Vertical = 1,
        Horizontal = 2,
    }

    public void EndAnim()
    {
        _isGrounded = true;
        anim.SetBool("Landing", _isGrounded);
    }
}
