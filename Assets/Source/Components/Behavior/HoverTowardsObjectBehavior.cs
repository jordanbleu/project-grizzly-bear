using Assets.Editor.Attributes;
using Assets.Source.Math;
using Assets.Source.Unity;
using UnityEngine;

namespace Assets.Source.Components.Behavior
{
    public class HoverTowardsObjectBehavior : MonoBehaviour
    {

        [SerializeField]
        private GameObject targetObject;

        [SerializeField]
        private Vector2 positionOffset;

        [SerializeField]
        private Rigidbody2D rigidBody;

        [SerializeField]
        private float maxSpeed = 0.5f;

        [SerializeField]
        private float followDistanceThreshold = 0.001f;

        [SerializeField]
        private float acceleration = 0.01f;

        [SerializeField]
        [ReadOnly]
        private float xVelocity = 0f;
        [ReadOnly]
        [SerializeField]
        private float yVelocity = 0f;   

        private void Update()
        {
            var targetPosition = (Vector2)(targetObject.transform.position) + positionOffset;



            if (!transform.position.x.IsWithin(followDistanceThreshold, targetPosition.x)) { 
            
                if (xVelocity < maxSpeed && targetPosition.x > transform.position.x)
                {
                    xVelocity += acceleration;
                }
                else if (xVelocity > -maxSpeed && targetPosition.x < transform.position.x)
                {
                    xVelocity -= acceleration;
                }
            }

            if (!transform.position.y.IsWithin(followDistanceThreshold, targetPosition.y)) { 
                if (yVelocity < maxSpeed && targetPosition.y > transform.position.y)
                {
                    yVelocity += acceleration;
                }
                else if (yVelocity > -maxSpeed && targetPosition.y < transform.position.y) {
                    yVelocity -= acceleration;            
                }
            }

            rigidBody.velocity = new Vector2(xVelocity, yVelocity);
            //.AddForce(new Vector2(xVelocity, yVelocity));

        }


        private void OnDrawGizmosSelected()
        {
            if (UnityUtils.Exists(targetObject)) { 
                Gizmos.color = Color.yellow;
                var tgtPosition = (Vector2)targetObject.transform.position + positionOffset;
                Gizmos.DrawWireCube(tgtPosition, targetObject.transform.localScale);
            }
        }



    }
}
