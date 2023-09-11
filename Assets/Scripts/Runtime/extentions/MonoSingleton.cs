using System;
using UnityEngine;

namespace Runtime.extentions
{
    public class MonoSingleton<t> : MonoBehaviour where t: Component
    {

        private static t instance;

        public static t Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = FindObjectOfType<t>(); 
                    // var instances = FindObjectsOfType<t>();
                    // for (int i = 0; i < instances.Length-1; i++)
                    // {
                    //     Destroy(instances[i]);
                    //     
                    // }
                    if (instance==null)
                    {
                        GameObject newgameObject = new GameObject(typeof(t).Name);
                        instance = newgameObject.AddComponent<t>();

                    }
                }

                return instance;
            }
        }

        protected void Awake()
        {
            instance = this as t;
        }
    }
}