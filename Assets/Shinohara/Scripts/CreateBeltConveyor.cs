using System;
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
    [SerializeField, Header("移動速度")] float _moveSpeed = 1f;
    [SerializeField, Header("ベルトコンベアの数(長さ)"), Min(0)] int _beltConveyorNumber = 0;
    [SerializeField, Tooltip("ベルトコンベアのプレハブ")] SetBeltConveyorSpeed _beltConveyor = default;
    [SerializeField, HideInInspector] float _startPositionX = 0;
    [SerializeField, HideInInspector] float _startRotationZ = 0;

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

        if (_beltConveyorNumber != 0)   //ピボットの位置を中心に持ってくる
        {
            parentPositionX /= _beltConveyorNumber;
            transform.position = new Vector2(parentPositionX, transform.position.y);
            Array.ForEach(beltConveyors, t => t.SetParent(transform));
        }

        transform.rotation = Quaternion.Euler(0, 0, _startRotationZ);
        transform.position = new Vector2(_startPositionX, transform.position.y);
    }
}
