using System;
using Runtime.Enums;
using signals;
using UnityEngine;

namespace Runtime.manager
{
    public class UIManager : MonoBehaviour
    {
        private void OnEnable()
        {
            subscribeEvent();
        }

        private  void subscribeEvent()
        {
            CoreGameSignals.Instance.OnLevelInitialize += OnlevelInitialize;
            CoreGameSignals.Instance.OnlevelSuccesful += OnlevelSuccesful;
            CoreGameSignals.Instance.Onlevelfailed += OnlevelFailed;
            CoreGameSignals.Instance.OnReset += OnReset;
        }

        private void OnlevelFailed()
        {
            CoreUIsignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.fail,2);
        }

        private void OnlevelSuccesful()
        {
            CoreUIsignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.win,2);
            
        }

        private void OnlevelInitialize(byte arg0)
        {
            CoreUIsignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.level,0);
            UISignals.Instance.onSetLevelValue?.Invoke(CoreGameSignals.Instance.OnGetLevelValue?.Invoke());
        }

        private  void OnReset()
        {
            CoreUIsignals.Instance.onCloseAllPanels?.Invoke();
            CoreUIsignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.start,1);
        }
        private  void UnsubscribeEvent()
        {
            CoreGameSignals.Instance.OnLevelInitialize -= OnlevelInitialize;
            CoreGameSignals.Instance.OnlevelSuccesful -= OnlevelSuccesful;
            CoreGameSignals.Instance.Onlevelfailed -= OnlevelFailed;
            CoreGameSignals.Instance.OnReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvent();
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.OnNextlevel?.Invoke();
        }

        public void restartLevel()
        {
            CoreGameSignals.Instance.OnRestartlevel?.Invoke();
        }

        public void play()
        {
            UISignals.Instance.onPlay?.Invoke();
            CoreUIsignals.Instance.onClosePanel?.Invoke(1);
            InputSignals.Instance.OnenableInput?.Invoke();
        }
    }
}