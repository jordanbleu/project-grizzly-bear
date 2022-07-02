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

        private void Start()
        {
            animator = GetComponent<Animator>(); 
        }

        public void OnInteract()
        {
            animator.SetTrigger("press");
            onPress?.Invoke();

            if (!wasPressed) { 
                wasPressed = true;
                onPressFirstTime?.Invoke();
            }
        }
    }
}
