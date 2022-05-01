using Assets.Source.Math;
using Assets.Source.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Components.Triggers
{
    /// <summary>
    /// Spawns an object when this object collides with another object.
    /// This is used for explosions.
    /// </summary>
    public class CollisionEffectSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> objects = new List<GameObject>();

        [SerializeField]
        private LayerMask layers;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (layers.IncludesLayer(collision.collider.gameObject.layer)) {

                foreach (var obj in objects) {
                    var inst = Instantiate(obj);
                    inst.transform.position = collision.GetContact(0).point;
                }
            }            
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (layers.IncludesLayer(collider.gameObject.layer))
            {
                var closestPoint = collider.ClosestPoint(transform.position);

                foreach (var obj in objects)
                {
                    var inst = Instantiate(obj);
                    inst.transform.position = closestPoint;
                }
            }
        }
    }
}
