using Assets.Source.Components.Utilities;
using System;
using UnityEngine;

namespace Assets.Source.Components.Audio
{
    [RequireComponent(typeof(SingleUseObjectFactory))]
    public class PlayerSoundEffects : MonoBehaviour
    {
        private SingleUseObjectFactory objectFactory;


        [SerializeField]
        private AudioClip jumpSound;

        [SerializeField]
        private AudioClip pickupSound;

        [SerializeField]
        private AudioClip throwSound;

        [SerializeField]
        private AudioClip resetSound;

        [SerializeField]
        private AudioClip switchSound;

        private void Start()
        {
            objectFactory = GetComponent<SingleUseObjectFactory>();
        }

        public void PlayJumpSound() =>
            objectFactory.PlaySound(jumpSound);

        public void PlayPickupSound() =>
            objectFactory?.PlaySound(pickupSound);

        public void PlayThrowSound() =>
            objectFactory?.PlaySound(throwSound);

        public void PlayResetSound() =>
            objectFactory?.PlaySound(resetSound);

        public void PlaySwitchSOund() =>
            objectFactory?.PlaySound(switchSound);
    }
}
