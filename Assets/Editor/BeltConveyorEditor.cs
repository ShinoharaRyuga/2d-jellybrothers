using UnityEditor;
using UnityEngine;

/// <summary>
/// �x���g�R���x�A�̏����ʒu�Ɖ�]�l��ݒ肷��
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

        EditorGUILayout.HelpBox("Transform�̒l��ۑ����Ȃ���\n�Q�[���Đ����ɐݒ肵���ʒu�ɃI�u�W�F�N�g�����Ȃ��̂�\nTransform�̒l��ύX������ۑ�����悤�ɂ��ĉ�����", MessageType.None);

        if (GUILayout.Button("Transform�̒l��ۑ�"))
        {
            serializedObject.Update();
            createBeltConveyor.SetStartPosition();
            serializedObject.ApplyModifiedProperties();
            Debug.Log("�l���ۑ�����܂���");
        }
    }
}

