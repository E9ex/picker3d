using UnityEditor.iOS;
using UnityEngine;

namespace commands.Level
{
    public class Onlevelloadercommand
    {
        private Transform _levelholder;
        public Onlevelloadercommand(Transform levelholder)
        {
            _levelholder = levelholder;
        }

        public void execute(byte levelIndex)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefab/LevelPrefabs/Level {levelIndex}"));
        }
    }
}