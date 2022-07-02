using Assets.Source.Components.ActorControllers;
using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Components.Behavior
{
    /// <summary>
    /// This component is for objects that can inflict damage on objects.
    /// </summary>
    public class DamageBroadcaster : MonoBehaviour
    {
        public void BroadcastDamage(GameObject objectToDamage, int amount, Vector2 force) {
            var damageReceivers = objectToDamage.GetComponents<IDamageReceiver>();

            foreach (var receiver in damageReceivers) {
                receiver.RecieveDamage(gameObject, amount, force);
            }
        }
    }
}
