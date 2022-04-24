using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.EventHooks
{
    public class DestroyEventHook : MonoBehaviour
    {
        /// <summary>
        /// Subscribe to this via code to subscribe to the destroy event.
        /// </summary>
        public UnityEvent<GameObject> onDestroy = new UnityEvent<GameObject>();


        public void OnDestroy()
        {
            onDestroy?.Invoke(gameObject);
        }


    }
}
