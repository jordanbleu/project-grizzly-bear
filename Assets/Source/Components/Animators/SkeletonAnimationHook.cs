using Spine.Unity;
using UnityEngine;

namespace Assets.Source.Components.Animators
{
    public class SkeletonAnimationHook : MonoBehaviour
    {
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;
        

        public void SetAnimation(string name) {
            skeletonAnimation.AnimationName = name;
        }

        public void FlipSkeleton() => 
            skeletonAnimation.Skeleton.ScaleX = -Mathf.Abs(skeletonAnimation.Skeleton.ScaleX);

        public void UnflipSkeleton() { 
            skeletonAnimation.Skeleton.ScaleX = Mathf.Abs(skeletonAnimation.Skeleton.ScaleX);
        
        }
    }
}
