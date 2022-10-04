using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.EventHooks
{
    public class AwakeEventHook : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onAwake = new UnityEvent();

        private void Awake()
        {
            onAwake?.Invoke();
        }
    }
}