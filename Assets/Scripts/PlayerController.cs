using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 3f;

    Vector2 _moveDirction = Vector2.zero;
    float _horizontal = 0f;
    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();
    PhotonView _view => GetComponent<PhotonView>();
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();

    void Start()
    {
        
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _moveDirction = new Vector2(_horizontal, 0).normalized * _speed;
    }

    void FixedUpdate()
    {
      //  var moveDirction = new Vector2(_horizontal, 0).normalized * _speed;

        if (_moveDirction != Vector2.zero)
        {
            _rb2D.velocity = _moveDirction;
        }
        else
        {
            _rb2D.velocity = Vector2.zero;
        }
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
