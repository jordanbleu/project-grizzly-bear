using Spine.Unity;
using UnityEngine;

namespace Assets.Source.Components.Items
{
    public class Movable : MonoBehaviour
    {


        [SerializeField]
        private Rigidbody2D rigidBody;

        [SerializeField]
        private BoneFollower boneFollower;

        [SerializeField]
        private Collider2D attachedCollider;
        
        public void Carry() {
            attachedCollider.enabled = false;
            boneFollower.enabled = true;
            rigidBody.gravityScale = 0;
        }

        public void Drop(Vector2 velocity) {
            rigidBody.AddForce(velocity, ForceMode2D.Impulse);
            attachedCollider.enabled = true;
            boneFollower.enabled = false;
            rigidBody.gravityScale = 1;
        }

    }
}
