using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("ˆÚ“®‘¬“x")] float _speed = 3f;

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
        _horizontal = Input.GetAxisRaw("Horizontal");
        _moveDirction = new Vector2(_horizontal, 0).normalized * _speed;
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
}
