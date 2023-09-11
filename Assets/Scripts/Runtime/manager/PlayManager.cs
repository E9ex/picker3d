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
        public  byte stagevalue;
        internal forceballstopoolcommand forcecommand;

        [SerializeField] public playermovementcontroller _playermovementcontroller;
        [SerializeField] public playermeshcontroller _playermeshcontroller;
        [SerializeField] public playerphysicsController _playerphysicsController;

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

        private void SubscribeEvents()
        {
            InputSignals.Instance.OnInputTaken += OnInputTaken;
            InputSignals.Instance.OnInputReleased += OnInputReleased;
            InputSignals.Instance.OnInputDragged += OnInputDragged;
            UISignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.OnlevelSuccesful += OnlevelSuccesful;
            CoreGameSignals.Instance.Onlevelfailed += Onlevelfailed;
            CoreGameSignals.Instance.onStageAreaEntered += OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccesful += OnStageAreaSuccesful;
            CoreGameSignals.Instance.onFinishAreadEntered += OnFinishAreadEntered;
            CoreGameSignals.Instance.OnReset += Onreset;
        }

        private void Onreset()
        {
            stagevalue = 0;
            _playermovementcontroller.Onreset();
            _playerphysicsController.Onreset();
            _playermeshcontroller.Onreset();
        }

        private void OnFinishAreadEntered()
        {
            CoreGameSignals.Instance.OnlevelSuccesful?.Invoke();
        }

        private void OnStageAreaSuccesful(byte value)
        {
            stagevalue = (byte)(value + 1);
        }

        private void OnStageAreaEntered()
        {
            _playermovementcontroller.IsreadyToPlay(false);
        }

        private void OnlevelSuccesful()
        {
            _playermovementcontroller.IsreadyToPlay(false);
        }

        private void Onlevelfailed()
        {
            _playermovementcontroller.IsreadyToPlay(false);
        }

        private void OnPlay()
        {
            _playermovementcontroller.IsreadyToPlay(true);
        }

        private void OnInputDragged(horizontalInputParams Inputparams)
        {
            _playermovementcontroller.updateInputParams(Inputparams);
        }

        private void OnInputTaken()
        {
            _playermovementcontroller.IsreadyTomove(false);
        }

        private void OnInputReleased()
        {
            _playermovementcontroller.IsreadyTomove(true);
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.OnInputTaken -= OnInputTaken;
            InputSignals.Instance.OnInputReleased -= OnInputReleased;
            InputSignals.Instance.OnInputDragged -= OnInputDragged;
            UISignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.OnlevelSuccesful -= OnlevelSuccesful;
            CoreGameSignals.Instance.Onlevelfailed -= Onlevelfailed;
            CoreGameSignals.Instance.onStageAreaEntered -= OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccesful -= OnStageAreaSuccesful;
            CoreGameSignals.Instance.onFinishAreadEntered -= OnFinishAreadEntered;
            CoreGameSignals.Instance.OnReset -= Onreset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}
