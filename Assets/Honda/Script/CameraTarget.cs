using UnityEngine;
using Cinemachine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField, Header("プレイヤーの周りの映る範囲を変更出来る")] float _weight = 3f;
    [SerializeField, Header("プレイヤーの周りの映る範囲を変更出来る")] float _radius = 3f;
    CinemachineTargetGroup _cinemachineTargetGroup => transform.GetChild(1).GetComponent<CinemachineTargetGroup>();
    GameObject[] targets;

    /// <summary>
    /// ルーム内のPlayerを取得してカメラに映すための関数
    /// </summary>
    public void GetTarget()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");

        foreach (var t in targets)
        {
            _cinemachineTargetGroup.AddMember(t.transform, _weight, _radius);
        }
    }
}

