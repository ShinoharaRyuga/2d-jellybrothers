using UnityEngine;

/// <summary>GoalButton����Q�Ƃ���ׂ̃N���X </summary>
public class SetGoalWall : MonoBehaviour
{
    [SerializeField, Tooltip("�S�[�����O�̔�")] GoalWall goalWall = default;

    public GoalWall GoalWall { get => goalWall; }
}
