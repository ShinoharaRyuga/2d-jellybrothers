using Photon.Pun;
using UnityEngine;

/// <summary>�S�[�����O�ɂ�����@2�l�����Ƀ{�^���������Ȃ���΂Ȃ�Ȃ� </summary>
public class GoalWall : EnvironmentGimmickBase
{
    [PunRPC]
    public override void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

    [PunRPC]
    public override void SetActiveTrue()
    {
        gameObject.SetActive(true);
    }
}
