using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RespawnManager))]
public class RespawnManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.HelpBox("RespawnPoint�̗v�f���͒��Ԓn�_�Ɠ������ł�\nRespawnPoints��Element�ɂ���\nElement0��Player1�̃��X�|�[���|�C���g�ł�\nElement1��Player2�̃��X�|�[���|�C���g�ł�", MessageType.Info);
    }
}
