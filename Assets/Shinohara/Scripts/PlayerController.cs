using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;   
using ExitGames.Client.Photon;  

/// <summary>自機を操作する為のクラス </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField, Header("移動速度")] float _speed = 3f;
    [SerializeField, Header("ジャンプ力")] float _jumpPower = 3f;
    [SerializeField, Tooltip("現在の形")] Shape _currentShape = Shape.Cube;
    Rigidbody2D _rb2D = default;
    /// <summary>何番目のプレイヤーなのか 0=player1 1=player2</summary>
    int _playerNumber = 0;
    /// <summary>滑る床で掛ける力 </summary>
    float _slideFloorPower = 0.01f;
    bool _isJump = true;
    /// <summary>滑る床に乗っているかどうか </summary>
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

            if (!_onSlideFloor) //通常時
            {
                _rb2D.velocity = moveDirction + Vector2.up * verticalVelocity;
            }
            else if (_onSlideFloor) //滑る床に乗っている時
            {
                var slidePower = (_rb2D.velocity * _slideFloorPower);
        
                if (moveDirction != Vector2.zero && _isGrounded)
                {
                    _rb2D.AddForce(moveDirction + slidePower);
                }
            }

            if (Input.GetButtonDown("Fire3"))   //自機の形を変更する
            {
                ChangeShape();
            }

            if (Input.GetButtonDown("Jump") && _isJump)
            {
                _rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            }

            _anim.SetFloat("Speed", Mathf.Abs(horizontal));
            //着地時にアニメーション開始
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

    //アニメーション終了時の処理
    public void EndAnim()
    {
        _isGrounded = true;
        _anim.SetBool("Landing", _isGrounded);
    }

    /// <summary>
    /// RespawnManager.Respawn()が起こしたイベントを受け取る
    /// playerNumberを使って決められた位置にリスポーンする
    /// </summary>
    /// <param name="photonEvent">リスポーンポイント</param>
    public void OnEvent(EventData photonEvent)
    {
        if ((int)photonEvent.Code < 200 && _view.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);  //プレイヤーを再生成する為に削除する
            var data = photonEvent.CustomData;
            var respawnPoints = (Vector3[])data;
            var spawnPoint = respawnPoints[_playerNumber]; 
            NetworkManager.PlayerInstantiate(_playerNumber, spawnPoint);    //プレイヤーを生成する
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
