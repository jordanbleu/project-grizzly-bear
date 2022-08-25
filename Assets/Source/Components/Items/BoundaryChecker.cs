using Assets.Editor.Attributes;
using Assets.Source.Math;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.Items
{
    public class BoundaryChecker : MonoBehaviour
    {
        [Tooltip("The center in world space, generally just use the object's position.")]
        [SerializeField]
        private Vector2 boundaryCenter;

        [SerializeField]
        private Vector2 boundarySize;

        [SerializeField]
        private OutOfBoundsAction action = OutOfBoundsAction.DoNothing;

        [SerializeField]
        private UnityEvent OnOutOfBounds = new UnityEvent();


        // Used for easier maths
        private Square square;
        
        [SerializeField]
        [Tooltip("This is the position the object will reset to if the action is set to 'reset'")]
        [ReadOnly]
        private Vector3 resetPosition;

        private void Start()
        {
            resetPosition = transform.position;
            square = new Square(boundaryCenter, boundarySize);

            if (!square.SurroundsPoint(transform.position))
            {
                UnityEngine.Debug.LogWarning($"Hey, the object '{gameObject.name}' is out of bounds" +
                                             $" before the game even started.  It's probably gonna act weird.");
            }
        }

        private void Update()
        {
            if (!square.SurroundsPoint(transform.position))
            {

                if (action == OutOfBoundsAction.Destroy)
                {
                    Destroy(gameObject);
                }
                else if (action == OutOfBoundsAction.Reset) {
                    transform.position = resetPosition;

                    // if the object has a rigid body, set velocity to zero (?)
                    if (TryGetComponent <Rigidbody2D>(out var rigidBody)) {
                        rigidBody.velocity = Vector3.zero;
                    }

                }

                // Invoked when the object goes out of bounds.  For example,
                // used by movable objects to tell the player to stop carrying the object
                OnOutOfBounds?.Invoke();

            }
        }


        public enum OutOfBoundsAction { 
            [Tooltip("Object won't do anything.")]
            DoNothing,
            [Tooltip("The object will reset to the position it was at when created.")]
            Reset,
            [Tooltip("The object will destroy itself.")]
            Destroy
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(boundaryCenter, boundarySize);
        }
        


    }
}
