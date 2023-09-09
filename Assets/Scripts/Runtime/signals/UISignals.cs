using System;
using UnityEngine;
using UnityEngine.Events;

namespace signals
{
    public class UISignals : MonoBehaviour
    {
        #region singleton

        public static UISignals Instance;

        private void Awake()
        {
            if (Instance!=null&&Instance!=this)
            {
               Destroy(gameObject);
               return;
            }

            Instance = this;
        }

        #endregion
        
        public UnityAction<byte>OnsetStagecolor=delegate {  };
        public UnityAction<byte?> onSetLevelValue=delegate {  };
        public UnityAction onPlay=delegate {  };
    }
}