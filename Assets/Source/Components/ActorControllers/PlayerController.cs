using Assets.Editor.Attributes;
using Assets.Source.Components.ActorControllers.Interfaces;
using Assets.Source.Components.Animators;
using Assets.Source.Components.Items;
using Assets.Source.Components.Physics;
using Assets.Source.Math;
using Assets.Source.Unity;
using Spine.Unity;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Source.Components.ActorControllers
{
    /// <summary>
    /// The player controller takes controls from user input and applies them to the character
    /// </summary>
    public class PlayerController : MonoBehaviour, IActorController
    {
        // How high the player jumps
        private const int JUMP_FORCE = 10;
        // How quickly the player accelerates to max speed
        private const float HORIZONTAL_ACCELERATION = 1;
        // How quickly the player decelerates to zero
        private const float HORIZONTAL_DECELERATION = 0.25f;
        // the player's maximum movement speed
        private const float MAX_SPEED = 8;        

        [SerializeField]
        private Rigidbody2D rigidBody;

        [SerializeField]
        private PlayerAnimator playerAnimator;


        [SerializeField]
        private Animator faceAnimator;

        [SerializeField]
        private GroundDetector groundDetector;

        [SerializeField]
        private SkeletonMecanim skeleton;

        [SerializeField]
        private PlayerPickupTrigger pickupTrigger;

        [SerializeField]
        private CapsuleCollider2D attachedCollider;

        private Destructible destructible;

        /// <summary> The horizontal input axis from the player </summary>
        public float HorizontalInput { get; set; } = 0f;

        public float HorizontalSpeed { get; set; } = 0f;

        [SerializeField]
        [ReadOnly]
        private GameObject carriedItem;

        private bool isMovementLocked = false;

        private void Awake()
        {
            carriedItem = null;
        }

        private void Start()
        {
            destructible = GetComponent<Destructible>(); 
        }


        private void Update()
        {
            HandleMovement();
            UpdateAnimations();
        }

        // Apply animations
        private void UpdateAnimations()
        {
            faceAnimator.SetFloat("health-percentage", ((float)destructible.Health / destructible.MaxHealth));

            playerAnimator.IsGrounded = groundDetector.IsGrounded;
            playerAnimator.IsHoldingItem = UnityUtils.Exists(carriedItem);

            if (!isMovementLocked) { 
                if (HorizontalInput != 0) { 
                    playerAnimator.Direction = HorizontalInput;
                }

                playerAnimator.HorizontalSpeed = HorizontalInput;
            }        

            var skeletonRot = skeleton.gameObject.transform.rotation;

            if (groundDetector.GroundNormal.x > 0.5f)
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
            else if (groundDetector.GroundNormal.x < -0.5f)
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
            if (!isMovementLocked)
            {
                // Accelerate
                HorizontalSpeed += (HorizontalInput * HORIZONTAL_ACCELERATION);

                // Decelerate
                HorizontalSpeed = HorizontalSpeed.Stabilize(HORIZONTAL_DECELERATION, 0);

                // Clamp Max Speed
                HorizontalSpeed = Mathf.Clamp(HorizontalSpeed, -MAX_SPEED, MAX_SPEED);

                rigidBody.velocity = CombineVelocities();
            }
            else {
                rigidBody.velocity = Vector2.zero;
            }
        }

        // Combines all velocities into one final velocity
        private Vector2 CombineVelocities()
        {
            return new Vector2(HorizontalSpeed, rigidBody.velocity.y);
        }

        // an overly complicated way to calculate throw strength
        private Vector2 CalculateThrowSpeed() {
            var THROW_SPEED_MULTIPLIER = 15;

            var xs = (THROW_SPEED_MULTIPLIER * rigidBody.velocity.x) + (playerAnimator.Direction * 100);
            var ys = 200;

            return new Vector2(xs, ys);
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
            // if the user presses the pickup button, we either pick up an
            // item or drop the current item.
            if (UnityUtils.Exists(carriedItem))
            {
                playerAnimator.PutDown();
            }
            else if (pickupTrigger.CurrentItems.Any()) { 
                playerAnimator.Pickup();
            }
        }

        private void OnThrow(InputValue inputValue) {
            if (UnityUtils.Exists(carriedItem))
            {
                playerAnimator.Throw();
            }
        }

        #endregion

        public void ToggleMovementLock(bool isLocked) {
            isMovementLocked = isLocked;
        }

        public void ReleaseCarriedItem() {
            if (UnityUtils.Exists(carriedItem)) {
                carriedItem = null;
            }
        }

        #region Animator Callbacks
        // The below callbacks are invoked via the PlayerAnimationHook

        public void AnimatorPickup() {
            // Confusingly enough, this animator event is called from the
            // pickup animation and also the put-down animation as well.

            if (UnityUtils.Exists(carriedItem))
            {
                var movable = carriedItem.GetComponent<Movable>();
                movable.Drop(Vector2.zero);
                ReleaseCarriedItem();
            }
            else { 
                var item = pickupTrigger.CurrentItems.FirstOrDefault();

                if (UnityUtils.Exists(item)) {
                
                    // player faces the item
                    if (item.transform.position.x < transform.position.x)
                    {
                        playerAnimator.Direction = -1;
                    }
                    else {
                        playerAnimator.Direction = 1;
                    }

                    carriedItem = item;
                    carriedItem.GetComponent<Movable>().Carry();    
                }
            }
        }

        public void AnimatorThrow() {
            if (UnityUtils.Exists(carriedItem)) { 
                var movable = carriedItem.GetComponent<Movable>();
                movable.Drop(CalculateThrowSpeed());
                ReleaseCarriedItem();
            }
        }


        #endregion
    }
}
