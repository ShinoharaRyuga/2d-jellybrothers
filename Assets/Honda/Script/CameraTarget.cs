using UnityEngine;
using Cinemachine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField, Header("�v���C���[�̎���̉f��͈͂�ύX�o����")] float _weight = 3f;
    [SerializeField, Header("�v���C���[�̎���̉f��͈͂�ύX�o����")] float _radius = 3f;
    CinemachineTargetGroup _cinemachineTargetGroup => transform.GetChild(1).GetComponent<CinemachineTargetGroup>();
    GameObject[] targets;

    /// <summary>
    /// ���[������Player���擾���ăJ�����ɉf�����߂̊֐�
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

