using System;
using Runtime.commands.Level;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using signals;
using UnityEditor.iOS;
using UnityEngine;

namespace manager
{
    public class levelmanager : MonoBehaviour
    {
        [SerializeField] private Transform levelholder;
        [SerializeField] private byte totallevelcount;
        private Onlevelloadercommand _levelloadercommand;
        private Onleveldestroyercommand _leveldestroyercommand;
            
        private short _currentlevel;
        private leveldata _leveldata;


        private void Awake()
        {
           _leveldata= getleveldata();
           _currentlevel= Getactivelevel();
           Init();
        }
        

        private void Init()
        {
            _levelloadercommand = new Onlevelloadercommand(levelholder);
            _leveldestroyercommand = new Onleveldestroyercommand(levelholder);
        }

        private byte Getactivelevel()
        {
            return (byte)_currentlevel;
        }

        private leveldata getleveldata()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[_currentlevel];
        }

        private void OnEnable()
        {
            subscribeEvent();
        }

        private  void subscribeEvent()
        {
            CoreGameSignals.Instance.OnLevelInitialize += _levelloadercommand.execute;
            CoreGameSignals.Instance.OnClearActiveLevel += _leveldestroyercommand.execute;
            CoreGameSignals.Instance.OnGetLevelValue += OnGetLevelValue;
            CoreGameSignals.Instance.OnNextlevel += OnNextlevel;
            CoreGameSignals.Instance.OnRestartlevel += OnRestartLevel;
        }

     

        private byte OnGetLevelValue()
        {
            return (byte)_currentlevel;
        }
        private  void UnsubscribeEvent()
        {
            CoreGameSignals.Instance.OnLevelInitialize -= _levelloadercommand.execute;
            CoreGameSignals.Instance.OnClearActiveLevel -= _leveldestroyercommand.execute;
            CoreGameSignals.Instance.OnGetLevelValue -= OnGetLevelValue;
            CoreGameSignals.Instance.OnNextlevel -= OnNextlevel;
            CoreGameSignals.Instance.OnRestartlevel -= OnRestartLevel;
        }

       


        private void OnDisable()=> UnsubscribeEvent();

        private void Start()
        {
            CoreGameSignals.Instance.OnLevelInitialize?.Invoke((byte)(_currentlevel%totallevelcount));
            //uısignals.
            CoreUIsignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.start,1);
        }

        private void OnNextlevel()
        {
            _currentlevel++;
            CoreGameSignals.Instance.OnClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.OnReset?.Invoke();
            CoreGameSignals.Instance.OnLevelInitialize?.Invoke((byte)(_currentlevel%totallevelcount));
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.OnClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.OnReset?.Invoke();
            CoreGameSignals.Instance.OnLevelInitialize?.Invoke((byte)(_currentlevel%totallevelcount));
        }
    }
}