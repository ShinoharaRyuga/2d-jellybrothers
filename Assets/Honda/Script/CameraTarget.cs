using UnityEngine;
using UnityEngine.UI;

namespace Cinemachine
{
    public class CameraTarget : MonoBehaviour
    {
        CinemachineTargetGroup cinemachineTargetGroup;
        [SerializeField]GameObject[] targets;
        [SerializeField] Text text;

        void Start()
        {
            targets = new GameObject[2];
            cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
            //�f�o�b�N�p
            GetTarget();
        }

        /// <summary>
        /// ���[������Player���擾���ăJ�����ɉf�����߂̊֐�
        /// </summary>
        public void GetTarget()
        {
            targets = GameObject.FindGameObjectsWithTag("Player");
            foreach (var t in targets)
            {
                cinemachineTargetGroup.AddMember(t.transform, 1, 1);
            }
        }

    }
}
