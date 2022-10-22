using System;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using Event = Spine.Event;

namespace Assets.Source.Components.ActorControllers
{
    
    public class ShooterCutsceneActor : MonoBehaviour
    {
        [SerializeField]
        private GameObject bulletPrefab;

        [SerializeField]
        private UnityEvent shootEvent;

        private void Start()
        {
            GetComponent<SkeletonAnimation>().AnimationState.Event += HandleEvent;
        }

        private void HandleEvent(TrackEntry trackentry, Event e)
        {
            if (e.Data.Name.Equals("onShoot"))
            {
                shootEvent?.Invoke();
            }
        }
        
    }
}