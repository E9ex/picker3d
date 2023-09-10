using System;
using UnityEngine;
using UnityEngine.Events;

namespace signals
{
    public class CoreGameSignals : MonoBehaviour
    {
        #region Singelton

        public static CoreGameSignals Instance;
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
       
        public UnityAction<byte>OnLevelInitialize= delegate {  };
        public UnityAction OnClearActiveLevel= delegate {  };
        public UnityAction OnNextlevel= delegate {  };
        public UnityAction OnlevelSuccesful=delegate {  };
        public UnityAction Onlevelfailed =delegate {  };
        public UnityAction OnRestartlevel= delegate {  };
        public UnityAction OnReset=delegate {  };
        public Func<byte>OnGetLevelValue= delegate { return 0;};
        public UnityAction onStageAreaEntered=delegate {  };
        public UnityAction <byte>onStageAreaSuccesful=delegate {  };
        public UnityAction onFinishAreadEntered=delegate {  };

    }
}