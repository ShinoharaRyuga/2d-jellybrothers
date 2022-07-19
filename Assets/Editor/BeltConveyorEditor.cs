using UnityEditor;
using UnityEngine;

/// <summary>
/// ベルトコンベアの初期位置と回転値を設定する
/// </summary>
[CustomEditor(typeof(CreateBeltConveyor))]
public class BeltConveyorEditor : Editor
{
    SerializedProperty _startPositionProperty;
    SerializedProperty _startRotationProperty;
   
    private void OnEnable()
    {
        _startPositionProperty = serializedObject.FindProperty("_startPosition");
        _startRotationProperty = serializedObject.FindProperty("_startRotation");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CreateBeltConveyor createBeltConveyor = target as CreateBeltConveyor;

        EditorGUILayout.HelpBox("Transformの値を保存しないと\nゲーム再生時に設定した位置にオブジェクトが来ないので\nTransformの値を変更したら保存するようにして下さい", MessageType.None);

        if (GUILayout.Button("Transformの値を保存"))
        {
            serializedObject.Update();
            createBeltConveyor.SetStartPosition();
            serializedObject.ApplyModifiedProperties();
            Debug.Log("値が保存されました");
        }
    }
}

