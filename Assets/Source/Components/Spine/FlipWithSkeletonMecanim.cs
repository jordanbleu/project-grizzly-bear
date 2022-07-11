using Spine;
using Spine.Unity;
using UnityEngine;

namespace Assets.Source.Components.Spine
{
    /// <summary>
    /// This should be used for basic objects that are children of a spine animation object.  It basically just hooks into
    ///  the skeleton animation flipping and applies rotation based on that.  
    /// </summary>
    public class FlipWithSkeletonMecanim : MonoBehaviour
    {

        [SerializeField]
        private SkeletonMecanim skeletonMecanim;

        private void Update()
        {
            if (skeletonMecanim.skeleton.ScaleX < 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, 180);
            }
            else {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }            
        }

    }
}
