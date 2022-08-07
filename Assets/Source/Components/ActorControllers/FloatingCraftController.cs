using Assets.Source.Components.Animators;
using Assets.Source.Components.Finders;
using Assets.Source.Components.Timer;
using Assets.Source.Math;
using Assets.Source.Unity;
using UnityEngine;

namespace Assets.Source.Components.ActorControllers
{
    [RequireComponent(typeof(PlayerAware), typeof(Rigidbody2D))]
    public class FloatingCraftController : MonoBehaviour
    {
        private GameObject player;
        private Rigidbody2D rigidBody;
        private Animator animator;
        private Destructible destructible;
        private GameTimer preAttackTimer;

        private float xVelocity = 0f;
        private float yVelocity = 0f;

        [SerializeField]
        private float maxSpeed = 1f;

        [SerializeField]
        private float acceleration = 0.1f;

        [SerializeField]
        [Tooltip("This should be roughly the width of the player")]
        private float attackRange = 2f;

        [SerializeField]
        [Tooltip("How far away from the player the enemy seeks out")]
        private float verticalDistanceTarget = 3f;

        [SerializeField]
        [Tooltip("How close the enemy has to be to the target position")]
        private float distanceThreshold = 0.01f;

        [SerializeField]
        [Tooltip("Drag the laser object here")]
        private FloatingCraftLaserController laserController;

        private void Start()
        {
            preAttackTimer = GetComponent<GameTimer>();
            player = GetComponent<PlayerAware>().Player;            
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            destructible = GetComponent<Destructible>(); 

        }

        private void Update()
        {
            UpdateMovement();
            ShootPlayer();
        }

        private void ShootPlayer()
        {
            if (!preAttackTimer.IsActive && !laserController.IsAnimating && 
                xVelocity.IsWithin(acceleration, 0) &&
                transform.position.x.IsWithin(attackRange, player.transform.position.x) &&
                destructible.IsAlive) {
                laserController.FireLaser();                
            }
        }

        private void UpdateMovement()
        {
            if (laserController.IsAnimating)
            {
                xVelocity = 0f;
                yVelocity = 0f; 
            }
            else { 
            
                var yTarget = player.transform.position.y + verticalDistanceTarget;

                if (!transform.position.y.IsWithin(distanceThreshold, yTarget))
                {
                    if (transform.position.y > yTarget)
                    {
                        yVelocity += -acceleration;
                    }
                    else
                    {
                        yVelocity += acceleration;
                    }
                }
                else
                {
                    yVelocity = yVelocity.Stabilize(acceleration / 2, 0);
                }

                var xTarget = player.transform.position.x;

                if (!transform.position.x.IsWithin(distanceThreshold, xTarget))
                {
                    if (transform.position.x > xTarget)
                    {
                        xVelocity += -acceleration;
                    }
                    else
                    {
                        xVelocity += acceleration;
                    }
                }
                else
                {
                    xVelocity = xVelocity.Stabilize(acceleration / 2, 0);
                }


                xVelocity = Mathf.Clamp(xVelocity, -maxSpeed, maxSpeed);
                yVelocity = Mathf.Clamp(yVelocity, -maxSpeed, maxSpeed);
            }

            animator.SetFloat("horizontal-speed", xVelocity);

            rigidBody.velocity = new Vector2(xVelocity, yVelocity);
        }

        private void OnDrawGizmosSelected()
        {
            var plrAware = GetComponent<PlayerAware>();
            if (UnityUtils.Exists(plrAware)) {

                // white circle shows position
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.position, 0.2f);

                // red circle shows target position.
                var plr = plrAware.Player;
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(new Vector3(plr.transform.position.x, plr.transform.position.y + verticalDistanceTarget,1), distanceThreshold);

                // yellow shows attack range
                Gizmos.color = Color.yellow;
                
                Gizmos.DrawLine(new Vector3(plr.transform.position.x - attackRange, plr.transform.position.y - 0.5f, 0),
                                new Vector3(plr.transform.position.x - attackRange, plr.transform.position.y + 0.5f, 0));
                
                Gizmos.DrawLine(new Vector3(plr.transform.position.x + attackRange, plr.transform.position.y - 0.5f, 0),
                                new Vector3(plr.transform.position.x + attackRange, plr.transform.position.y + 0.5f, 0));


            }
        }





    }
}
