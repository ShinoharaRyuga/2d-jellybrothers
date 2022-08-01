using UnityEngine;
using Photon.Pun;

/// <summary>二点間を移動するリフトを移動させる為のクラス</summary>
[RequireComponent(typeof(PhotonView))]
public class LiftGimmick : MonoBehaviour
{
    [SerializeField, Header("移動速度")] float _moveSpeed = 3f;
    [SerializeField, Header("開始位置")] Transform _startPosition = default;
    [SerializeField, Header("終了位置")] Transform _endPosition = default;
    float _usetime = 0f;
    /// <summary>目的地に到達したかどうか</summary>
    bool _isArrival = false;
    /// <summary>endPositionから_startPositionに戻っている</summary>
    bool _reversal = false;
    /// <summary>目的地が_startPositionと_endPositionどちらかになる </summary>
    Vector3 _goalPosition = Vector2.zero;
    public PhotonView View => GetComponent<PhotonView>();
    float _distance => Vector2.Distance(_startPosition.position, _endPosition.position);

    public float Usetime { get => _usetime; }

    private void Start()
    {
        _goalPosition = _endPosition.position;
    }

    /// <summary>
    /// Playerがliftを動かすボタンから離れた時に呼ばれる
    /// 次の目的地に移動させる為にfalseにする
    /// </summary>
    [PunRPC]
    void ChangeIsArrivel()
    {
        if (_isArrival)
        {
            _isArrival = false;
        }
    }

    /// <summary>
    ///  Playerがliftを動かすボタンに当たっている時に呼ばれる
    ///  Liftを動かす
    /// </summary>
    /// <param name="deltaTime">Time.deltaTime</param>
    [PunRPC]
    void LiftMove(float deltaTime)
    {
        if (!_isArrival)
        {
            _usetime += deltaTime;
            float currentPoition = (_usetime * _moveSpeed) / _distance;  // 現在の位置

            if (!_reversal)
            {
                transform.position = Vector3.Lerp(_startPosition.position, _endPosition.position, currentPoition);  //endPoitionに向かって移動している
            }
            else
            {
                transform.position = Vector3.Lerp(_endPosition.position, _startPosition.position, currentPoition);  //startPositionに向かって移動している
            }

            if (transform.position == _goalPosition)    //目的地に到達
            {
                _usetime = 0f;
                _reversal = !_reversal;
                ChangeGoalPosition();
                _isArrival = true;
            }
        }
    }

    /// <summary>目的地を入れ替える </summary>
    void ChangeGoalPosition()
    {
        if (!_reversal)
        {
            _goalPosition = _endPosition.position;
        }
        else
        {
            _goalPosition = _startPosition.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);   //Liftと共に動かす為に子オブジェクトにする
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
