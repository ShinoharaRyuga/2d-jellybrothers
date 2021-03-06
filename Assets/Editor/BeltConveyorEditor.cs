using UnityEditor;
using UnityEngine;

/// <summary>
/// ベルトコンベアの初期位置と回転値を設定する
/// </summary>
[CustomEditor(typeof(CreateBeltConveyor))]
public class BeltConveyorEditor : Editor
{
    SerializedProperty _startPositionX = default;

    private void OnEnable()
    {
        _startPositionX = serializedObject.FindProperty("_startPositionX");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CreateBeltConveyor createBeltConveyor = target as CreateBeltConveyor;

        EditorGUILayout.HelpBox("Transformの値を保存しないと\nゲーム再生時に設定した位置にオブジェクトが来ないので\nTransformの値を変更したら保存するようにして下さい", MessageType.None);

        if (GUILayout.Button("Transformの値を保存"))
        {
            serializedObject.Update();
            _startPositionX.floatValue = createBeltConveyor.transform.position.x;
            serializedObject.ApplyModifiedProperties();
            Debug.Log("値が保存されました");
        }
    }
}

