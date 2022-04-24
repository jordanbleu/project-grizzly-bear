using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void Start()
        {   
            animator = GetComponent<Animator>();            
            boxCollider = GetComponent<BoxCollider2D>();
            cameraImpulseSource = GetComponent<CinemachineImpulseSource>(); 
        }


        public void FireLaser() {
            animator.SetTrigger("laser");
        }

        public void OnAttackEnable()
        {
            IsAttackEnabled = true;
            boxCollider.enabled = true;
            cameraImpulseSource.GenerateImpulse();
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
                cameraImpulseSource.GenerateImpulse(10);
            }
            
        }


    }
}
