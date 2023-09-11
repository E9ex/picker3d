using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Runtime.signals;
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

        private  void subscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.OnReset += Onreset;
        }

        private  void Onreset()
        {
            transform.position = _firstPosition;
        }

        private void OnSetCameraTarget()
        {
            var player = FindObjectOfType<PlayManager>();
            if (player != null)
            {
                _virtualCamera.Follow = player.transform;
                // _virtualCamera.LookAt = player.transform;
            }
            else
            {
                Debug.LogError("PlayManager not found in the scene.");
            }
        }

        private  void UnsubscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.OnReset -= Onreset;
        }

        private void OnEnable()
        {
            StartCoroutine(WaitForInitialization());
        }

        private IEnumerator WaitForInitialization()
        {
            while (CameraSignals.Instance == null || CoreGameSignals.Instance == null)
            {
                yield return null;
            }

            subscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

    }
}