using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.Switches
{
    public class Button : MonoBehaviour, IInteract
    {
        private bool wasPressed = false;
        private Animator animator;

        [SerializeField]
        private UnityEvent onPress = new UnityEvent();

        [SerializeField]
        private UnityEvent onPressFirstTime = new UnityEvent();

        [SerializeField] private bool isEnabled = true;
        
        private void Start()
        {
            animator = GetComponent<Animator>(); 
        }

        public void OnInteract()
        {
            // play the press animation even if its disabled.
            animator.SetTrigger("press");
            
            if (!isEnabled) return;
            
            onPress?.Invoke();

            if (wasPressed) return;
            
            wasPressed = true;
            onPressFirstTime?.Invoke();
        }

        public void SetEnabled(bool enabled) => isEnabled = enabled;
    }
}
