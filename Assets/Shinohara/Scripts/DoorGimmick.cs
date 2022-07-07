using Photon.Pun;
using UnityEngine;

public class DoorGimmick : EnvironmentGimmickBase
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
