using System;
using Cinemachine;
using UnityEngine;

namespace Runtime.extentions
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class LockCinemachineAxis : CinemachineExtension
    {
        public enum CinemachinelockAxiss
        {
            x,
            y,
            z
        }

        [SerializeField] private CinemachinelockAxiss lockaxis;
        [Tooltip("lock the camera's X axis Position ")]
        public float Xclampvalue = 0;
        
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            switch (lockaxis)
            {
                case CinemachinelockAxiss.x:
                    if (stage== CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.x = Xclampvalue;
                        state.RawPosition = pos;
                    }
                    break;
                case CinemachinelockAxiss.y:
                    if (stage== CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.y = Xclampvalue;
                        state.RawPosition = pos;
                    }
                    break;
                case CinemachinelockAxiss.z:
                    if (stage== CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.z = Xclampvalue;
                        state.RawPosition = pos;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}