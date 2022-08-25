using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class TestingHackBase : MonoBehaviour
    {
        private void Start()
        {
            if (Application.isEditor)
            {
                Apply();
            }
            else
            {
                Debug.LogWarning("Some hacks were left enabled for this build but we ignored them.");
            }
        }

        /// <summary>
        /// Apply the hack.  This happens on Start()
        /// </summary>
        protected abstract void Apply();

    }
}