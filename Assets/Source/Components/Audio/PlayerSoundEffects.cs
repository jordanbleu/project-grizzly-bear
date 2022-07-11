using Assets.Source.Components.Utilities;
using UnityEngine;

namespace Assets.Source.Components.Audio
{
    [RequireComponent(typeof(SingleUseObjectFactory))]
    public class PlayerSoundEffects : MonoBehaviour
    {
        private SingleUseObjectFactory objectFactory;


        [SerializeField]
        private AudioClip jumpSound;

        private void Start()
        {
            objectFactory = GetComponent<SingleUseObjectFactory>();
        }

        public void PlayJumpSound() =>
            objectFactory.PlaySound(jumpSound);

    }
}
