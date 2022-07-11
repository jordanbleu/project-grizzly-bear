using UnityEngine;

namespace Assets.Source.Components.Hooks
{
    public class ParticleSystemHook : MonoBehaviour
    {

        [SerializeField]
        private ParticleSystem particles;

        public void PlayParticlesWithChildren() => particles.Play(true);


    }
}
