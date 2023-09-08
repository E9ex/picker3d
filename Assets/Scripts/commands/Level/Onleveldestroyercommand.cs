using UnityEngine;

namespace commands.Level
{
    public class Onleveldestroyercommand
    {
        private Transform _levelholder;
        public Onleveldestroyercommand(Transform levelholder)
        {
            _levelholder = levelholder;
        }

        public void execute()
        {
            if (_levelholder.transform.childCount<=0)return;
            Object.Destroy(_levelholder.transform.GetChild(0).gameObject);
        }
    }
}