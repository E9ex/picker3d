using System;
using Runtime.commands.Level.player;
using Runtime.controller.Player;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using signals;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.manager
{
    public class PlayManager : MonoBehaviour
    {
        public static byte stagevalue;
        internal forceballstopoolcommand forcecommand;

        [SerializeField] private static playermovementcontroller _playermovementcontroller;
        [SerializeField] private static playermeshcontroller _playermeshcontroller;
        [SerializeField] private static playerphysicsController _playerphysicsController;

        private PlayerData _data;

        private void Awake()
        {
            _data = Getplayerdata();
        }

        private PlayerData Getplayerdata()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").data;
        }

        void sendDataToControllers()
        {
            _playermovementcontroller.setdata(_data.PlayermovementData);
            _playermeshcontroller.setdata(_data.meshdata);
        }

        void Init()
        {
            forcecommand = new forceballstopoolcommand(this, _data.ForceData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private static void SubscribeEvents()
        {
            InputSignals.Instance.OnInputTaken += OnInputTaken;
            InputSignals.Instance.OnInputReleased += OnInputReleased;
            InputSignals.Instance.OnInputDragged += OnInputDragged;
            UISignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.OnlevelSuccesful += OnlevelSuccesful;
            CoreGameSignals.Instance.Onlevelfailed += Onlevelfailed;
            CoreGameSignals.Instance.onStageAreaEntered +=  OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccesful +=  OnStageAreaSuccesful;
            CoreGameSignals.Instance.onFinishAreadEntered += OnFinishAreadEntered;
            CoreGameSignals.Instance.OnReset += Onreset;
        }

        private static void Onreset()
        {
            stagevalue = 0;
            _playermovementcontroller.onreset();
            _playerphysicsController.onreset();
            _playermeshcontroller.Onreset();
        }

       

        private static void OnFinishAreadEntered()
        {
           CoreGameSignals.Instance.OnlevelSuccesful?.Invoke();
        }
        private static void OnStageAreaSuccesful(byte value)
        {
            stagevalue = (byte)++value;
        }
        private static void OnStageAreaEntered()
        {
            _playermovementcontroller._isreadytoPlay(false);
        }

        static void OnlevelSuccesful()
        {
            _playermovementcontroller._isreadytoPlay(false);
        }

        static void Onlevelfailed()
        {
            _playermovementcontroller._isreadytoPlay(false);
        }

        static void OnPlay()
        {
            _playermovementcontroller._isreadytoPlay(true);
        }

        private static void OnInputDragged(horizontalInputParams Inputparams)
        {
            _playermovementcontroller.updateInputParams(Inputparams);
        }

        static void OnInputTaken()
        {
            _playermovementcontroller.IsreadyToMove(false);
        }

        static void OnInputReleased()
        {
            _playermovementcontroller.IsreadyToMove(true);
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.OnInputTaken -= OnInputTaken;
            InputSignals.Instance.OnInputReleased -= OnInputReleased;
            InputSignals.Instance.OnInputDragged -= OnInputDragged;
            UISignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.OnlevelSuccesful -= OnlevelSuccesful;
            CoreGameSignals.Instance.Onlevelfailed -= Onlevelfailed;
            CoreGameSignals.Instance.onStageAreaEntered -=  OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccesful -=  OnStageAreaSuccesful;
            CoreGameSignals.Instance.onFinishAreadEntered -= OnFinishAreadEntered;
            CoreGameSignals.Instance.OnReset -= Onreset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}