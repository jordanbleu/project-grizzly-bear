using Assets.Source.Components.Utilities;
using Spine;
using Spine.Unity;
using System;
using UnityEngine;

namespace Assets.Source.Components.ActorControllers
{
    /// <summary>
    /// if your skeleton animation has 'left-footstep' and 'right-footstep' events,
    /// this will play footstep sounds automatically
    /// </summary>
    [RequireComponent(typeof(SkeletonAnimation),typeof(SingleUseObjectFactory))]
    public  class AutoFootstepSounds : MonoBehaviour
    {
        private SingleUseObjectFactory objectFactory;
        [SerializeField] AudioClip leftFootstep;
        [SerializeField] AudioClip rightFootstep;   

        private void Start()
        {
            GetComponent<SkeletonAnimation>().AnimationState.Event += HandleEvent;
            objectFactory = GetComponent<SingleUseObjectFactory>();
        }

        private void HandleEvent(TrackEntry trackEntry, global::Spine.Event e)
        {
            if (e.Data.Name.Equals("left-footstep"))
            {
                objectFactory.PlaySound(leftFootstep);
            }
            else if (e.Data.Name.Equals("right-footstep"))
            {
                objectFactory.PlaySound(rightFootstep);
            }
        }
    }
}
