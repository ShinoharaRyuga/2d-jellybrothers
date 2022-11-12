using UnityEngine;
using UnityEditor;

/// <summary>iインスペクターで表示される配列やリストの要素名を変更する為クラス </summary>
[CustomPropertyDrawer(typeof(PlayerNameArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.PropertyField(rect, property, new GUIContent(((PlayerNameArrayAttribute)attribute)._names[pos]));

        }
        catch
        {
            EditorGUI.PropertyField(rect, property, label);
        }
    }
}