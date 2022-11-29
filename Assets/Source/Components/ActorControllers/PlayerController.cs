using Assets.Editor.Attributes;
using Assets.Source.Components.ActorControllers.Interfaces;
using Assets.Source.Components.Animators;
using Assets.Source.Components.Audio;
using Assets.Source.Components.Input;
using Assets.Source.Components.Items;
using Assets.Source.Components.Physics;
using Assets.Source.Components.Switches;
using Assets.Source.Math;
using Assets.Source.Unity;
using Spine.Unity;
using System;
using System.Linq;
using Assets.Source.Components.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Source.Components.ActorControllers
{
    /// <summary>
    /// The player controller takes controls from user input and applies them to the character
    /// </summary>
    public class PlayerController : MonoBehaviour, IActorController
    {
        // How high the player jumps
        private const int JUMP_FORCE = 50;
        // How quickly the player accelerates to max speed
        private const float HORIZONTAL_ACCELERATION = 1;
        // How quickly the player decelerates to zero
        private const float HORIZONTAL_DECELERATION = 0.25f;
        // the player's maximum movement speed
        private const float MAX_SPEED = 6;
        // the rate at which engine audio rises
        private const float ENGINE_REV_SPEED = 0.1f;

        private Dictionary<int, Vector2> externalEffectors = new Dictionary<int, Vector2>();

        [SerializeField]
        private InputReceiverHook[] inputReceivers;

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
        private PlayerInteractionTrigger interactionTrigger;

        [SerializeField]
        private CapsuleCollider2D attachedCollider;

        [SerializeField]
        private AcceleratedAudioPlayer engineAudio;

        [SerializeField]
        private AnimatorTriggerHook jumpAnimatorHook;

        [SerializeField]
        private PlayerSoundEffects soundEffects;

        [SerializeField]
        private PlayerIconAnimator playerIconAnimator;

        [SerializeField]
        private PlayerResetterController playerResetter;
        
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
            UpdateEngineAudio();
            UpdateAnimations();
            UpdatePlayerIcon();
        }

        private void UpdatePlayerIcon()
        {
            if (interactionTrigger.InteractibleItems.Any())
            {
                playerIconAnimator.ShowButtonPrompt = true;
            }
            else
            {
                playerIconAnimator.ShowButtonPrompt = false;
            }

            if (!UnityUtils.Exists(carriedItem))
            {
                if (interactionTrigger.CarryableItems.Any())
                {
                    playerIconAnimator.ShowPickupPrompt = true;
                }
                else
                {
                    playerIconAnimator.ShowPickupPrompt = false;
                }
            }
            else 
            {
                playerIconAnimator.ShowPickupPrompt = false;
            }            

        }

        private void UpdateEngineAudio()
        {
            var absSpeed = Mathf.Abs(HorizontalSpeed);

            if (groundDetector.IsGrounded)
            {

                if (absSpeed > 0f)
                {
                    if (engineAudio.AudioVelocity < 1f)
                    {
                        engineAudio.AudioVelocity += ENGINE_REV_SPEED;
                    }

                    engineAudio.AudioVelocity = engineAudio.AudioVelocity.Snap(ENGINE_REV_SPEED, 1f);

                }
                else
                {
                    if (engineAudio.AudioVelocity > 0f)
                    {
                        engineAudio.AudioVelocity -= ENGINE_REV_SPEED;
                    }
                    engineAudio.AudioVelocity = engineAudio.AudioVelocity.Snap(ENGINE_REV_SPEED, 0f);
                }

                engineAudio.AudioVelocity = Mathf.Clamp(engineAudio.AudioVelocity, 0, 1f);
            }
            else {
                // if we are not grounded, engine is at full blast or zero because we have no ground resistance
                if (absSpeed > 0f)
                {
                    engineAudio.AudioVelocity = 1.25f;
                }
                else
                {
                    engineAudio.AudioVelocity = 0;
                }
            }

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
            var externalForceVelocityX = externalEffectors.Sum(kvp => kvp.Value.x);
            var externalForceVelocityY = externalEffectors.Sum(kvp => kvp.Value.y);
            return new Vector2(HorizontalSpeed + externalForceVelocityX, rigidBody.velocity.y + externalForceVelocityY);
        }

        // an overly complicated way to calculate throw strength.
        // these are literally just random numbers that happened to work.
        // for best results movables should have rigid body mass between 1 and 10 
        // otherwise the player wont be able to throw the object far at all
        private Vector2 CalculateThrowSpeed() {
            var THROW_SPEED_MULTIPLIER = 2;

            var xs = (THROW_SPEED_MULTIPLIER * rigidBody.velocity.x) + (playerAnimator.Direction * 25);
            var ys = 25;

            return new Vector2(xs, ys);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent<RigidBodyVelocityEffector>(out var rbve))
            {
                var instid = rbve.GetInstanceID();
                if (externalEffectors.TryGetValue(rbve.GetInstanceID(), out _))
                {
                    externalEffectors[instid] = rbve.EffectVelocity;
                }
                else
                {
                    externalEffectors.Add(instid, rbve.EffectVelocity);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<RigidBodyVelocityEffector>(out var rbve))
            {
                var instid = rbve.GetInstanceID();
                if (externalEffectors.ContainsKey(instid))
                {
                    externalEffectors.Remove(instid);
                }
            }
        }

        #region Input Callbacks - invoked via PlayerInput component.

        private void OnReset()
        {
            if (!isMovementLocked)
            {
                soundEffects.PlayResetSound();
                playerResetter.TriggerReset();
            }
        }


        private void OnJump(InputValue inputValue)
        {
            if (!isMovementLocked)
            {
                if (groundDetector.IsGrounded)
                {

                    float carriedItemWeight = 0;
                    // carrying heavier items affects jump height
                    if (UnityUtils.Exists(carriedItem))
                    {
                        var carriedItemRigidBody = carriedItem.GetComponent<Rigidbody2D>();
                        carriedItemWeight = Mathf.Clamp(carriedItemRigidBody.mass / 2, 0f, 7f);
                    }

                    rigidBody.AddForce(new Vector2(0, JUMP_FORCE - carriedItemWeight), ForceMode2D.Impulse);
                    playerAnimator.Jump();
                    soundEffects.PlayJumpSound();
                    jumpAnimatorHook.SetAnimatorTrigger("jump");
                }
            }

            // broadcast to receiver hooks
            if (inputReceivers == null) return;
            
            foreach (var hook in inputReceivers)
            {
                if (UnityUtils.ActiveAndExists(hook) && UnityUtils.ActiveAndExists(hook.gameObject))
                {
                    hook.JumpButtonPressed();
                }
            }
        }

        private void OnMove(InputValue inputValue)
        {
            HorizontalInput = inputValue.Get<Vector2>().x;
        }

        private void OnPickup(InputValue inputValue)
        {
            if (isMovementLocked) return;

            // if the user presses the pickup button, we either pick up an
            // item or drop the current item.
            if (UnityUtils.Exists(carriedItem))
            {
                playerAnimator.PutDown();
                soundEffects.PlayPickupSound();
            }
            else if (interactionTrigger.CarryableItems.Any()) {
                soundEffects.PlayPickupSound();
                playerAnimator.Pickup();
            }
        }

        private void OnThrow(InputValue inputValue)
        {
            if (isMovementLocked) return;
            
            if (UnityUtils.Exists(carriedItem))
            {
                soundEffects.PlayThrowSound();
                playerAnimator.Throw();
            }
        }

        private void OnInteract(InputValue inputValue)
        {
            if (isMovementLocked) return;
            if (!interactionTrigger.InteractibleItems.Any()) return;
            
            // just pick the first one if there are multiple (there usually shouldn't be)
            var item = interactionTrigger.InteractibleItems.FirstOrDefault();

            if (UnityUtils.Exists(item) && item.TryGetComponent<IInteract>(out var interact)) {
                soundEffects.PlaySwitchSOund();
                interact?.OnInteract();
            }
        }

        #endregion

        public void ToggleMovementLock(bool isLocked) {

            HorizontalInput = 0;
            playerAnimator.HorizontalSpeed = 0;
            HorizontalSpeed = 0;
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
                var item = interactionTrigger.CarryableItems.FirstOrDefault();

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
