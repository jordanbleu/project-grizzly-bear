using Assets.Source.Components.Finders;
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
        private Destructible playerDestructible;
        
        [SerializeField]
        private PlayerAware playerAware;

        [SerializeField]
        private int laserDamage = 4;

        private void Start()
        {   
            animator = GetComponent<Animator>();            
            boxCollider = GetComponent<BoxCollider2D>();
            cameraImpulseSource = GetComponent<CinemachineImpulseSource>();

            playerDestructible = playerAware.Player.GetComponent<Destructible>();
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
            // If player is hit
            if (IsAttackEnabled && collision.gameObject.name == "Player") { 
                // end the attack early
                OnAttackDisable();
                cameraImpulseSource.GenerateImpulse(2);
                playerDestructible.DecreaseHealth(laserDamage);
            }
            
        }


    }
}
