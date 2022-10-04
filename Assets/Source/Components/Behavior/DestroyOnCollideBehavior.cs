using System;
using Assets.Source.Components.ActorControllers;
using Assets.Source.Math;
using UnityEngine;

namespace Assets.Source.Components.Behavior
{
    public class DestroyOnCollideBehavior : MonoBehaviour
    {

        [SerializeField]
        private LayerMask layers;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!layers.IncludesLayer(col.gameObject.layer))
            {
                return;
            }

            if (col.TryGetComponent<Destructible>(out var destructible))
            {
                destructible.DecreaseHealth(destructible.MaxHealth);
            }
        }
    }
}