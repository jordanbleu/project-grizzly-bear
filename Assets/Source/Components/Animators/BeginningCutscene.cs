using Assets.Source.Components.ActorControllers;
using Assets.Source.Components.Finders;
using Assets.Source.Components.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.Animators
{
	/// <summary>
    /// A mostly useless class but its just how it's gotta be.
    /// </summary>
    public class BeginningCutscene : MonoBehaviour
	{
		private PlayerAware playerAware;

		[SerializeField]
		private GameObject menuObject;

		[SerializeField]
		private UnityEvent onCutsceneEnded = new UnityEvent();

		private GameObjectUtilities gameObjectUtilities;

        private void Start()
        {
			playerAware = GetComponent<PlayerAware>();
			gameObjectUtilities = GetComponent<GameObjectUtilities>();
        }

		public void UpdatePlayerAnimation() {
			var anim = playerAware.Player.GetComponent<PlayerAnimator>();
			// show the player falling down animation
			anim.IsDead = false;
			// update face to show the actual face
			anim.IsFaceGlitched = false;
			
		}

        public void ShowMenu()
		{
			menuObject.GetComponent<AnimatorTriggerHook>().SetAnimatorTrigger("show");
		}

		public void EndCutscene() {
			onCutsceneEnded?.Invoke();
			Destroy(menuObject);
			playerAware.Player.GetComponent<PlayerController>().ToggleMovementLock(false);
			// Deactivate self instead of kill self because destroying
            // cameras causes glitches in cinemachine
			gameObjectUtilities.DeactivateSelf();
		}


	}

}