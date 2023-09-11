using System;
using System.Collections.Generic;
using Runtime.Enums;
using signals;
using UnityEngine;


    namespace Runtime.controller.UI
    {
        public class UIController : MonoBehaviour
        {
            [SerializeField] private List<Transform> layers = new List<Transform>();

            private void OnEnable()
            {
                SubscribeEvent();
            }

            private void SubscribeEvent()
            {
                CoreUIsignals.Instance.onClosePanel += OnClosePanel;
                CoreUIsignals.Instance.onOpenPanel += OnOpenPanel;
                CoreUIsignals.Instance.onCloseAllPanels += OnCloseAllPanels;
            }

            private void OnCloseAllPanels()
            {
                foreach (var layer in layers)
                {
                    if (layer.childCount > 0)
                    {
#if UNITY_EDITOR
                        DestroyImmediate(layer.GetChild(0).gameObject);
#else
                    Destroy(layer.GetChild(0).gameObject);
#endif
                    }
                }
            }

            private void OnOpenPanel(UIPanelTypes panelTypes, int value)
            {
                Instantiate(Resources.Load<GameObject>($"Screens/{panelTypes}Panel"), layers[value]);
            }

            private void OnClosePanel(int value)
            {
                if (layers.Count <= value || layers[value].childCount <= 0) return;

#if UNITY_EDITOR
                DestroyImmediate(layers[value].GetChild(0).gameObject);
#else
            Destroy(layers[value].GetChild(0).gameObject);
#endif
            }

            private void UnSubscribeEvent()
            {
                CoreUIsignals.Instance.onClosePanel -= OnClosePanel;
                CoreUIsignals.Instance.onOpenPanel -= OnOpenPanel;
                CoreUIsignals.Instance.onCloseAllPanels -= OnCloseAllPanels;
            }

            private void OnDisable()
            {
                UnSubscribeEvent();
            }
        }
    }
