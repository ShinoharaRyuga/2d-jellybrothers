using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpdateSpawnPoint))]
public class UpdateRespawnPointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.HelpBox("PointNumber�ɂ���\n0,1,2�c�Ƒ����čs���܂�\n���̒l�̓v���C���[�ɐG��Ăق������Ԃł�\n����Ēl����������΃X�^�[�g�ɋ߂����X�|�[���ʒu�ł�", MessageType.Info);
    }
}
