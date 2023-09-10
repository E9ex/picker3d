using System;
using Runtime.manager;
using signals;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Runtime.controller.Player
{
    public class playerphysicsController : MonoBehaviour
    {
        [SerializeField] private PlayManager _manager;
        [SerializeField] private new Collider _collider;
        [SerializeField] private new Rigidbody _rigidbody;

        private const string finish = "stageArea";
        private const string stagearea = "stageArea";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tag: stagearea))
            {
                _manager.forcecommand.execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.OndisableInput?.Invoke();
            }

            if (other.CompareTag(finish))
            {
                CoreGameSignals.Instance.onFinishAreadEntered?.Invoke();
                InputSignals.Instance.OndisableInput?.Invoke();
                CoreGameSignals.Instance.OnlevelSuccesful?.Invoke();
                return;
            }
        }
    }
}