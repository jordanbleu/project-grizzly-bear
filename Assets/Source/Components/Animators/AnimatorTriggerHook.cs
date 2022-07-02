using UnityEngine;

namespace Assets.Source.Components.Animators
{
    /// <summary>
    /// Bit of a hack that allows us to toggle animator triggers externally.
    /// </summary>
    public class AnimatorTriggerHook : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void SetAnimatorTrigger(string triggerName) => animator.SetTrigger(triggerName);

        public void EnableAnimatorBool(string parameterName) => animator.SetBool(parameterName, true);

        public void DisbaleAnimatorBool(string parameterName) => animator.SetBool(parameterName, false);

        public void SetInt(string parameterName, int value)
            => animator.SetInteger(parameterName, value);


    }
}
