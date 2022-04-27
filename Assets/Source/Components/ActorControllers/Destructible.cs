using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.ActorControllers
{
    public class Destructible : MonoBehaviour
    {

        [SerializeField]
        private int health = 10;
        
        [SerializeField]
        private UnityEvent onHealthDecreased = new UnityEvent();

        public void IncreaseHealth(int amount) {
            health += amount;
        }

        public void DecreaseHealth() {
            health -= 10;
            onHealthDecreased?.Invoke();
        }    

    }

}
