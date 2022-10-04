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

        [SerializeField]
        private AnimatorTriggerHook blackOutObject;

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

        public void EnableGlitchedFace() =>
            playerAware.Player.GetComponent<PlayerAnimator>().IsFaceGlitched = true;

        public void DisableGlitchedFace() =>
            playerAware.Player.GetComponent<PlayerAnimator>().IsFaceGlitched = false;

        public void EnableCutsceneMode() =>
            playerAware.GetComponent<CutscenePlayerController>().enabled = true;

        public void DisableCutsceneMode() =>
           playerAware.GetComponent<CutscenePlayerController>().enabled = false;

        public void EnablePlayerDeadAnimation() =>
            playerAware.Player.GetComponent<PlayerAnimator>().IsDead = true;

        public void DisablePlayerDeadAnimation() =>
            playerAware.Player.GetComponent<PlayerAnimator>().IsDead = false;

        public void FadeInfromBlack() =>
            blackOutObject.SetAnimatorTrigger("fade-in");

        public void FadeToBlack() =>
            blackOutObject.SetAnimatorTrigger("fade-out");


    }
}
