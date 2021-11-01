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

        [SerializeField]
        private SkeletonMecanim skeleton;


        [SerializeField]
        private CapsuleCollider2D attachedCollider;

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
            playerAnimator.IsGrounded = groundDetector.IsGrounded;

            if (HorizontalInput != 0) { 
                playerAnimator.Direction = HorizontalInput;
            }

            playerAnimator.HorizontalSpeed = HorizontalInput;

            var skeletonRot = skeleton.gameObject.transform.rotation;

            if (groundDetector.GroundNormal.x > 0)
            {
                // on a slope going down facing left
                if (playerAnimator.IsFlipped)
                {
                    attachedCollider.direction = CapsuleDirection2D.Horizontal;
                    skeleton.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
                }
                // on a slope going down facing right
                else
                {
                    attachedCollider.direction = CapsuleDirection2D.Horizontal;
                    skeleton.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
                }
            }
            else if (groundDetector.GroundNormal.x < 0)
            {
                // slope going upwards, facing left
                if (playerAnimator.IsFlipped)
                {
                    attachedCollider.direction = CapsuleDirection2D.Horizontal;
                    skeleton.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                }
                // slope going upwards facing right
                else
                {
                    attachedCollider.direction = CapsuleDirection2D.Horizontal;
                    skeleton.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                }
            }
            else
            {
                attachedCollider.direction = CapsuleDirection2D.Vertical;
                skeleton.gameObject.transform.rotation = new Quaternion(0, 0, 0, skeletonRot.w);
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
                playerAnimator.Jump();
            }
        }

        private void OnMove(InputValue inputValue)
        {
            HorizontalInput = inputValue.Get<Vector2>().x;
        }

        private void OnPickup(InputValue inputValue) 
        {
            playerAnimator.Pickup();                        
        }
        #endregion
    }
}
