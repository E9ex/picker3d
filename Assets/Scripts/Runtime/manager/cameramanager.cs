using System;
using Cinemachine;
using signals;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.manager
{
    public class cameramanager : MonoBehaviour
    {
        [SerializeField]private CinemachineVirtualCamera _virtualCamera;

        private float3 _firstPosition;

        private void Start()
        {
            Init();
        }

        private  void Init()
        {
            _firstPosition = transform.position;
        }

        private void OnEnable()
        {
            subscribeEvents();
        }

        private  void subscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.OnReset += Onreset;
        }

        private  void Onreset()
        {
            transform.position = _firstPosition;

        }

        private  void OnSetCameraTarget()
        {
            //var player = FindObjectOfType<Playermanager>().transform;
          //  _virtualCamera.Follow = player;
           // _virtualCamera.LookAt = player;
        }
        private  void UnsubscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.OnReset -= Onreset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}