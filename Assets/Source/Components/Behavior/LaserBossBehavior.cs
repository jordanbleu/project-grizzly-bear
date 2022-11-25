using System;
using Assets.Source.Components.Finders;
using Assets.Source.Math;
using UnityEngine;

namespace Assets.Source.Components.Behavior
{
    [RequireComponent(typeof(PlayerAware))]
    public class LaserBossBehavior : MonoBehaviour
    {
        [SerializeField]
        private float speed = 2f;

        [SerializeField]
        private float threshold = 1f;

        [SerializeField]
        [Range(0, 100)]
        private int lifterFrequency = 25;

        [SerializeField]
        private GameObject bombTemplate;
        
        private Rigidbody2D rigidBody;
        private PlayerAware playerAware;
        private Animator animator;
        private State state = State.Chase;
        
        private enum State
        {
            Chase,
            Lift,
            Disabled
        }

        private void Start()
        {
            playerAware = GetComponent<PlayerAware>();
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (state == State.Chase)
            {
                // chase the player 
                var playerX = playerAware.Player.transform.position.x;
                
                if (!playerX.IsWithin(threshold, transform.position.x))
                {
                    if (playerX > transform.position.x)
                    {
                        rigidBody.velocity = new Vector2(speed, 0);
                    }
                    else if (playerX < transform.position.x)
                    {
                        rigidBody.velocity = new Vector2(-speed, 0);
                    }
                }
                else
                {
                    rigidBody.velocity = Vector2.zero;
                }

                var roll = UnityEngine.Random.Range(0, 3000);

                if (roll < lifterFrequency)
                {
                    // transition to lift state
                    state = State.Lift;
                    animator.SetTrigger("trigger-lifter");
                }

            }
            else if (state == State.Lift)
            {
                rigidBody.velocity = Vector2.zero;
            }

        }

        // called from animator
        public void SetStateToChase() => state = State.Chase;

        public void DropBomb()
        {
            var inst = Instantiate(bombTemplate, transform);
            inst.transform.position = transform.position;
        }

    }
}