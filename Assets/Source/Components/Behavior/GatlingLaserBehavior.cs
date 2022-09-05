using UnityEngine;
using Assets.Source.Components.Finders;
using Assets.Source.Math;

namespace Assets.Source.Components.Behavior
{
    [RequireComponent(typeof(PlayerAware))]
    public class GatlingLaserBehavior : MonoBehaviour
    {

        [SerializeField]
        private GameObject gunObject;

        [SerializeField]
        private float range;

        [SerializeField]
        private float speed = 1f;

        private PlayerAware playerAware;
        private Rigidbody2D gunObjectRigidBody;

        private float yMin;
        private float yMax;

        private void Start()
        {
            yMin = gunObject.transform.position.y - range;
            yMax = gunObject.transform.position.y + range;
            gunObjectRigidBody = gunObject.GetComponent<Rigidbody2D>();
            playerAware = GetComponent<PlayerAware>();
        }

        private void Update()
        {
            var yVel = 0f;
            var playerY = playerAware.Player.transform.position.y;

            if (playerY.IsBetween(yMin, yMax))
            {
                if (!gunObject.transform.position.y.IsWithin(0.005f, playerY)) {
                    yVel = ((playerY - gunObject.transform.position.y)) * speed;
                }                
            }

            gunObjectRigidBody.velocity = new Vector2(0, yVel);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y - range, 0), new Vector3(0.1f, 0.1f, 0.1f));
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + range, 0), new Vector3(0.1f, 0.1f, 0.1f));
        }

    }
}
