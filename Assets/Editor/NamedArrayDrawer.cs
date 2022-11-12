using UnityEngine;
using UnityEditor;

/// <summary>i�C���X�y�N�^�[�ŕ\�������z��⃊�X�g�̗v�f����ύX����׃N���X </summary>
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