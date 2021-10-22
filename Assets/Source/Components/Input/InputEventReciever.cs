using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Source.Components.Input
{

    /// <summary>
    /// This component adds a level of abstraction between the code that handles user inputs and the code that responds to said inputs.  
    /// This should be used when responding to user controls as it is input system agnostic.
    /// </summary>
    public class InputEventReciever : MonoBehaviour
    {



        public void OnJump(InputValue inputValue) {
            UnityEngine.Debug.Log(inputValue);
        }

        ///// <summary>
        ///// Value between -1 and 1 that represents the current requested horizontal movement speed.
        ///// This will change depending on how much the analog stick is held
        ///// </summary>
        //public float RequestedHorizontalMovementSpeed { get; set; } = 0f;

        //public void OnRequestMove(InputAction.CallbackContext context) {
        //    UnityEngine.Debug.Log("hello");
        //    RequestedHorizontalMovementSpeed = context.ReadValue<Vector2>().x;
        //}

    }
}
