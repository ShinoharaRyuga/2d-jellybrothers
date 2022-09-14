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
    /// <summary>���鏰�Ŋ|����� </summary>
    float _slideFloorPower = 0.01f;
    bool _isJump = true;
    /// <summary>���鏰�ɏ���Ă��邩�ǂ��� </summary>
    bool _onSlideFloor = false;
    bool _isGrounded = true;
    PhotonView _view => GetComponent<PhotonView>();
    Animator _anim;
   
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

    public bool OnSlideFloor { get => _onSlideFloor; set => _onSlideFloor = value; }

    void Start()
    {
        if (_view.IsMine)
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (_view.IsMine)
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            if(horizontal > 0)
            {
                gameObject.transform.eulerAngles = new Vector2(0, 0);

            }
            else if(horizontal < 0)
            {
                gameObject.transform.eulerAngles = new Vector2(0, 180);
            }

            var moveDirction = new Vector2(horizontal, 0).normalized * _speed;
            float verticalVelocity = _rb2D.velocity.y;

            if (!_onSlideFloor) //�ʏ펞
            {
                _rb2D.velocity = moveDirction + Vector2.up * verticalVelocity;
            }
            else if (_onSlideFloor) //���鏰�ɏ���Ă��鎞
            {
                var slidePower = (_rb2D.velocity * _slideFloorPower);
        
                if (moveDirction != Vector2.zero && _isGrounded)
                {
                    _rb2D.AddForce(moveDirction + slidePower);
                }
            }

            if (Input.GetButtonDown("Fire3"))   //���@�̌`��ύX����
            {
                ChangeShape();
            }

            if (Input.GetButtonDown("Jump") && _isJump)
            {
                _rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            }

            _anim.SetFloat("Speed", Mathf.Abs(horizontal));
            //���n���ɃA�j���[�V�����J�n
            if (_isJump && !_isGrounded)
            {
                _anim.SetBool("Landing", _isGrounded);
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("SlideFloor"))
        {
            _isJump = true;

            if (!collision.gameObject.CompareTag("SlideFloor"))
            {
                _onSlideFloor = false;
            }  
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("SlideFloor"))
        {
            _isJump = false;
            _isGrounded = false;
        }
    }

    public void GetSlidePower (float power)
    {
        _slideFloorPower = power;
    }

    //�A�j���[�V�����I�����̏���
    public void EndAnim()
    {
        _isGrounded = true;
        _anim.SetBool("Landing", _isGrounded);
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
            PhotonNetwork.Destroy(gameObject);  //�v���C���[���Đ�������ׂɍ폜����
            var data = photonEvent.CustomData;
            var respawnPoints = (Vector3[])data;
            var spawnPoint = respawnPoints[_playerNumber]; 
            NetworkManager.PlayerInstantiate(_playerNumber, spawnPoint);    //�v���C���[�𐶐�����
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
