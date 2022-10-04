using System;
using Assets.Source.Components.ActorControllers;
using UnityEngine;

namespace Assets.Source.Components.Behavior
{
    public class DamageOnCollideBehavior : MonoBehaviour
    {
        [SerializeField]
        private int damage = 1;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Destructible>(out var destructible))
            {
                destructible.DecreaseHealth(damage);   
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Destructible>(out var destructible))
            {
                destructible.DecreaseHealth(damage);   
            }
        }
    }
}