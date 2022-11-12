using UnityEngine;

/// <summary>インスペクターで表示する配列やリストの要素名を変更する為クラス </summary>
public class PlayerNameArrayAttribute : PropertyAttribute
{
    /// <summary>表示したい名前の配列 </summary>
    public readonly string[] _names;
    public PlayerNameArrayAttribute(string[] names) { _names = names; }
}