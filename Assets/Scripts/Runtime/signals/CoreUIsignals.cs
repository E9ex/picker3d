using System;
using Runtime.Enums;
using Runtime.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace signals
{
    public class CoreUIsignals : MonoBehaviour
    {
        #region singleton

        public static CoreUIsignals Instance;

        private void Awake()
        {
            if (Instance!=null&& Instance!=this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
        }

        #endregion
        
        
        public UnityAction<UIPanelTypes,int >onOpenPanel=delegate {  };
        public UnityAction<int> onClosePanel=delegate {  };
        public UnityAction onCloseAllPanels=delegate {  };
    }
}