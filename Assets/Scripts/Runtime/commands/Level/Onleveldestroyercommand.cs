using UnityEngine;

namespace Runtime.commands.Level
{
    public class Onleveldestroyercommand
    {
        private Transform _levelholder;
        internal Onleveldestroyercommand(Transform levelholder)
        {
            _levelholder = levelholder;
        }

        internal void execute()
        {
            if (_levelholder.transform.childCount<=0)return;
            Object.Destroy(_levelholder.transform.GetChild(0).gameObject);
        }
    }
}