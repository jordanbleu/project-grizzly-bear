using Assets.Source.Components.Behavior;
using UnityEngine;

namespace Assets.Source.Components.Physics
{
    [RequireComponent(typeof(Collider2D))]
    public class RigidbodyExploder : MonoBehaviour, IDamageReceiver
    {
        private Rigidbody2D rigidBody;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
        public void RecieveDamage(GameObject sender, int amount, Vector2 force)
        {
            rigidBody.AddForceAtPosition(sender.transform.position, force);
        }

    }
}
