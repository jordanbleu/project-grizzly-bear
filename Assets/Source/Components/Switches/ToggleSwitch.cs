using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.Switches
{
    public class ToggleSwitch : MonoBehaviour
    {

        private bool wasToggled = false;

        [SerializeField]
        private UnityEvent onToggleOn = new UnityEvent();

        [SerializeField]
        private UnityEvent onToggleOff = new UnityEvent();

        [SerializeField]
        private UnityEvent onToggleFirstTime = new UnityEvent();

        public void Toggle(bool isOn) {

            if (isOn) {

                if (!wasToggled) {
                    onToggleFirstTime?.Invoke();
                    wasToggled = true;
                }

                onToggleOn?.Invoke();
            } else { 
                onToggleOff?.Invoke();            
            }

        }

    }
}
