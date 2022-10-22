using System;
using Assets.Source.Components.Utilities;
using UnityEngine;

namespace Assets.Source.Components.ActorControllers
{
    public class BulletActorController : MonoBehaviour
    {
        private Rigidbody2D rigidBody;
        private SingleUseObjectFactory objectFactory;
        
        [SerializeField]
        private ParticleSystem particles;

        
        [SerializeField]
        private GameObject lightObject;

        private bool isDead = false;
        
        private void Start()
        {
            objectFactory = GetComponent<SingleUseObjectFactory>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!isDead)
            {
                rigidBody.velocity = Vector2.zero;
                rigidBody.gravityScale = 1;
                transform.localScale = new Vector3(0.25f, 1, 1);
                particles.Play();
                lightObject.SetActive(false);
                objectFactory.InstantiateCameraImpulse(0.25f);
                isDead = true;
            }

        }
    }
}