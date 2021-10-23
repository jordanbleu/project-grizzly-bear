
using Assets.Source.Math;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Source.Components.ActorControllers
{
    /// <summary>
    /// The player controller takes controls from user input and applies them to the character
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        private const int JUMP_FORCE = 10;
        private const float HORIZONTAL_ACCELERATION = 1;
        private const float HORIZONTAL_DECELERATION = 0.25f;
        private const float MAX_SPEED = 8;

        [SerializeField]
        private Rigidbody2D rigidBody;

        // How fast the player is moving themselves
        private float horizontalInput = 0f;
        [SerializeField]
        private float horizontalSpeed = 0f;

        private void Update()
        {
            // Accelerate
            horizontalSpeed += (horizontalInput * HORIZONTAL_ACCELERATION);
            
            // Decelerate
            horizontalSpeed = horizontalSpeed.Stabilize(HORIZONTAL_DECELERATION, 0);

            // Clamp Max Speed
            horizontalSpeed = Mathf.Clamp(horizontalSpeed, -MAX_SPEED, MAX_SPEED);

            rigidBody.velocity = CombineVelocities();
        }

        // Combines all velocities into one final velocity
        private Vector2 CombineVelocities()
        {
            return new Vector2(horizontalSpeed, rigidBody.velocity.y);
        }

        #region Input Callbacks - invoked via PlayerInput component.
        private void OnJump(InputValue inputValue)
        {
            rigidBody.AddForce(new Vector2(0, JUMP_FORCE), ForceMode2D.Impulse);            
        }

        private void OnMove(InputValue inputValue)
        {
            horizontalInput = inputValue.Get<Vector2>().x;
        }
        #endregion
    }
}
