using System;
using UnityEngine;
using UnityEditor;

/// <summary>
/// ベルトコンベア（動く床ギミック）を作成するクラス
/// ベルトコンベアのプレハブを並べて作成している
/// </summary>
public class CreateBeltConveyor : MonoBehaviour
{
    [SerializeField, Header("移動速度")] float _moveSpeed = 1f;
    [SerializeField, Header("ベルトコンベアの数(長さ)"), Min(0)] int _beltConveyorNumber = 0;
    [SerializeField, Tooltip("ベルトコンベアのプレハブ")] SetBeltConveyorSpeed _beltConveyor = default;
    /// <summary>オブジェクトの初期位置を設定する為 </summary>
    [SerializeField, HideInInspector] Vector2 _startPosition = Vector2.zero;
    [SerializeField, HideInInspector] Quaternion _startRotation = Quaternion.identity;

    private void OnValidate()
    {
        //　ベルトコンベアの長さを変更する
        for (var i = 0; i < transform.childCount; i++)  // 長さを変更する時は一旦長さを０にする
        {
            var belt = transform.GetChild(i).gameObject;
            EditorApplication.delayCall += () => DestroyImmediate(belt);
        }

        var beltConveyors = new Transform[_beltConveyorNumber];
        var parentPositionX = 0;

        for (var i = 0; i < _beltConveyorNumber; i++)   //新しくベルトコンベアを指定された長さ分生成する
        {
            var pos = new Vector2(i, transform.position.y);
            var belt = Instantiate(_beltConveyor, pos, Quaternion.identity);
            beltConveyors[i] = belt.transform;
            belt.Speed = _moveSpeed;
            parentPositionX += i;
        }

        if (_beltConveyorNumber != 0)   //ピボットの位置を中心に持ってくる
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
    /// 初期位置と回転値を決める
    /// </summary>
    public void SetStartPosition()
    {
        _startPosition = transform.position;
        _startRotation = Quaternion.Euler(transform.localEulerAngles);
    }
}
