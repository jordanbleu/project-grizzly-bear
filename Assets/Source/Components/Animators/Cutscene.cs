using Assets.Source.Components.ActorControllers;
using Assets.Source.Components.Finders;
using UnityEngine;

namespace Assets.Source.Components.Animators
{
    [RequireComponent(typeof(PlayerAware))]
    public class Cutscene : MonoBehaviour
    {

        [SerializeField]
        private AnimatorTriggerHook cinematicBarObject;

        [SerializeField]
        private AnimatorTriggerHook whiteOutObject;

        private PlayerAware playerAware;
        private void Start()
        {
            playerAware = GetComponent<PlayerAware>();
        }

        public void EnableCinematicBars() =>
            cinematicBarObject.SetAnimatorTrigger("enable");

        public void DisableCinematicBars() =>
            cinematicBarObject.SetAnimatorTrigger("disable");

        public void LockPlayerMovement() =>
            playerAware.Player.GetComponent<PlayerController>().ToggleMovementLock(true);

        public void UnlockPlayerMovement() =>
            playerAware.Player.GetComponent<PlayerController>().ToggleMovementLock(false);

        public void TriggerShortWhiteOut() =>
            whiteOutObject.SetAnimatorTrigger("white-out-short");



    }
}
