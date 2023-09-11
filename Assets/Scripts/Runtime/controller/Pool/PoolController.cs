using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using Runtime.Data.UnityObjects;
using signals;
using TMPro;

namespace Runtime.controller.Pool
{
    public class PoolController : MonoBehaviour
    {
       
        [SerializeField] private TextMeshPro pooltext;
        [SerializeField] private byte stageID;
        [SerializeField] private new Renderer _renderer;

        private PoolData _data;
        private byte _collectedcount;

        
        
    }
}