using Runtime.Data.ValueObjects;
using Runtime.manager;
using UnityEditor.Experimental.GraphView;

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
            
        }
    }
}