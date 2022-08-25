using UnityEngine;
using System.Collections;


namespace Assets.Source.Components.ActorControllers
{
	/// <summary>
    /// Hook for cutscene controller to control the player object.  Activating this will
    /// automatically disable the normal player controller, and disabling this will re-enable the
    /// player controller.
    /// 
    /// </summary>
	public class CutscenePlayerController : MonoBehaviour
	{

        private PlayerController playerController;

        void Awake()
		{
            playerController = GetComponent<PlayerController>();
		}

        private void OnEnable() => playerController.enabled = false;
        private void OnDisable() => playerController.enabled = true;

        void Update()
		{
            // todo: this doesnt do anything yet and should be removed or something should be done in this class.

		}
	}
}

