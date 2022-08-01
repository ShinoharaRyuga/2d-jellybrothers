using UnityEngine;
using Photon.Pun;

/// <summary>��_�Ԃ��ړ����郊�t�g���ړ�������ׂ̃N���X</summary>
[RequireComponent(typeof(PhotonView))]
public class LiftGimmick : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")] float _moveSpeed = 3f;
    [SerializeField, Header("�J�n�ʒu")] Transform _startPosition = default;
    [SerializeField, Header("�I���ʒu")] Transform _endPosition = default;
    float _usetime = 0f;
    /// <summary>�ړI�n�ɓ��B�������ǂ���</summary>
    bool _isArrival = false;
    /// <summary>endPosition����_startPosition�ɖ߂��Ă���</summary>
    bool _reversal = false;
    /// <summary>�ړI�n��_startPosition��_endPosition�ǂ��炩�ɂȂ� </summary>
    Vector3 _goalPosition = Vector2.zero;
    public PhotonView View => GetComponent<PhotonView>();
    float _distance => Vector2.Distance(_startPosition.position, _endPosition.position);

    public float Usetime { get => _usetime; }

    private void Start()
    {
        _goalPosition = _endPosition.position;
    }

    /// <summary>
    /// Player��lift�𓮂����{�^�����痣�ꂽ���ɌĂ΂��
    /// ���̖ړI�n�Ɉړ�������ׂ�false�ɂ���
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
    ///  Player��lift�𓮂����{�^���ɓ������Ă��鎞�ɌĂ΂��
    ///  Lift�𓮂���
    /// </summary>
    /// <param name="deltaTime">Time.deltaTime</param>
    [PunRPC]
    void LiftMove(float deltaTime)
    {
        if (!_isArrival)
        {
            _usetime += deltaTime;
            float currentPoition = (_usetime * _moveSpeed) / _distance;  // ���݂̈ʒu

            if (!_reversal)
            {
                transform.position = Vector3.Lerp(_startPosition.position, _endPosition.position, currentPoition);  //endPoition�Ɍ������Ĉړ����Ă���
            }
            else
            {
                transform.position = Vector3.Lerp(_endPosition.position, _startPosition.position, currentPoition);  //startPosition�Ɍ������Ĉړ����Ă���
            }

            if (transform.position == _goalPosition)    //�ړI�n�ɓ��B
            {
                _usetime = 0f;
                _reversal = !_reversal;
                ChangeGoalPosition();
                _isArrival = true;
            }
        }
    }

    /// <summary>�ړI�n�����ւ��� </summary>
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
        collision.transform.SetParent(transform);   //Lift�Ƌ��ɓ������ׂɎq�I�u�W�F�N�g�ɂ���
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
