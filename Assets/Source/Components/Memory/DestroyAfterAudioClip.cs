using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Components.Memory
{
    internal class DestroyAfterAudioClip : MonoBehaviour
    {
        private AudioSource audioSource;
        public bool IsActive { get; set; } = false;
        

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();            
        }

        private void Update()
        {
            if (IsActive && !audioSource.isPlaying) {
                Destroy(gameObject);
            }
        }


    }
}
