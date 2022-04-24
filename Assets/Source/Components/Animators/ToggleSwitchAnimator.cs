using UnityEngine;

namespace Assets.Source.Components.Animators
{
    /// <summary>
    /// This component is meant for a toggle switch with a basic animator controller.  The 
    /// only thing the animator controller needs is a bool parameter called "is-on".  
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class ToggleSwitchAnimator : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void AnimateToggle(bool enabled) {
            
            var isCurrentlyEnabled = animator.GetBool("is-on");

            if (isCurrentlyEnabled != enabled) {
                animator.SetBool("is-on", enabled);
            }

        } 



    }
}
