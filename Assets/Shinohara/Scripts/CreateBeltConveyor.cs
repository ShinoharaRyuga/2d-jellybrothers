using System;
using UnityEngine;
# if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// �x���g�R���x�A�i�������M�~�b�N�j���쐬����N���X
/// �x���g�R���x�A�̃v���n�u����ׂč쐬���Ă���
/// </summary>
public class CreateBeltConveyor : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")] float _moveSpeed = 1f;
    [SerializeField, Header("�x���g�R���x�A�̐�(����)"), Min(0)] int _beltConveyorNumber = 0;
    [SerializeField, Tooltip("�x���g�R���x�A�̃v���n�u")] SetBeltConveyorSpeed _beltConveyor = default;
    [SerializeField, HideInInspector] float _startPositionX = 0;
    [SerializeField, HideInInspector] float _startRotationZ = 0;

    BoxCollider2D _bc2D => GetComponent<BoxCollider2D>();

    private void OnValidate()
    {
        //�@�x���g�R���x�A�̒�����ύX����
        for (var i = 0; i < transform.childCount; i++)  // ������ύX���鎞�͈�U�������O�ɂ���
        {
            var belt = transform.GetChild(i).gameObject;
#if UNITY_EDITOR
            EditorApplication.delayCall += () => DestroyImmediate(belt);
#endif
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

        if (_beltConveyorNumber % 2 == 0)
        {
            _bc2D.offset = new Vector2(0.5f, 0);
            _bc2D.size = new Vector2(_beltConveyorNumber, 0.5f);
        }
        else
        {
            _bc2D.offset = Vector2.zero;
            _bc2D.size = new Vector2(_beltConveyorNumber, 0.5f);
        }

        if (_beltConveyorNumber != 0)   //�s�{�b�g�̈ʒu�𒆐S�Ɏ����Ă���
        {
            parentPositionX /= _beltConveyorNumber;
            transform.position = new Vector2(parentPositionX, transform.position.y);
            Array.ForEach(beltConveyors, t => t.SetParent(transform));
        }

        transform.rotation = Quaternion.Euler(0, 0, _startRotationZ);
        transform.position = new Vector2(_startPositionX, transform.position.y);
    }
}
