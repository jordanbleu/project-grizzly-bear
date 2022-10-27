using System;
using Assets.Source.Components.ActorControllers;
using Assets.Source.Math;
using Assets.Source.Unity;
using UnityEngine;
using UnityEngine.Analytics;

namespace Assets.Source.Components.Behavior
{
    public class DamageMeOnCollideBehavior : MonoBehaviour
    {
        
        [SerializeField]
        private int damage = 1;

        [SerializeField]
        [Tooltip("if the current object's velocity + the colliding object velocity is > this value, cause damage.")]
        private float velocity = 0f;

        [SerializeField]
        private LayerMask layers;

        private Destructible destructible;
        private void Start()
        {
            destructible = GetComponent<Destructible>();

            if (!UnityUtils.Exists(destructible))
            {
                throw new UnityException(
                    $"Add the destructible component for the {nameof(DamageMeOnCollideBehavior)} to destroy on game object '{gameObject.name}'");
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) => PerformDamage(collision.gameObject);


        private void OnTriggerEnter2D(Collider2D other) => PerformDamage(other.gameObject);

        private void PerformDamage(GameObject other)
        {

            if (!layers.IncludesLayer(other.layer))
            {
                return;
            }

            var xVel = 0f;
            var yVel = 0f;
            
            // If i have a rigid body grab its vel
            if (gameObject.TryGetComponent<Rigidbody2D>(out var rigidBod))
            {
                xVel += Mathf.Abs(rigidBod.velocity.x);
                yVel += Mathf.Abs(rigidBod.velocity.y);
            }
            
            // if the colliding velocity has a rigid body, add in its vel
            if (other.TryGetComponent<Rigidbody2D>(out var otherRb))
            {
                xVel += Mathf.Abs(otherRb.velocity.x);
                yVel += Mathf.Abs(otherRb.velocity.y);
            }

            // if velocity is too low then ignore it.
            if (xVel < velocity && yVel < velocity)
            {
                return;
            }

            destructible.DecreaseHealth(damage);
        }

    }
}