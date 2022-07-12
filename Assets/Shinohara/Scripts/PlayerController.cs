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

            if (Input.GetButtonDown("Fire3"))   //自機の形を変更する
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

    /// <summary>
    /// RespawnManager.Respawn()が起こしたイベントを受け取る
    /// playerNumberを使って決められた位置にリスポーンする
    /// </summary>
    /// <param name="photonEvent">リスポーンポイント</param>
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
