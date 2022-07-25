using System;
using System.Collections.Generic;
using UnityEngine;
# if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// ベルトコンベア（動く床ギミック）を作成するクラス
/// ベルトコンベアのプレハブを並べて作成している
/// </summary>
public class CreateBeltConveyor : MonoBehaviour
{
    [SerializeField, Header("移動方向反転")] bool _isReverse = false;
    [SerializeField, Header("移動速度")] float _moveSpeed = 1f;
    [SerializeField, Header("アニメーションスピード")] float _animationSpeed = 1f;
    [SerializeField, Header("ベルトコンベアの数(長さ)"), Min(0)] int _beltConveyorNumber = 0;
    [SerializeField, Tooltip("ベルトコンベアのプレハブ")] SetBeltConveyorSpeed _beltConveyor = default;
    /// <summary>
    /// X位置を保存しておく為の変数
    /// 保存をしないと値が勝手に変わってしまう
    /// </summary>
    [SerializeField, HideInInspector] float _startPositionX = 0;

    /// <summary>ベルトコンベアに触れたオブジェクトのList</summary>
    List<Rigidbody2D> _rb2Ds = new List<Rigidbody2D>();
    BoxCollider2D _bc2D => GetComponent<BoxCollider2D>();

    private void OnValidate()
    {
        //　ベルトコンベアの長さを変更する
        for (var i = 0; i < transform.childCount; i++)  // 長さを変更する時は一旦長さを０にする
        {
            var belt = transform.GetChild(i).gameObject;
#if UNITY_EDITOR
            EditorApplication.delayCall += () => DestroyImmediate(belt);
#endif
        }

        var beltConveyors = new Transform[_beltConveyorNumber]; //生成したオブジェクトを一括で子オブジェクトに入れる為に保存しておく変数
        var parentPositionX = 0;    //最終的な親オブジェクトの位置（ピボットの位置）

        for (var i = 0; i < _beltConveyorNumber; i++)   //新しくベルトコンベアを指定された長さ分生成する
        {
            var pos = new Vector2(i, transform.position.y);
            var belt = Instantiate(_beltConveyor, pos, Quaternion.identity);
            beltConveyors[i] = belt.transform;
           // beltConveyors[i].position = transform.position + transform.eulerAngles.normalized * parentPositionX * i;
            belt.Speed = _animationSpeed;
            parentPositionX += i;
        }

        if (_beltConveyorNumber != 0)   //ピボットの位置を中心に持ってくる
        {
            parentPositionX /= _beltConveyorNumber;
            transform.position = new Vector2(parentPositionX + 0.5f, transform.position.y);
            Array.ForEach(beltConveyors, t => t.SetParent(transform));
        }
        
        //当たり判定を変更する
        _bc2D.offset = new Vector2(-0.5f, 0); ;
        _bc2D.size = new Vector2(_beltConveyorNumber, 0.5f);

        //位置と回転を変更する
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
