using System;
using Runtime.Keys;
using Runtime.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace signals
{
    public class InputSignals : MonoBehaviour
    {
        #region singelton

        public static InputSignals Instance;

        private void Awake()
        {
            if (Instance!=null && Instance!=this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion
        
        public UnityAction onfirstTimeTouchTaken=delegate {  };
        public UnityAction OndisableInput=delegate {  };
        public UnityAction OnenableInput=delegate {  };
        public UnityAction OnInputTaken=delegate {  };
        public UnityAction OnInputReleased=delegate {  };
        public UnityAction<horizontalInputParams> OnInputDragged=delegate {  };
    }
}