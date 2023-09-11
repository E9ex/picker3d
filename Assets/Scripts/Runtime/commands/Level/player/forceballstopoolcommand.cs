using System.Linq;
using Runtime.Data.ValueObjects;
using Runtime.manager;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Runtime.commands.Level.player
{
    public class forceballstopoolcommand
    {
        private PlayManager _manager;
        private PlayerForceData _forceData;
        public forceballstopoolcommand(PlayManager manager, PlayerForceData forceData)
        {
            _manager = manager;
            _forceData = forceData;
        }

        internal void execute()
        {
            var transform1 = _manager.transform;
            var position1 = transform1.position;
            var forcepos = new UnityEngine.Vector3(position1.x, position1.y+1f, position1.z + 1);
            var collider = Physics.OverlapSphere(forcepos, 1.35f);

            var collectablecolliderList = collider.Where(col => col.CompareTag("Collectable")).ToList();
            foreach (var col in collectablecolliderList)
            {
                if (col.GetComponents<Rigidbody>()==null)continue;
                Rigidbody rb;
                rb = col.GetComponent<Rigidbody>();
                rb.AddForce(new UnityEngine.Vector3(0,_forceData.forceparameters.y,_forceData.forceparameters.z),ForceMode.Impulse);
            }
            collectablecolliderList.Clear();;
        }
    }
}