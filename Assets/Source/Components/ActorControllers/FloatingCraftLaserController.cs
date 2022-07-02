using Assets.Source.Components.Behavior;
using Assets.Source.Components.Finders;
using Assets.Source.Components.Physics;
using Assets.Source.Math;
using Cinemachine;
using UnityEngine;

namespace Assets.Source.Components.ActorControllers
{
    public class FloatingCraftLaserController : MonoBehaviour
    {
        public bool IsAttackEnabled { get; set; }
        public bool IsAnimating { get; set; }

        private Animator animator;
        private BoxCollider2D boxCollider;
        private CinemachineImpulseSource cameraImpulseSource;
        private DamageBroadcaster damageBroadcaster;
        
        
        [SerializeField]
        private PlayerAware playerAware;

        [SerializeField]
        private int laserDamage = 4;


        [SerializeField]
        private int force = 100;

        private void Start()
        {   
            animator = GetComponent<Animator>();            
            boxCollider = GetComponent<BoxCollider2D>();
            cameraImpulseSource = playerAware.Player.GetComponent<CinemachineImpulseSource>();
            damageBroadcaster = GetComponent<DamageBroadcaster>();
        }


        public void FireLaser() {
            animator.SetTrigger("laser");
        }

        public void OnAttackEnable()
        {
            IsAttackEnabled = true;
            boxCollider.enabled = true;
            cameraImpulseSource.GenerateImpulse(0.5f);
        }


        public void OnAttackDisable()
        {
            IsAttackEnabled = false;
            boxCollider.enabled = false;
        }

        public void OnAnimationStart() => IsAnimating = true;

        public void OnAnimationStop() => IsAnimating = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnAttackDisable();

            // either choose negative x force, 0, or positive x force
            var xVel = RandomUtils.Choose(new[] { -force, 0, force});
            var yVel = force;

            // broadcasts the damage to each object that collides with the laser
            damageBroadcaster.BroadcastDamage(collision.gameObject, laserDamage, new Vector2(xVel, yVel));
        }


    }
}
