using System.Linq;
using Assets.Editor.Attributes;
using Assets.Source.Math;
using Assets.Source.Unity;
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
            Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position + feetPosition, radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject && (!colliders[i].isTrigger) && groundLayers.IncludesLayer(colliders[i].gameObject.layer))
                {
                    return true;
                }
            }
            return false;
        }

        private Vector2 CalculateNormal() {

            var raycasts = Physics2D.RaycastAll((Vector2)transform.position+feetPosition, -transform.up, radius*2, groundLayers, -9999, 9999);

            var raycast = raycasts.Where(r => !r.collider.isTrigger).FirstOrDefault();
           
            // This stops the player from rotating his whole body when passing over smaller obstacles, like rocks, etc.
            // So, the player will only rotate towards an incline if the height of the ground is above this value
            if (UnityUtils.Exists(raycast.collider) && raycast.collider.bounds.size.y < 2) {
                return Vector2.zero;                
            }            

            return raycast.normal;
  
        }

        private void OnDrawGizmosSelected()
        {
            // white or magenta shows the feet radius
            if (IsGrounded)
            {
                Gizmos.color = Color.white;
            }
            else {
                Gizmos.color = Color.magenta;
            }
            Gizmos.DrawWireSphere(new Vector3(transform.position.x + feetPosition.x, transform.position.y + feetPosition.y, 1), radius);
            
            // Red line shows the normal
            Gizmos.color = Color.red;
            Gizmos.DrawLine((Vector2)transform.position + feetPosition, (Vector2)transform.position + feetPosition + groundNormal);

            // Blue line shows the raycast (approximately) d
            var temp = (Vector2)transform.position + feetPosition;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(temp, new Vector2(temp.x, temp.y - radius));
        }
    }
}
