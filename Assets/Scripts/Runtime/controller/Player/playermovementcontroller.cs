using System;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.controller.Player
{
    public class playermovementcontroller : MonoBehaviour
    {
        [SerializeField] private new Rigidbody _rigidbody;
        private playermovementData data;
        [SerializeField]private bool _isreadytoPlay,IsreadyToMove;

        private float xvalue;
        private float2 _clampValues;


        internal void setdata(playermovementData _data)
        {
            data = _data;
        }

        private void FixedUpdate()
        {
            if (!_isreadytoPlay)
            {
                stopPlayer();
                return;
            }

            if (_isreadytoPlay)
            {
                MovePlayer();
            }
            else
            {
                StopPlayerHorizontally();
            }
        }

        private  void MovePlayer()
        {
            var velocity = _rigidbody.velocity;
            velocity = new float3(xvalue*data.sidewayspeed,velocity.y,data.forwardSpeed);
            _rigidbody.velocity = velocity;
            var position = _rigidbody.position;
            position = new Vector3(Mathf.Clamp(_rigidbody.position.x,_clampValues.x,_clampValues.y),(position=_rigidbody.position).y,position.z);
            _rigidbody.position = position;
        }

        private  void StopPlayerHorizontally()
        {
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, data.forwardSpeed);
            _rigidbody.angularVelocity = Vector3.zero;
            
        }

        private  void stopPlayer()
        {
            _rigidbody.velocity=Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        internal void IsreadyToPlay(bool condition)
        {
            _isreadytoPlay = condition; 
        }

        internal void IsreadyTomove(bool condition)
        {
            IsreadyToMove = condition;
        }

        internal void updateInputParams(horizontalInputParams inputParams)
        {
            xvalue = inputParams.horizontalValue;
            _clampValues = inputParams.clampValues;
        }

        internal void Onreset()
        {
            stopPlayer();
            IsreadyToMove = false;
            _isreadytoPlay = false;
        }
    }
}