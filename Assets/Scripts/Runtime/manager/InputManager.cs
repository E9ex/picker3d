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

namespace manager
{
    public class InputManager : MonoBehaviour
    {
        private InputData _data;
        private bool _isavailablefortouch, _isfirstTimetouchtaken,_istouching;
        private float _currentVelocity;
        private float3 _moveVector;
        private Vector2? _Mouseposition;//reftype

        private void Awake()
        {
            _data = Getınputdata();
        }

        private InputData Getınputdata()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").data;
        }

        private void OnEnable()
        {
            subscribeevent();
        }

        private  void subscribeevent()
        {
            CoreGameSignals.Instance.OnReset += OnReset;
            InputSignals.Instance.OnenableInput += OnEnableInput;
            InputSignals.Instance.OndisableInput += OndisableInput;
        }

       
        private  void OndisableInput()
        {
            _isavailablefortouch = false;
          
        }

        private void OnEnableInput()
        {
            _isavailablefortouch = true;
        }


        private  void OnReset()
        {
            _isavailablefortouch = false;
           // _isfirstTimetouchtaken = false;
            _istouching = false;
        }

        private void unsubscribeEvent()
        {
            CoreGameSignals.Instance.OnReset -= OnReset;
            InputSignals.Instance.OnenableInput -= OnEnableInput;
            InputSignals.Instance.OndisableInput -= OndisableInput;
        }

        private void OnDisable()
        {
            unsubscribeEvent();
        }

        private void Update()
        {
            if (!_isavailablefortouch) return;

            if (Input.GetMouseButtonUp(0)&& !IspointerOverUIElement())
            {
                _istouching = false;
                InputSignals.Instance.OnInputReleased?.Invoke();
            }

            if (Input.GetMouseButtonUp(0)&& !IspointerOverUIElement())
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
            if (Input.GetMouseButton(0)&& !IspointerOverUIElement())
            {
                if (_istouching)
                {
                    if (_Mouseposition!=null)
                    {
                        Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _Mouseposition.Value;
                        if (mouseDeltaPos.x>_data.HorizontalInputSpeed)
                        {
                            _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        }
                        else if (mouseDeltaPos.x<_data.HorizontalInputSpeed)
                        {
                            _moveVector.x = -_data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
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

        private bool IspointerOverUIElement()
        {
            var eventdata = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventdata,results);
            return results.Count > 0;
        }

    }
}