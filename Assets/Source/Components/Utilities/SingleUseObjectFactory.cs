using System;
using Assets.Source.Components.Finders;
using Assets.Source.Components.Memory;
using Assets.Source.Lambda;
using Assets.Source.Unity;
using Cinemachine;
using UnityEngine;

namespace Assets.Source.Components.Utilities
{
    /// <summary>
    /// This behavior is for creating one time, single use prefabs that fulfil a single purpose then 
    /// destroy themselves
    /// </summary>
    [RequireComponent(typeof(PlayerAware))]
    public class SingleUseObjectFactory : MonoBehaviour
    {
        private PlayerAware playerAware;

        private void Start()
        {
            if (!UnityUtils.Exists(playerAware))
            {
                playerAware = GetComponent<PlayerAware>();    
            }
        }

        private void Awake()
        {
            playerAware = GetComponent<PlayerAware>(); 
        }


        private Deferred<GameObject> cameraImpulsePrefab = new Deferred<GameObject>(() => Resources.Load<GameObject>("Prefabs/SingleUseObjects/CAMERA-IMPULSE-SOURCE"));
        private Deferred<GameObject> musicBoxPrefab = new Deferred<GameObject>(() => Resources.Load<GameObject>("Prefabs/SingleUseObjects/MUSIC-BOX"));
        private Deferred<GameObject> soundPrefab = new Deferred<GameObject>(() => Resources.Load<GameObject>("Prefabs/SingleUseObjects/SOUND"));
        private Deferred<GameObject> sound2dPrefab = new Deferred<GameObject>(() => Resources.Load<GameObject>("Prefabs/SingleUseObjects/SOUND_2D"));

        public void InstantiateCameraImpulse(float impulseAmount) {

            var impulseObj = Instantiate(cameraImpulsePrefab.Value, playerAware.Player.transform.position, new Quaternion(), null);

            var impulse = impulseObj.GetComponent<CinemachineImpulseSource>();

            impulse.GenerateImpulse(impulseAmount);
        }

        // todo:  why did i write this?  
        public void PlayMusic(AudioClip musicClip) {

            var musicBoxInst = Instantiate(musicBoxPrefab.Value);

            var audioSource = musicBoxInst.GetComponent<AudioSource>();

            audioSource.PlayOneShot(musicClip);

            musicBoxInst.GetComponent<DestroyAfterAudioClip>().IsActive = true;
        }

        public void PlaySound(AudioClip clip) {
            var soundInst = Instantiate(soundPrefab.Value,transform.position, Quaternion.identity);
            soundInst.name = $"SND-{clip.name}<-{gameObject.name}";
            var audioSource = soundInst.GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip);
            soundInst.GetComponent<DestroyAfterAudioClip>().IsActive = true;
        }

        /// <summary>
        /// Same as the other PlaySound but this doesn't use the spatial volume thing.
        /// </summary>
        /// <param name="clip"></param>
        public void PlaySound2D(AudioClip clip)
        {
            var soundInst = Instantiate(sound2dPrefab.Value, transform.position, Quaternion.identity);
            soundInst.name = $"SND-{clip.name}<-{gameObject.name}";
            var audioSource = soundInst.GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip);
            soundInst.GetComponent<DestroyAfterAudioClip>().IsActive = true;
        }

    }
}
