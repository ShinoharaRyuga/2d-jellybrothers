using UnityEngine;

/// <summary>�C���X�y�N�^�[�ŕ\������z��⃊�X�g�̗v�f����ύX����׃N���X </summary>
public class PlayerNameArrayAttribute : PropertyAttribute
{
    /// <summary>�\�����������O�̔z�� </summary>
    public readonly string[] _names;
    public PlayerNameArrayAttribute(string[] names) { _names = names; }
}