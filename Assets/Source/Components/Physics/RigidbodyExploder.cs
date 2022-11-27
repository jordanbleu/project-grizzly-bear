using Assets.Source.Components.Behavior;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.Physics
{
    [RequireComponent(typeof(Collider2D))]
    public class RigidbodyExploder : MonoBehaviour, IDamageReceiver
    {
        private Rigidbody2D rigidBody;
        
        [SerializeField] private UnityEvent onExplode = new UnityEvent();

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
        public void RecieveDamage(GameObject sender, int amount, Vector2 force)
        {
            onExplode?.Invoke();
            rigidBody.AddForceAtPosition(sender.transform.position, force/10);
        }

    }
}
