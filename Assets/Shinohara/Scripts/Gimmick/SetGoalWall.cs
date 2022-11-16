using UnityEngine;

/// <summary>GoalButtonから参照する為のクラス </summary>
public class SetGoalWall : MonoBehaviour
{
    [SerializeField, Tooltip("ゴール直前の扉")] GoalWall goalWall = default;

    public GoalWall GoalWall { get => goalWall; }
}
