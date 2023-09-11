using System;
using System.Collections.Generic;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using signals;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.manager
{
    public class InputManager : MonoBehaviour
    {
        private InputData _data;
        private bool _isavailablefortouch, _isfirstTimetouchtaken, _istouching;
        private float _currentVelocity;
        private float3 _moveVector;
        private Vector2? _Mouseposition;

        private void Awake()
        {
            _data = GetInputdata();
        }

        private InputData GetInputdata()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").data;
        }

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            if (CoreGameSignals.Instance != null)
                CoreGameSignals.Instance.OnReset += OnReset;
            if (InputSignals.Instance != null)
            {
                InputSignals.Instance.OnenableInput += OnEnableInput;
                InputSignals.Instance.OndisableInput += OndisableInput;
            }
        }

        private void OndisableInput()
        {
            _isavailablefortouch = false;
        }

        private void OnEnableInput()
        {
            _isavailablefortouch = true;
        }

        private void OnReset()
        {
            _isavailablefortouch = false;
            _istouching = false;
        }

        private void UnsubscribeEvent()
        {
            if (CoreGameSignals.Instance != null)
                CoreGameSignals.Instance.OnReset -= OnReset;
            if (InputSignals.Instance != null)
            {
                InputSignals.Instance.OnenableInput -= OnEnableInput;
                InputSignals.Instance.OndisableInput -= OndisableInput;
            }
        }

        private void OnDisable()
        {
            UnsubscribeEvent();
        }

        private void Update()
        {
            if (!_isavailablefortouch) return;

            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _istouching = false;
                InputSignals.Instance.OnInputReleased?.Invoke();
            }

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                _istouching = true;
                InputSignals.Instance.OnInputTaken?.Invoke();
                if (!_isfirstTimetouchtaken)
                {
                    _isfirstTimetouchtaken = true;
                    InputSignals.Instance.onfirstTimeTouchTaken?.Invoke();
                }

                _Mouseposition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                if (_istouching)
                {
                    if (_Mouseposition != null)
                    {
                        Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _Mouseposition.Value;
                        if (Mathf.Abs(mouseDeltaPos.x) > _data.HorizontalInputSpeed)
                        {
                            _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        }
                        else
                        {
                            _moveVector.x = Mathf.SmoothDamp(-_moveVector.x, 0f, ref _currentVelocity, _data.ClampSpeed);
                        }

                        _Mouseposition = Input.mousePosition;
                        InputSignals.Instance.OnInputDragged?.Invoke(new horizontalInputParams()
                        {
                            horizontalValue = _moveVector.x,
                            clampValues = _data.clampValues
                        });
                    }
                }
            }
        }

        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}
