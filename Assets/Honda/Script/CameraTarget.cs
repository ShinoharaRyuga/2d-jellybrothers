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
            //デバック用
            //GetTarget();
        }

        //ルーム内のPlayerを取得してカメラに映すための関数
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
