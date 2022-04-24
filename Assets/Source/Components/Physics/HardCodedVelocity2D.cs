using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Components.Physics
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HardCodedVelocity2D : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Determines starting velocity for the RigidBody2D")]
        private Vector2 velocity;

        private Rigidbody2D rigidBody;
        public void Start() {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            rigidBody.velocity = velocity;
        }



    }
}
