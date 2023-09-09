using System;
using UnityEngine;

namespace Data.ValueObjects
{
    [Serializable]
    public struct InputData
    {
        public float HorizontalInputSpeed;
        public Vector2 clampValues;
        public float ClampSpeed;

        
    }
}