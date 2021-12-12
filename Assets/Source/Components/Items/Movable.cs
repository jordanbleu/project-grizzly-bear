using Spine.Unity;
using UnityEngine;

namespace Assets.Source.Components.Items
{
    public class Movable : MonoBehaviour
    {
        private RigidbodyConstraints2D rigidBodyStartingConstraints;

        private void Start()
        {
            rigidBodyStartingConstraints = rigidBody.constraints;
        }

        [SerializeField]
        private Rigidbody2D rigidBody;

        [SerializeField]
        private BoneFollower boneFollower;

        [SerializeField]
        private Collider2D attachedCollider;
        
        public void Carry() {
            boneFollower.boneName = "ItemBone";
            attachedCollider.enabled = false;
            boneFollower.enabled = true;
            rigidBody.gravityScale = 0;
            // When you pick up an item, its z position is always frozen.
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public void Drop(Vector2 velocity) {
            // this is a hack to fix a bug in the bone follower component where even if
            // it's inactive it will still mess up the object position.
            boneFollower.boneName = string.Empty;
            rigidBody.AddForce(velocity, ForceMode2D.Impulse);
            attachedCollider.enabled = true;
            boneFollower.enabled = false;
            rigidBody.gravityScale = 1;
            // When you release an item, the rigid body constraints go back to what they were by default
            rigidBody.constraints = rigidBodyStartingConstraints;
        }

    }
}
