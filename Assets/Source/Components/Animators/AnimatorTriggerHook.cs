using Assets.Source.Unity;
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

        public void SetAnimatorTrigger(string triggerName) { if (UnityUtils.Exists(animator)) animator.SetTrigger(triggerName); }

        public void EnableAnimatorBool(string parameterName) { if (UnityUtils.Exists(animator)) animator.SetBool(parameterName, true); }

        public void DisableAnimatorBool(string parameterName) { if (UnityUtils.Exists(animator)) animator.SetBool(parameterName, false); }

        public void SetInt(string parameterName, int value)
            => animator.SetInteger(parameterName, value);


    }
}
