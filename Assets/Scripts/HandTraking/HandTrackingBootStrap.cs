using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;
using Debug = UnityEngine.Debug;


namespace HandTracking
{

    public class HandTrackingBootStrap : MonoBehaviour
    {
        // 오른쪽 손인지 왼쪽 손인지에 대한 값이 들어감
        public HandEnum handEnum;

        // 해당 앵커는 특정 하위 오브젝트에 대해서 위치를 고정해주는 역할을 함.
        public Transform pinAnchor;

        // 어느쪽 손인지 확인
        private const string RIGHT_HAND_LABEL = "R:";
        private const string LEFT_HAND_LABEL = "L:";


        // 제스쳐를 업데이트 하는 함수
        protected void UpdateGesture(HandJointID part)
        {
            // 오른쪽 손 혹은 왼쪽손에 대한 손 상태를 handState에 저장한다.
            var handState = NRInput.Hands.GetHandState(handEnum);

            // 만약 어떠한 제스쳐도 없다면 그냥 리턴해 준다.
            if (handState == null) return;

            string text = GetHandEnumLabel() + GetHandStateText(handState);



            DetectHandTracking(handState, part);
        }

        // 어느 쪽에 대한 손 인지에 대한 문자열을 리턴해 주는 함수
        private string GetHandEnumLabel()
        {
            switch (handEnum)
            {
                case HandEnum.RightHand:
                    return RIGHT_HAND_LABEL;
                case HandEnum.LeftHand:
                    return LEFT_HAND_LABEL;
                default:
                    break;
            }
            return string.Empty;
        }

        // 성공적으로 손이 트래킹이 되면 해당 anchor를 활성화 시키고 transform시켜줌
        private void DetectHandTracking(dynamic handState, HandJointID part)
        {
            if (handState.isTracked)
            {
                // 해당 손 에 대한 위치를 담아줄 변수
                Pose partPose;
                // HandJointID.Palm 이란 손바닥에 대한 아이디 값
                if (handState.jointsPoseDict.TryGetValue(part, out partPose))
                {
                    UpdateAnchorTransform(partPose.position);
                }
                pinAnchor.gameObject.SetActive(true);
            }
            else
            {
                pinAnchor.gameObject.SetActive(false);
            }
        }


        // 현재 손 상태에 대한 문자열을 반환해 주는 함수
        private string GetHandStateText(dynamic handState)
        {
            switch (handState.currentGesture)
            {
                case HandGesture.OpenHand:
                    return "손 펴짐";
                case HandGesture.Grab:
                    return "잡음";
                case HandGesture.Pinch:
                    return "꼬집음";
                case HandGesture.Victory:
                    return "승리";
                case HandGesture.Call:
                    return "뭔가 부르는거 같은데";
                case HandGesture.System:
                    return "뭘까 이건?";
                default:
                    return string.Empty;
            }
        }

        private void UpdateAnchorTransform(Vector3 jointPos)
        {
            var vec_from_head = jointPos - Camera.main.transform.position;
            var vec_horizontal = Vector3.Cross(Vector3.down, vec_from_head).normalized;
            pinAnchor.position = jointPos + Vector3.up * 0.01f - vec_horizontal * 0.015f;
            pinAnchor.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(vec_from_head, Vector3.up), Vector3.up);
        }

    }

}