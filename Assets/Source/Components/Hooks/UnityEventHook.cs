using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.Hooks
{
    /// <summary>
    /// Kind of a hack.  Used so that you can call any unity event from an animation event. 
    /// </summary>
    public class UnityEventHook : MonoBehaviour
    {
        [SerializeField]
        private List<EventItem> unityEvents;
        
        public void TriggerEventById(string id)
        {
            if (unityEvents.Any(e => string.Equals(id, e.id, StringComparison.OrdinalIgnoreCase)))
            {
                var ev = unityEvents
                    .First(e => string.Equals(id, e.id, StringComparison.OrdinalIgnoreCase));
                
                ev.unityEvent.Invoke();
            }
            else
            {
                throw new UnityException($"UnityEventHook: Event with id {id} does not exist on game object {gameObject.name}!!!!!");
            }

            

        
            
        }

        [Serializable]
        public struct EventItem
        {
            public string id;
            public UnityEvent unityEvent;
        }

    }
}