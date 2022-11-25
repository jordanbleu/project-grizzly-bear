using System;
using Assets.Source.Components.Finders;
using Assets.Source.Math;
using UnityEngine;

namespace Assets.Source.Components.ActorControllers
{
    [RequireComponent(typeof(Animator), typeof(PlayerAware))]
    public class BossLaserActor : MonoBehaviour
    {
        private Animator animator;
        private PlayerAware playerAware;
        
        [SerializeField]
        private Range<float> activeZone = new Range<float>(-2, 2);

        private bool isFiring = false;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            playerAware = GetComponent<PlayerAware>();
        }

        private void Update()
        {
            var playerPos = playerAware.Player.transform.position;

            if (isFiring)
            {
                return;
            }

            var position = transform.position;
            var rangeMin = position.y + activeZone.min;
            var rangeMax = position.y + activeZone.max;

            if (playerPos.y.IsBetween(rangeMin, rangeMax))
            {
                animator.SetTrigger("shoot");
                isFiring = true;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;

            var position = transform.position;
            Gizmos.DrawLine(new Vector3(position.x, position.y+activeZone.min, position.z), 
                new Vector3(position.x, position.y+activeZone.max, position.z));
        }

        public void OnAnimatorShootComplete() =>
            isFiring = false;
    }
}