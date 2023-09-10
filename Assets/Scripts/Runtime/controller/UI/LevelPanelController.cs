using System;
using System.Collections.Generic;
using DG.Tweening;
using signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.controller.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        [SerializeField] private List<Image> stageImages = new List<Image>();
        [SerializeField] private List<TextMeshProUGUI> leveltexts = new List<TextMeshProUGUI>();


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private  void SubscribeEvents()
        {
            UISignals.Instance.onSetLevelValue += OnSetLevelValue;
            UISignals.Instance.OnsetStagecolor += OnsetStagecolor;
        }

        private void OnsetStagecolor(byte stageValue)
        {
            stageImages[stageValue].DOColor(new Color(0.9960785f,0.4196079f,0.07843139f), .5f);
        }
        private void OnSetLevelValue(byte? levelValue)
        {
            var additionalvalue  = ++levelValue; 
            leveltexts[0].text = additionalvalue.ToString();
            additionalvalue++;
            leveltexts[1].text = additionalvalue.ToString();
        }
        private  void UnSubscribeEvents()
        {
            UISignals.Instance.onSetLevelValue -= OnSetLevelValue;
            UISignals.Instance.OnsetStagecolor -= OnsetStagecolor;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}