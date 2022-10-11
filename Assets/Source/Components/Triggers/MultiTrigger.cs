using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

namespace Assets.Source.Components.Triggers
{
    public class MultiTrigger : MonoBehaviour
    {
        private bool wasEnabled = false;
        
        [SerializeField]
        private bool[] triggers;

        [SerializeField]
        [Tooltip("Triggered when a trigger is enabled and all triggers are active.")]
        private UnityEvent onAllTriggersEnabled = new UnityEvent();

        [SerializeField]
        [Tooltip("Triggered when all triggers are active for the first time.")]
        private UnityEvent onAllTriggersEnabledFirstTime = new UnityEvent();
        public void EnableTriggerById(int index)
        {
            if (index > triggers.Length-1)
            {
                throw new UnityException($"Index {index} is not valid.");
            }

            triggers[index] = true;
            Refresh();
        }

        public void DisableTriggerById(int index)
        {
            if (index > triggers.Length-1)
            {
                throw new UnityException($"Index {index} is not valid.");
            }

            triggers[index] = false;
            Refresh();
        }

        private void Refresh()
        {
            if (triggers.All(t => t))
            {
                onAllTriggersEnabled?.Invoke();
                if (!wasEnabled)
                {
                    wasEnabled = true;
                    onAllTriggersEnabledFirstTime?.Invoke();
                }
            }
        }
    }
}