using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;   
using ExitGames.Client.Photon;  

/// <summary>���@�𑀍삷��ׂ̃N���X </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    /// <summary>�g�p�o����photo�̃C�x���g�R�[�h </summary>
    const int PHOTON_EVENT_CODE = 200;

    [SerializeField, Header("�ړ����x")] float _speed = 3f;
    [SerializeField, Header("�W�����v��")] float _jumpPower = 3f;
    [SerializeField, Header("�g�p�C���X�g")] Sprite[] _useSprites = default;
    [SerializeField, Tooltip("���݂̌`")] Shape _currentShape = Shape.Cube;
    Rigidbody2D _rb2D = default;
    /// <summary>���Ԗڂ̃v���C���[�Ȃ̂� 0=player1 1=player2</summary>
    int _playerNumber = 0;
    /// <summary>���鏰�Ŋ|����� </summary>
    float _slideFloorPower = 0.01f;
    /// <summary>�O�t���[���̓��� </summary>
    float _lastHorizontal =  0f;
    bool _isJump = true;
    /// <summary>���鏰�ɏ���Ă��邩�ǂ��� </summary>
    bool _onSlideFloor = false;
    bool _onBeltConveyor = false;
    bool _isGrounded = true;
    /// <summary>�x���g�R���x�A�ɏ���Ă��鎞�̌���</summary>
    Vector2 _onBeltConveyorDirection = Vector2.zero;
    PhotonView _view => GetComponent<PhotonView>();
    Animator _anim => GetComponent<Animator>();
    SpriteRenderer _sr => GetComponent<SpriteRenderer>();
   
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
    public bool OnBeltConveyor { get => _onBeltConveyor; set => _onBeltConveyor = value; }

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

            if (_lastHorizontal != horizontal)  //������ς���
            {
                var parameter = new object[] { horizontal };
                _view.RPC(nameof(ChangePlayerDirection), RpcTarget.All, parameter);
            }

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

            if(_anim)
            {
                _anim.SetFloat("Speed", Mathf.Abs(horizontal));
                //���n���ɃA�j���[�V�����J�n
                if (_isJump && !_isGrounded)
                {
                    _anim.SetBool("Landing", _isGrounded);
                }
            }

            if (_onBeltConveyor)
            {
                transform.up = _onBeltConveyorDirection;
            }

            _lastHorizontal = horizontal;
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
                _sr.sprite = _useSprites[0];
                break;
            case Shape.Vertical:
                transform.localScale = new Vector3(1, 3, 1);
                _sr.sprite = _useSprites[1];
                break;
            case Shape.Horizontal:
                transform.localScale = new Vector3(3, 1, 1);
                _sr.sprite = _useSprites[2];
                break;
        }
    }

    /// <summary>���@�̌�����ύX���������� </summary>
    [PunRPC]
    void ChangePlayerDirection(float horizontal)
    {
        if (horizontal > 0)     //�E������
        {
            _sr.flipX = false;
        }
        else if (horizontal < 0)    //��������
        {
            _sr.flipX = true;
        }

    }

    /// <summary>���鏰�Ŋ|����͂��擾 </summary>
    /// <param name="power"></param>
    public void GetSlidePower (float power)
    {
        _slideFloorPower = power;
        _onSlideFloor = true;
    }

    //�A�j���[�V�����I�����̏���
    public void EndAnim()
    {
        if(_anim)
        {
            _isGrounded = true;
            _anim.SetBool("Landing", _isGrounded);
        }
    }

    /// <summary>
    /// RespawnManager.Respawn()���N�������C�x���g���󂯎��
    /// playerNumber���g���Č��߂�ꂽ�ʒu�Ƀ��X�|�[������
    /// </summary>
    /// <param name="photonEvent">���X�|�[���|�C���g</param>
    public void OnEvent(EventData photonEvent)
    {
        if ((int)photonEvent.Code < PHOTON_EVENT_CODE && _view.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);  //�v���C���[���Đ�������ׂɍ폜����
            var data = photonEvent.CustomData;
            var respawnPoints = (Vector3[])data;
            var spawnPoint = respawnPoints[_playerNumber];
            transform.position = spawnPoint;
            NetworkManager.PlayerInstantiate(_playerNumber, spawnPoint);    //�v���C���[�𐶐�����
            PlayerData.Instance.GetCameraTargetInvoke();
        }
    }

    enum Shape
    {
        Cube = 0,
        Vertical = 1,
        Horizontal = 2,
    }
}
