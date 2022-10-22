using UnityEngine;

namespace Assets.Source.Components.Physics
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HardCodedVelocity2D : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Determines starting velocity for the RigidBody2D")]
        private Vector2 velocity;

        [SerializeField]
        private bool isConstant = true;
        
        private Rigidbody2D rigidBody;
        public void Start() {
            rigidBody = GetComponent<Rigidbody2D>();
            if (!isConstant)
            {
                rigidBody.velocity = velocity;
            }
        }

        private void Update()
        {
            if (isConstant)
            {
                rigidBody.velocity = velocity;
            }
        }



    }
}
