using System;
using UnityEngine;
using UnityEditor;

/// <summary>
/// �x���g�R���x�A�i�������M�~�b�N�j���쐬����N���X
/// �x���g�R���x�A�̃v���n�u����ׂč쐬���Ă���
/// </summary>
public class CreateBeltConveyor : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")] float _moveSpeed = 1f;
    [SerializeField, Header("�x���g�R���x�A�̐�(����)"), Min(0)] int _beltConveyorNumber = 0;
    [SerializeField, Tooltip("�x���g�R���x�A�̃v���n�u")] SetBeltConveyorSpeed _beltConveyor = default;
    /// <summary>�I�u�W�F�N�g�̏����ʒu��ݒ肷��� </summary>
    [SerializeField, HideInInspector] Vector2 _startPosition = Vector2.zero;
    [SerializeField, HideInInspector] Quaternion _startRotation = Quaternion.identity;

    private void OnValidate()
    {
        //�@�x���g�R���x�A�̒�����ύX����
        for (var i = 0; i < transform.childCount; i++)  // ������ύX���鎞�͈�U�������O�ɂ���
        {
            var belt = transform.GetChild(i).gameObject;
            EditorApplication.delayCall += () => DestroyImmediate(belt);
        }

        var beltConveyors = new Transform[_beltConveyorNumber];
        var parentPositionX = 0;

        for (var i = 0; i < _beltConveyorNumber; i++)   //�V�����x���g�R���x�A���w�肳�ꂽ��������������
        {
            var pos = new Vector2(i, transform.position.y);
            var belt = Instantiate(_beltConveyor, pos, Quaternion.identity);
            beltConveyors[i] = belt.transform;
            belt.Speed = _moveSpeed;
            parentPositionX += i;
        }

        if (_beltConveyorNumber != 0)   //�s�{�b�g�̈ʒu�𒆐S�Ɏ����Ă���
        {
            parentPositionX /= _beltConveyorNumber;
            transform.position = new Vector2(parentPositionX, transform.position.y);
            Array.ForEach(beltConveyors, t => t.SetParent(transform));
        }
    }

    private void Start()
    {
        transform.position = _startPosition;
    }

    /// <summary>
    /// �����ʒu�Ɖ�]�l�����߂�
    /// </summary>
    public void SetStartPosition()
    {
        _startPosition = transform.position;
        _startRotation = Quaternion.Euler(transform.localEulerAngles);
    }
}
