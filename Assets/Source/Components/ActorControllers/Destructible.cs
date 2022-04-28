using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.ActorControllers
{
    public class Destructible : MonoBehaviour
    {

        [SerializeField]
        private int health = 10;
        public int Health { get => health; } 

        [SerializeField]
        private int maxHealth = 10;
        public int MaxHealth { get => maxHealth; }


        [SerializeField]
        private UnityEvent onHealthDecreased = new UnityEvent();


        [SerializeField]
        private UnityEvent onHealthEmpty = new UnityEvent();

        public void IncreaseHealth(int amount) {
            health += amount;
        }

        public void DecreaseHealth(int amount) {
            if (health > 0)
            {
                health -= amount;
                health = Mathf.Clamp(health, 0, maxHealth);
                onHealthDecreased?.Invoke();

                if (health <= 0)
                {
                    onHealthEmpty?.Invoke();
                }
            }
        }    

    }

}
