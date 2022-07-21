using UnityEditor;
using UnityEngine;

/// <summary>
/// �x���g�R���x�A�̏����ʒu�Ɖ�]�l��ݒ肷��
/// </summary>
[CustomEditor(typeof(CreateBeltConveyor))]
public class BeltConveyorEditor : Editor
{
    SerializedProperty _startPositionX = default;
    SerializedProperty _startRotationZ = default;

    private void OnEnable()
    {
        _startPositionX = serializedObject.FindProperty("_startPositionX");
        _startRotationZ = serializedObject.FindProperty("_startRotationZ");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CreateBeltConveyor createBeltConveyor = target as CreateBeltConveyor;

        EditorGUILayout.HelpBox("Transform�̒l��ۑ����Ȃ���\n�Q�[���Đ����ɐݒ肵���ʒu�ɃI�u�W�F�N�g�����Ȃ��̂�\nTransform�̒l��ύX������ۑ�����悤�ɂ��ĉ�����", MessageType.None);

        if (GUILayout.Button("Transform�̒l��ۑ�"))
        {
            serializedObject.Update();
            _startPositionX.floatValue = createBeltConveyor.transform.position.x;
            _startRotationZ.floatValue = createBeltConveyor.transform.localEulerAngles.z;
            Debug.Log(createBeltConveyor.transform.localEulerAngles.z);
            serializedObject.ApplyModifiedProperties();
            Debug.Log("�l���ۑ�����܂���");
        }
    }
}

