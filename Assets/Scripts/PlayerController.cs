using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("移動速度")] float _speed = 3f;
    [SerializeField, Tooltip("現在の形")] Shape _currentShape = Shape.Cube;
    Vector2 _moveDirction = Vector2.zero;
    float _horizontal = 0f;
    Rigidbody2D _rb2D = default;
    PhotonView _view => GetComponent<PhotonView>();

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
            _horizontal = Input.GetAxisRaw("Horizontal");
            _moveDirction = new Vector2(_horizontal, 0).normalized * _speed;

            if (Input.GetButtonDown("Jump"))
            {
                ChangeShape();
            }
        }
       
    }

    void FixedUpdate()
    {
        if (_rb2D != null)
        {
            if (_moveDirction != Vector2.zero)
            {
                _rb2D.velocity = _moveDirction;
            }
            else
            {
                _rb2D.velocity = Vector2.zero;
            }
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
}
