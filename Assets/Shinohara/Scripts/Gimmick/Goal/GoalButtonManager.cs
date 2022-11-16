using System;
using UnityEngine;

/// <summary>�e�S�[���{�^�����Ǘ�����N���X </summary>
public class GoalButtonManager : MonoBehaviour
{
    [SerializeField] StageManager _stageManager = default;
    /// <summary>0=1P, 1=2P �̃{�^�� </summary>
    GoalButton[] _goalButtons = new GoalButton[2];

    private void Start()
    {
        _goalButtons[0] = transform.GetChild(0).GetComponent<GoalButton>();
        _goalButtons[1] = transform.GetChild(1).GetComponent<GoalButton>();

        Array.ForEach(_goalButtons, g => g.DelStageClear += _stageManager.StageClear);     //�e�{�^���Ɋ֐���o�^����
    }
}
