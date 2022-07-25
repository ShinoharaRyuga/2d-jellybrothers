using System;
using System.Collections.Generic;
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
    [SerializeField, Header("�ړ��������]")] bool _isReverse = false;
    [SerializeField, Header("�ړ����x")] float _moveSpeed = 1f;
    [SerializeField, Header("�A�j���[�V�����X�s�[�h")] float _animationSpeed = 1f;
    [SerializeField, Header("�x���g�R���x�A�̐�(����)"), Min(0)] int _beltConveyorNumber = 0;
    [SerializeField, Tooltip("�x���g�R���x�A�̃v���n�u")] SetBeltConveyorSpeed _beltConveyor = default;
    /// <summary>
    /// X�ʒu��ۑ����Ă����ׂ̕ϐ�
    /// �ۑ������Ȃ��ƒl������ɕς���Ă��܂�
    /// </summary>
    [SerializeField, HideInInspector] float _startPositionX = 0;

    /// <summary>�x���g�R���x�A�ɐG�ꂽ�I�u�W�F�N�g��List</summary>
    List<Rigidbody2D> _rb2Ds = new List<Rigidbody2D>();
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

        var beltConveyors = new Transform[_beltConveyorNumber]; //���������I�u�W�F�N�g���ꊇ�Ŏq�I�u�W�F�N�g�ɓ����ׂɕۑ����Ă����ϐ�
        var parentPositionX = 0;    //�ŏI�I�Ȑe�I�u�W�F�N�g�̈ʒu�i�s�{�b�g�̈ʒu�j

        for (var i = 0; i < _beltConveyorNumber; i++)   //�V�����x���g�R���x�A���w�肳�ꂽ��������������
        {
            var pos = new Vector2(i, transform.position.y);
            var belt = Instantiate(_beltConveyor, pos, Quaternion.identity);
            beltConveyors[i] = belt.transform;
           // beltConveyors[i].position = transform.position + transform.eulerAngles.normalized * parentPositionX * i;
            belt.Speed = _animationSpeed;
            parentPositionX += i;
        }

        if (_beltConveyorNumber != 0)   //�s�{�b�g�̈ʒu�𒆐S�Ɏ����Ă���
        {
            parentPositionX /= _beltConveyorNumber;
            transform.position = new Vector2(parentPositionX + 0.5f, transform.position.y);
            Array.ForEach(beltConveyors, t => t.SetParent(transform));
        }
        
        //�����蔻���ύX����
        _bc2D.offset = new Vector2(-0.5f, 0); ;
        _bc2D.size = new Vector2(_beltConveyorNumber, 0.5f);

        //�ʒu�Ɖ�]��ύX����
        float x = Mathf.Cos(transform.eulerAngles.z / 180 * Mathf.PI);
        float y = Mathf.Sin(transform.eulerAngles.z / 180 * Mathf.PI);
        Vector2 startPos = new Vector2(x, y);
        for (int i = 0; i < beltConveyors.Length; i++)
        {
            beltConveyors[i].position = transform.position + (Vector3)startPos * (i - beltConveyors.Length / 2f);
            beltConveyors[i].rotation = transform.rotation;
        }

        transform.position = new Vector2(_startPositionX, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            var rb2D = collision.gameObject.GetComponent<Rigidbody2D>();
            _rb2Ds.Add(rb2D);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var rb2D in _rb2Ds)
        {
            if (_isReverse)
            {
                rb2D.AddForce(-transform.right * ((_moveSpeed - rb2D.velocity.x) * 20), ForceMode2D.Force);
            }
            else
            {
                rb2D.AddForce(transform.right * ((_moveSpeed - rb2D.velocity.x) * 20), ForceMode2D.Force);
            }
           
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            var rb2D = collision.gameObject.GetComponent<Rigidbody2D>();
            _rb2Ds.Remove(rb2D);
        }
    }
}
