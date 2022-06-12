using Assets.Source.Components.Finders;
using Assets.Source.Components.Memory;
using Assets.Source.Lambda;
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
            playerAware = GetComponent<PlayerAware>();    
        }


        private Deferred<GameObject> cameraImpulsePrefab = new Deferred<GameObject>(() => Resources.Load<GameObject>("Prefabs/SingleUseObjects/CAMERA-IMPULSE-SOURCE"));
        private Deferred<GameObject> musicBoxPrefab = new Deferred<GameObject>(() => Resources.Load<GameObject>("Prefabs/SingleUseObjects/MUSIC-BOX"));

        public void InstantiateCameraImpulse(float impulseAmount) {

            var impulseObj = Instantiate(cameraImpulsePrefab.Value, playerAware.Player.transform.position, new Quaternion(), null);

            var impulse = impulseObj.GetComponent<CinemachineImpulseSource>();

            impulse.GenerateImpulse(impulseAmount);

        
        }


        public void PlayMusic(AudioClip musicClip) {

            var musicBoxInst = Instantiate(musicBoxPrefab.Value);

            var audioSource = musicBoxInst.GetComponent<AudioSource>();

            audioSource.PlayOneShot(musicClip);

            musicBoxInst.GetComponent<DestroyAfterAudioClip>().IsActive = true;

        }



    }
}
