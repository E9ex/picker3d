using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.signals
{
    public class CameraSignals : MonoBehaviour
    {
        public static CameraSignals Instance;

        private void Awake()
        {
            if (Instance!=null && Instance!=this) 
                {
                    Destroy(gameObject);
                    return;
                }

                Instance = this;
            
        }
        public UnityAction onSetCameraTarget= delegate {  };
        
    }
}