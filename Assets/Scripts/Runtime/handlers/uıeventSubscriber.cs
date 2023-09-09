using System;
using Runtime.Enums;
using Runtime.manager;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.handlers
{
    public class uıeventSubscriber : MonoBehaviour
    {
        #region MyRegion

        [SerializeField] private UIEventsubscriptonTypes type;
        [SerializeField] private Button button;


        private UIManager _manager;


        #endregion

        private void Awake()
        {
            getreferences();
        }

        private  void getreferences()
        {
            _manager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private  void SubscribeEvent()
        {
            switch (type)
            {
                case UIEventsubscriptonTypes.Play:
                    button.onClick.AddListener(_manager.play);
                    break;
                case UIEventsubscriptonTypes.NextLevel:
                    button.onClick.AddListener(_manager.NextLevel);
                    break;
                case UIEventsubscriptonTypes.RestartLevel:
                    button.onClick.AddListener(_manager.restartLevel);
                    break;
            }
        }
        private  void UnSubscribeEvent()
        {
            switch (type)
            {
                case UIEventsubscriptonTypes.Play:
                    button.onClick.RemoveListener(_manager.play);
                    break;
                case UIEventsubscriptonTypes.NextLevel:
                    button.onClick.RemoveListener(_manager.NextLevel);
                    break;
                case UIEventsubscriptonTypes.RestartLevel:
                    button.onClick.RemoveListener(_manager.restartLevel);
                    break;
            }
        }
    }
}