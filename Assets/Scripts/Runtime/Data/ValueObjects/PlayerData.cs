using System;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public playermovementData PlayermovementData;
        public playermeshdata meshdata;
        public PlayerForceData ForceData;
    }

    [Serializable]
    public struct playermovementData
    {
        public float forwardSpeed;
        public float sidewayspeed;
    }

    [Serializable]
    public struct playermeshdata
    {
        public float scaleCounter;
    }

    [Serializable]
    public struct PlayerForceData
    {
        public float3 forceparameters; 
    }
}
