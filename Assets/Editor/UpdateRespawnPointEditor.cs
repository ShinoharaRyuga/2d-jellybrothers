using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpdateSpawnPoint))]
public class UpdateRespawnPointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.HelpBox("PointNumberについて\n0,1,2…と続いて行きます\nこの値はプレイヤーに触れてほしい順番です\nよって値が小さければスタートに近いリスポーン位置です", MessageType.Info);
    }
}
