using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.Input
{

	/// <summary>
    /// Called from the player controller.  This allows external objects (other than the player) to respond to user inputs.
    /// This is stupid but it gets around a limitation in Unity where only one object can have the player controller (in reality
    /// i'm just too lazy to do it correctly and this is easier).
    ///
    /// Each of these needs to be registered in the player controller via the inspector. 
    /// </summary>
	public class InputReceiverHook : MonoBehaviour
	{

        [SerializeField]
        private UnityEvent onJumpButtonPressed;

        public void JumpButtonPressed() => onJumpButtonPressed?.Invoke();

	}

}
