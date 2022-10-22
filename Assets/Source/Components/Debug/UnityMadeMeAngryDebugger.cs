using System;
using UnityEngine;

namespace Assets.Source.Components.Debug
{
    public class UnityMadeMeAngryDebugger : MonoBehaviour
    {

        public Vector3 localPosition;
        public Vector3 worldPosition;

        private void Update()
        {
            localPosition = transform.localPosition;
            worldPosition = transform.position;
        }
    }
}