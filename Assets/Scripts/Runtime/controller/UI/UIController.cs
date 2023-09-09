using System;
using System.Collections.Generic;
using Runtime.Enums;
using signals;
using UnityEngine;

namespace Runtime.controller.UI
{
    public class UIController : MonoBehaviour
    {
       [SerializeField] private static List<Transform> layers = new List<Transform>();

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private static void SubscribeEvent()
        {
            CoreUIsignals.Instance.onClosePanel += OnClosePanel;
            CoreUIsignals.Instance.onOpenPanel += OnOpenPanel;
            CoreUIsignals.Instance.onCloseAllPanels += onCloseallpanels;
        }

        private static void onCloseallpanels()
        {
            foreach (var layer in layers)
            {
                if (layer.childCount > 0) return;
                
#if UNITY_EDITOR
                    DestroyImmediate(layer.GetChild(0).gameObject);
#else
                    Destroy(layer.GetChild(0).gameObject);
#endif                    
                
            }
        }

        private static void OnOpenPanel(UIPanelTypes panelTypes, int value)
        {
            Instantiate(Resources.Load<GameObject>($"Screens/{panelTypes}Panel"),layers[value]);
        }
         
        private static void OnClosePanel(int value)
        {
            if (layers[value].childCount <= 0) return;
            
#if UNITY_EDITOR
                DestroyImmediate(layers[value].gameObject);
#else
                Destroy(layers[value].gameObject);
#endif
            
        }
        private  void UnSubscribeEvent()
        {
            CoreUIsignals.Instance.onClosePanel -= OnClosePanel;
            CoreUIsignals.Instance.onOpenPanel -= OnOpenPanel;
            CoreUIsignals.Instance.onCloseAllPanels -= onCloseallpanels;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }
    }
    
}