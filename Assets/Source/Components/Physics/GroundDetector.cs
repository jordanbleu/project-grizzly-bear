using Assets.Editor.Attributes;
using Assets.Source.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Components.Physics
{
    /// <summary>
    /// Ground detection logic.
    /// </summary>
    public class GroundDetector : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("This is the position of the actor's 'feet'.  This should be the point at which the ground hits the bottom of the actor's body.")]
        private Vector2 feetPosition = Vector2.zero;
        

        [SerializeField]
        [Tooltip("How big of an area to check for feet.")]
        private float radius = 0.15f;

        [SerializeField]
        [Tooltip("Which layers are considered ground.")]
        private LayerMask groundLayers;
        

        [SerializeField]
        [ReadOnly]
        private bool isGrounded;
        public bool IsGrounded { get => isGrounded; set => isGrounded = value; }

        [ReadOnly]
        [SerializeField]
        private Vector2 groundNormal = Vector2.zero;
        public Vector2 GroundNormal { get => groundNormal; }

        private void Update()
        {
            // Do it this way so we only run this, at most, once per frame.
            IsGrounded = CheckIfGrounded();
            groundNormal = CalculateNormal();
        }

        private bool CheckIfGrounded()
        {
            // Represents the bottom area of the collider, which we call its "feet"
            Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position + feetPosition, radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                // If the game object we're colliding with:
                // * Is not ourself
                // * Is not a trigger
                // * Is included in our ground layers layermask
                if (colliders[i].gameObject != gameObject && (!colliders[i].isTrigger) && groundLayers.IncludesLayer(colliders[i].gameObject.layer))
                {
                    return true;
                }
            }
            return false;
        }

        private Vector2 CalculateNormal() {

            var contactFilter = new ContactFilter2D()
            {
                useLayerMask = true,
                layerMask = groundLayers
            };

            var raycast = Physics2D.Raycast((Vector2)transform.position+feetPosition, -transform.up, radius*2, groundLayers, -9999, 9999);

            return raycast.normal;
  
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(new Vector3(transform.position.x + feetPosition.x, transform.position.y + feetPosition.y, 1), radius);

            Gizmos.DrawLine((Vector2)transform.position + feetPosition, (Vector2)transform.position + feetPosition + groundNormal);
        }
    }
}
