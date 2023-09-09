using UnityEditor.iOS;
using UnityEngine;

namespace commands.Level
{
    public class Onlevelloadercommand
    {
        private Transform _levelholder;
        internal Onlevelloadercommand(Transform levelholder)
        {
            _levelholder = levelholder;
        }

        internal void execute(byte levelIndex)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefab/LevelPrefabs/Level {levelIndex}"));
        }
    }
}