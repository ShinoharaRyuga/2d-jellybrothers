using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Cinemachine
{
    public class CameraTarget : MonoBehaviour
    {
        CinemachineTargetGroup cinemachineTargetGroup;
        [SerializeField]GameObject[] targets = new GameObject[2];
        void Start()
        {
            cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
            //�f�o�b�N�p
            //GetTarget();
        }

        //���[������Player���擾���ăJ�����ɉf�����߂̊֐�
        public void GetTarget()
        {
            targets = GameObject.FindGameObjectsWithTag("Player");
            foreach(var t in targets)
            {
                cinemachineTargetGroup.AddMember(t.transform, 1, 1);
            }
        }
    }
}
