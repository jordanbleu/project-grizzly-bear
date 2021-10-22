using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Source.Components.Debug
{
    public class InputLogger : MonoBehaviour
    {

        [SerializeField]
        private float xAxis = 0f;

        [SerializeField]
        private float yAxis = 0f;
        


        public void LogButtonInput(InputAction.CallbackContext context) {
            UnityEngine.Debug.Log($"Button {context.action.name} - phase {context.phase}");
        }

        public void LogAxisInput(InputAction.CallbackContext context) {
            xAxis = context.ReadValue<Vector2>().x;
            yAxis = context.ReadValue<Vector2>().y;

        }

    }
}
