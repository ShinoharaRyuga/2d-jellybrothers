using UnityEngine;

public class PlayerNameArrayAttribute : PropertyAttribute
{
    public readonly string[] names;
    public PlayerNameArrayAttribute(string[] names) { this.names = names; }
}