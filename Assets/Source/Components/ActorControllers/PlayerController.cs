using Assets.Source.Components.ActorControllers.Interfaces;
using Assets.Source.Components.Animators;
using Assets.Source.Components.Physics;
using Assets.Source.Math;
using Spine.Unity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Source.Components.ActorControllers
{
    /// <summary>
    /// The player controller takes controls from user input and applies them to the character
    /// </summary>
    public class PlayerController : MonoBehaviour, IActorController
    {
        private const int JUMP_FORCE = 10;
        private const float HORIZONTAL_ACCELERATION = 1;
        private const float HORIZONTAL_DECELERATION = 0.25f;
        private const float MAX_SPEED = 8;

        [SerializeField]
        private Rigidbody2D rigidBody;

        [SerializeField]
        private PlayerAnimator playerAnimator;

        [SerializeField]
        private GroundDetector groundDetector;

        /// <summary> The horizontal input axis from the player </summary>
        public float HorizontalInput { get; set; } = 0f;

        public float HorizontalSpeed { get; set; } = 0f;

        private void Update()
        {
            HandleMovement();
            UpdateAnimations();
        }

        // Apply animations
        private void UpdateAnimations()
        {
            if (HorizontalInput != 0) { 
                playerAnimator.Direction = HorizontalInput;
            }

            playerAnimator.HorizontalSpeed = HorizontalInput;

            // todo:  The below logic works but we need a better way to handle collider rotation

            //
            // Figure out which slope to use based on the ground normal
            //
            // ...on a slope going upwards
            if (groundDetector.GroundNormal.x > 0)
            {
                // facing left
                if (playerAnimator.IsFlipped)
                {
                    //playerAnimator.SlopeAngle = PlayerAnimator.Slope.Down;
                }
                // facing right
                else
                {
                    //playerAnimator.SlopeAngle = PlayerAnimator.Slope.Up;
                }
            }
            // ...on a slope going downwards
            else if (groundDetector.GroundNormal.x < 0)
            {
                // facing left
                if (playerAnimator.IsFlipped)
                {
                    //playerAnimator.SlopeAngle = PlayerAnimator.Slope.Up;
                }
                // facing right
                else
                {
                    //playerAnimator.SlopeAngle = PlayerAnimator.Slope.Down;
                }
            }
            else {
                playerAnimator.SlopeAngle = PlayerAnimator.Slope.Flat;
            }
        }

        // Updates the user speed
        private void HandleMovement()
        {
            // Accelerate
            HorizontalSpeed += (HorizontalInput * HORIZONTAL_ACCELERATION);

            // Decelerate
            HorizontalSpeed = HorizontalSpeed.Stabilize(HORIZONTAL_DECELERATION, 0);

            // Clamp Max Speed
            HorizontalSpeed = Mathf.Clamp(HorizontalSpeed, -MAX_SPEED, MAX_SPEED);

            rigidBody.velocity = CombineVelocities();
        }

        // Combines all velocities into one final velocity
        private Vector2 CombineVelocities()
        {
            return new Vector2(HorizontalSpeed, rigidBody.velocity.y);
        }


        #region Input Callbacks - invoked via PlayerInput component.
        private void OnJump(InputValue inputValue)
        {
            if (groundDetector.IsGrounded) { 
                rigidBody.AddForce(new Vector2(0, JUMP_FORCE), ForceMode2D.Impulse);            
            }
        }

        private void OnMove(InputValue inputValue)
        {
            HorizontalInput = inputValue.Get<Vector2>().x;
        }
        #endregion
    }
}
