using Assets.Editor.Attributes;
using Assets.Source.Lambda;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Components.Audio
{
    /// <summary>
    /// Music in the game is dynamic and builds upon player progression.  It uses short, seamless loops.
    /// 
    /// This component basically manages that behavior and crossfades between the two songs
    /// </summary>
    public class DynamicMusicFader : MonoBehaviour
    {
        [SerializeField]
        [ReadOnly]
        private AudioSource activeAudioSource;

        [SerializeField]
        [Range(0f, 0.005f)]
        private float crossFadeSpeed = 0.06f;

        private AudioSource sourceA;
        private AudioSource sourceB;

        private AudioClip nextClip = null;

        private Deferred<AudioClip> silentAudio = new Deferred<AudioClip>(() => Resources.Load<AudioClip>("SoundFX/environment/silent"));

        private void Start()
        {
            var gameObjectA = new GameObject("audio-source-a");
            sourceA = gameObjectA.AddComponent<AudioSource>();
            sourceA.loop = true;
            sourceA.volume = 1f;
            gameObjectA.transform.SetParent(transform, false);
            
            var gameObjectB = new GameObject("audio-source-b");
            sourceB = gameObjectB.AddComponent<AudioSource>();
            sourceB.loop = true;
            sourceB.volume = 0f;
            gameObjectB.transform.SetParent(transform, false);

            activeAudioSource = sourceA;
        }

        private void Update()
        {
            var nonActiveAudioSource = GetNonActiveAudioSource();

            // cross fade slowly
            if (activeAudioSource.volume < 1 && activeAudioSource.clip != null)
            {
                activeAudioSource.volume += crossFadeSpeed;
            }

            if (nonActiveAudioSource.volume > 0 && nonActiveAudioSource.clip != null)
            {
                nonActiveAudioSource.volume -= crossFadeSpeed;
            }


            // no bad volumes
            activeAudioSource.volume = Mathf.Clamp(activeAudioSource.volume, 0, 1);
            nonActiveAudioSource.volume = Mathf.Clamp(nonActiveAudioSource.volume, 0, 1);

            // I'm not sure if this is really an optimization but it makes sense
            if (nonActiveAudioSource.volume == 0f && nonActiveAudioSource.isPlaying)
            {
                nonActiveAudioSource.Stop();
            }

            // if we have a clip queued up...
            if (nextClip != null)
            {
                // begin playing on the non active audio source (will be silent)
                nonActiveAudioSource.clip = nextClip;
                nonActiveAudioSource.Play();
                
                // swap the sources, they will begin cross fading on the next update
                activeAudioSource = nonActiveAudioSource;

                // we are done with nextClip
                nextClip = null;
            } 
        }


        private AudioSource GetNonActiveAudioSource() => activeAudioSource == sourceA ? sourceB : sourceA;

        /// <summary>
        /// Crossfades audio to the selected clip
        /// </summary>
        public void QueueAudioClip(AudioClip clip)
        {
            if (!activeAudioSource.isPlaying)
            {
                // if no audio is playing, just play the clip now but still fade in.
                activeAudioSource.clip = clip;
                activeAudioSource.volume = 0f;
                activeAudioSource.Play();
            }
            else
            {
                nextClip = clip;
            }
        }

        /// <summary>
        /// Switches audio immediately bypassing the cross fade
        /// </summary>
        /// <param name="clip"></param>
        public void PlayImmediate(AudioClip clip)
        {
            activeAudioSource.clip = clip;
            activeAudioSource.Play();
            activeAudioSource.volume = 1f;
            GetNonActiveAudioSource().volume = 0f;
            
        }

        /// <summary>
        /// Stops all audio immediately, permanently (used for death screen)
        /// </summary>
        public void StopAll()
        {
            activeAudioSource.Stop();
            GetNonActiveAudioSource().Stop();
        }

        public void FadeOut() => QueueAudioClip(silentAudio.Value);
    }
}
