using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RespawnManager))]
public class RespawnManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.HelpBox("RespawnPointの要素数は中間地点と同じ数です\nRespawnPointsのElementについて\nElement0がPlayer1のリスポーンポイントです\nElement1がPlayer2のリスポーンポイントです", MessageType.Info);
    }
}
