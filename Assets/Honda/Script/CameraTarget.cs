using UnityEngine;
using Cinemachine;

/// <summary>�J�����ɉf���v���C���[�̎��@�B���擾���� </summary>
public class CameraTarget : MonoBehaviour
{
    [SerializeField, Header("�V�[�����ɂ�����̂��A�^�b�`���ĉ�����")] CinemachineTargetGroup _cinemachineTargetGroup;
    [SerializeField, Header("�v���C���[�̎���̉f��͈͂�ύX�o����")] float _weight = 3f;
    [SerializeField] float _radius = 3f;

    /// <summary>
    /// ���[������Player���擾���ăJ�����ɉf�����߂̊֐�
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

