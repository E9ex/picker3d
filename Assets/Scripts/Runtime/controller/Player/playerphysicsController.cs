using System;
using DG.Tweening;
using Runtime.controller.Pool;
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

        private const string finish = "finish";
        private const string stagearea = "stageArea";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tag: stagearea))
            {
                _manager.forcecommand.execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.OndisableInput?.Invoke();
                DOVirtual.DelayedCall(3, () =>
                {
                    var result = other.transform.parent.GetComponentInChildren<PoolController>()
                        .takeresults(_manager.stagevalue);
                    if (result)
                    {
                        CoreGameSignals.Instance.onStageAreaSuccesful?.Invoke(_manager.stagevalue);
                        InputSignals.Instance.OnenableInput?.Invoke();
                    }
                    else
                    {
                        CoreGameSignals.Instance.Onlevelfailed?.Invoke();
                    }
                });
                return;
            }

            if (other.CompareTag(finish))
            {
                CoreGameSignals.Instance.onFinishAreadEntered?.Invoke();
                InputSignals.Instance.OndisableInput?.Invoke();
                CoreGameSignals.Instance.OnlevelSuccesful?.Invoke();
                return;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var transform1 = _manager.transform;
            var position1 = transform1.position;
            
            Gizmos.DrawSphere(new Vector3(position1.x,position1.y+1f,position1.z+1f),1.35f);
        }

        public void Onreset()
        {
            
        }
    }
}