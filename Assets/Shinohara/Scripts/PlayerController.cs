using System;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("�ړ����x")] float _speed = 3f;
    [SerializeField, Tooltip("�W�����v��")] float _jumpPower = 3f;
    [SerializeField, Tooltip("���݂̌`")] Shape _currentShape = Shape.Cube;
    Rigidbody2D _rb2D = default;
    PhotonView _view => GetComponent<PhotonView>();

    bool _isJump = true;
    void Start()
    {
        if (_view.IsMine)
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (_view.IsMine)
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var moveDirction = new Vector2(horizontal, 0).normalized * _speed;
            float verticalVelocity = _rb2D.velocity.y;
            _rb2D.velocity = moveDirction + Vector2.up * verticalVelocity;

            if (Input.GetButtonDown("Fire3"))   //���@�̌`��ύX����
            {
                ChangeShape();
            }

            if (Input.GetButtonDown("Jump") && _isJump)
            {
                _rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJump = false;
        }
    }

    /// <summary>���@�̌`��ύX���� </summary>
    void ChangeShape()
    {
        //���̌`�����߂�
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
}
