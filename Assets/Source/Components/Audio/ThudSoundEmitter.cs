using Assets.Source.Math;
using Assets.Source.Unity;
using UnityEngine;

namespace Assets.Source.Components.Audio
{
    /// <summary>
    /// Detects collisions and plays the specified thud noises 
    /// </summary>
    [RequireComponent(typeof(AudioSource), typeof(Collider2D), typeof(Rigidbody2D))]

    public class ThudSoundEmitter : MonoBehaviour
    {
        [SerializeField]
        private AudioClip lowVelocityThud;

        [SerializeField]
        private AudioClip highVelocityThud;

        [SerializeField]
        private LayerMask collisionLayers = UnityUtils.EVERYTHING_LAYER_MASK;

        private Rigidbody2D rigidBody;

        private AudioSource audioSource;

        // an imperfect way to try to get the sound to only play once per frame
        private bool playedSoundThisFrame = false;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var absX = Mathf.Abs(rigidBody.velocity.x);
            var absY = Mathf.Abs(rigidBody.velocity.y);

            if (!playedSoundThisFrame && collisionLayers.IncludesLayer(collision.collider.gameObject.layer) && (absX > 0.1f || absY > 0.1f)) {

                if (absX > 3 || absY > 3)
                {
                    audioSource.PlayOneShot(highVelocityThud);
                }
                else { 
                    audioSource.PlayOneShot(lowVelocityThud);
                }
                playedSoundThisFrame = true;
            }
        }

        private void FixedUpdate()
        {
            playedSoundThisFrame = false;
        }




    }
}
