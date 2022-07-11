using UnityEngine;

namespace Assets.Source.Components.Audio
{
    /// <summary>
    /// This is used for things like engines that whirrrr up to life and then whirrr back down.
    /// </summary>
    [RequireComponent(typeof(AudioSource))] 
    public class AcceleratedAudioPlayer : MonoBehaviour
    {
        private AudioSource audioSource;

        [SerializeField]
        //[Range(0f, 1f)]
        [Tooltip("Controls the virtual 'velocity' of the audio.  This affects volume and pitch.")]
        private float audioVelocity = 0f;
        public float AudioVelocity { get => audioVelocity; set => audioVelocity = value; }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (AudioVelocity < 0.01f)
            {
                if (audioSource.isPlaying) {
                    audioSource.Stop();
                }

            }
            else {

                if (!audioSource.isPlaying) {
                    audioSource.Play();
                }

                audioSource.volume = AudioVelocity;
                audioSource.pitch = AudioVelocity;            
            }
            
        }


    }
}
