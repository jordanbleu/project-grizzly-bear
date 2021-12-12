using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.Switches
{
    public class ToggleSwitch : MonoBehaviour
    {
        
        [SerializeField]
        private UnityEvent onToggleOn = new UnityEvent();

        [SerializeField]
        private UnityEvent onToggleOff = new UnityEvent();

        public void Toggle(bool isOn) {

            if (isOn) {
                onToggleOn?.Invoke();
            } else { 
                onToggleOff?.Invoke();            
            }

        }

    }
}
