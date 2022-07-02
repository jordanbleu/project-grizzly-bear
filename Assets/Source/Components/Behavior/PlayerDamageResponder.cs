using Assets.Source.Components.ActorControllers;
using Cinemachine;
using UnityEngine;

namespace Assets.Source.Components.Behavior
{
    /// <summary>
    /// Handles logic for when enemies attack the player.
    /// </summary>
    public class PlayerDamageResponder : MonoBehaviour, IDamageReceiver
    {
        private CinemachineImpulseSource cameraImpulse;
        private Destructible playerDestructible;

        private void Start()
        {
            cameraImpulse = GetComponent<CinemachineImpulseSource>();
            playerDestructible = GetComponent<Destructible>();
        }


        public void RecieveDamage(GameObject sender, int amount, Vector2 force)
        {
            // shake cam
            cameraImpulse.GenerateImpulse(2);

            // decrease health
            playerDestructible.DecreaseHealth(amount);

        }
    }
}
