using Assets.Source.Components.Behavior;
using UnityEngine;

namespace Assets.Source.Components.UI
{
    public class PlayerResetterController : MonoBehaviour
    {
        private bool isAwake = false;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private GameObject deathScreenObject;
        
        // called from input handler
        public void TriggerReset()
        {
            if (!isAwake)
            {
                animator.SetTrigger("show-animation");
            }
            else
            {
                deathScreenObject.SetActive(true);
            }
        }

        // called from animator events
        public void SetAwakeTrue() => isAwake = true;
        public void SetAwakeFalse() => isAwake = false;

    }
}