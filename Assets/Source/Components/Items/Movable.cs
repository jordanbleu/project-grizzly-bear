﻿using Assets.Editor.Attributes;
using Assets.Source.Components.ActorControllers;
using Assets.Source.Components.Finders;
using Assets.Source.Unity;
using Spine.Unity;
using UnityEngine;

namespace Assets.Source.Components.Items
{
    [RequireComponent(typeof(Movable))]
    public class Movable : MonoBehaviour
    {
        private RigidbodyConstraints2D rigidBodyStartingConstraints;

        [ReadOnly]
        private float startingMass = 0f;

        [SerializeField]
        private SkeletonRenderer skeletonRenderer;

        private PlayerController player;

        private void Awake()
        {
            //
            // Create the bone follower component
            // We can't do this the normal way because there's a bug that causes issues
            // where the rock constantly teleports to the player in editor mode
            //
            boneFollower = gameObject.AddComponent<BoneFollower>();
            boneFollower.enabled = false;
            boneFollower.SkeletonRenderer = skeletonRenderer;
            boneFollower.followBoneRotation = true;
            boneFollower.followXYPosition = true;
            boneFollower.followZPosition = true;
            boneFollower.followSkeletonFlip = true;
        }

        private void Start()
        {

            if (!attachedCollider.enabled) {
                throw new UnityException("The attachedCollider on the movable component is not enabled.  Please enable it or drag the right one");
            }

            if (!UnityUtils.Exists(boneFollower) || !UnityUtils.Exists(boneFollower.SkeletonRenderer)) {
                throw new UnityException("!! Please assign the player skeleton to the bone follower please");
            }
            
            boneFollower.SetBone("ItemBone");
            rigidBodyStartingConstraints = rigidBody.constraints;
            startingMass = rigidBody.mass;

            var playerAware = GetComponent<PlayerAware>();
            player = playerAware.Player.GetComponent<PlayerController>();

            
        }

        private void Update()
        {
            // if this item is being held, negate all forces
            // affecting it. 
            if (boneFollower.enabled) {
                rigidBody.velocity = Vector2.zero;
            }
        }

        [SerializeField]
        private Rigidbody2D rigidBody;

        private BoneFollower boneFollower;

        [SerializeField]
        private Collider2D attachedCollider;
        
        /// <summary>
        /// Used to have player pick up the object in game.
        /// </summary>
        public void Carry() {
            boneFollower.boneName = "ItemBone";
            attachedCollider.enabled = false;
            boneFollower.enabled = true;
            rigidBody.gravityScale = 0;
            //rigidBody.mass = 0f;
            rigidBody.velocity = Vector3.zero;
            // When you pick up an item, its z position is always frozen.
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        /// <summary>
        /// Used to have player either drop or throw the objects in game
        /// </summary>
        /// <param name="velocity"></param>
        public void Drop(Vector2 velocity) {
            //rigidBody.mass = startingMass;
            // this is a hack to fix a bug in the bone follower component where even if
            // it's inactive it will still mess up the object position.
            boneFollower.boneName = string.Empty;
            attachedCollider.enabled = true;
            
            
            boneFollower.enabled = false;
            rigidBody.gravityScale = 1;
            // When you release an item, the rigid body constraints go back to what they were by default
            rigidBody.constraints = rigidBodyStartingConstraints;
            rigidBody.AddForce(velocity, ForceMode2D.Impulse);
        }

        /// <summary>
        /// Like drop, but without the gravity and force stuff.  Used for weird situations like if the object is 
        /// destroyed while holding it. 
        /// </summary>
        public void Uncarry()
        {

            // this is a hack to fix a bug in the bone follower component where even if
            // it's inactive it will still mess up the object position.
            boneFollower.boneName = string.Empty;
            boneFollower.enabled = false;
            rigidBody.gravityScale = 1;
            attachedCollider.enabled = true;
            // Force the player to drop the item.  Usually this would be done in the player controller.
            player.ReleaseCarriedItem();

            // When you release an item, the rigid body constraints go back to what they were by default
            rigidBody.constraints = rigidBodyStartingConstraints;
        }

        private void OnDestroy()
        {
            Uncarry();            
        }


    }
}
