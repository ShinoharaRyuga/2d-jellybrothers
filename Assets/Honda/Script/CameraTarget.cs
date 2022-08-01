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
            //デバック用
            GetTarget();
        }

        /// <summary>
        /// ルーム内のPlayerを取得してカメラに映すための関数
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
