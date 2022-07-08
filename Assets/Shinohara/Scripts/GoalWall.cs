using Photon.Pun;
using UnityEngine;

/// <summary>ゴール直前にある扉　2人同時にボタンを押さなければならない </summary>
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
