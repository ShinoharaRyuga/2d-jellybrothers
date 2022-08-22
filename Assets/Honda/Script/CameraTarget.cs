using UnityEngine;
using Cinemachine;

/// <summary>カメラに映すプレイヤーの自機達を取得する </summary>
public class CameraTarget : MonoBehaviour
{
    [SerializeField, Header("シーン内にあるものをアタッチして下さい")] CinemachineTargetGroup _cinemachineTargetGroup;
    [SerializeField, Header("プレイヤーの周りの映る範囲を変更出来る")] float _weight = 3f;
    [SerializeField] float _radius = 3f;

    /// <summary>
    /// ルーム内のPlayerを取得してカメラに映すための関数
    /// </summary>
    public void GetTarget()
    {
        var targets = GameObject.FindGameObjectsWithTag("Player");
  
        foreach (var t in targets)
        {
             _cinemachineTargetGroup.AddMember(t.transform, _weight, _radius);
        }
    }
}

