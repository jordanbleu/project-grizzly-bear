using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Source.Components.Input
{
    public class InputEventReceiver : MonoBehaviour
    {

        // This doesn't work at all if I set the Player Input component behavior to "Broadcast" or "Send" messages
        private void OnJump(InputValue inputValue)
        {
            UnityEngine.Debug.Log(inputValue);
        }

        // If i set the Player Input component behavior to "Invoke Unity Events" this will only work for the gamepad, not keyboard
        public void Jump(InputAction.CallbackContext context) {
            UnityEngine.Debug.Log(context);
        }
    }
}
