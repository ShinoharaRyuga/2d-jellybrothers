using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;   
using ExitGames.Client.Photon;  

/// <summary>���@�𑀍삷��ׂ̃N���X </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField, Header("�ړ����x")] float _speed = 3f;
    [SerializeField, Header("�W�����v��")] float _jumpPower = 3f;
    [SerializeField, Tooltip("���݂̌`")] Shape _currentShape = Shape.Cube;
    Rigidbody2D _rb2D = default;
    /// <summary>���Ԗڂ̃v���C���[�Ȃ̂� 0=player1 1=player2</summary>
    int _playerNumber = 0;
    bool _isJump = true;
    PhotonView _view => GetComponent<PhotonView>();

    public int PlayerNumber 
    {
        get { return _playerNumber; }
        set
        {
            if (0 <= value)
            {
                _playerNumber = value;
            }
        }
    }

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

    /// <summary>
    /// RespawnManager.Respawn()���N�������C�x���g���󂯎��
    /// playerNumber���g���Č��߂�ꂽ�ʒu�Ƀ��X�|�[������
    /// </summary>
    /// <param name="photonEvent">���X�|�[���|�C���g</param>
    public void OnEvent(EventData photonEvent)
    {
        if ((int)photonEvent.Code < 200 && _view.IsMine)
        {
            var data = photonEvent.CustomData;
            var respawnPoints = (Vector3[])data;
            transform.position = respawnPoints[_playerNumber];
        }
    }

    enum Shape
    {
        Cube = 0,
        Vertical = 1,
        Horizontal = 2,
    }
}
