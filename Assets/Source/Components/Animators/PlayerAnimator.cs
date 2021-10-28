using Spine.Unity;
using UnityEngine;

namespace Assets.Source.Components.Animators
{

    public class PlayerAnimator : MonoBehaviour
    {


        [SerializeField]
        private SkeletonMecanim skeleton;

        [SerializeField]
        [Tooltip("Drag the spine mecanim component here")]
        private Animator animator;

        public float HorizontalSpeed { get; set; }

        public float Direction { get; set; } = 1;

        /// <summary>
        /// Returns true if the skeleton is flipped to the left
        /// </summary>
        public bool IsFlipped { get; private set; }

        public Slope SlopeAngle { get; set; } = Slope.Flat;

        private void Update()
        {
            HandleFlip();

            animator.SetFloat("horizontal-speed", HorizontalSpeed);
            // Has to be a float because blend trees don't work well with ints
            animator.SetFloat("slope", (float)SlopeAngle); 
        }

        private void HandleFlip()
        {
            var xScale = Mathf.Abs(skeleton.Skeleton.ScaleX);

            if (Direction > 0)
            {
                skeleton.Skeleton.ScaleX = xScale;
            }
            else {
                skeleton.Skeleton.ScaleX = -xScale;
            }

            IsFlipped = (skeleton.Skeleton.ScaleX < 0);
        }

        public enum Slope { 
            Flat = 0,
            Up = -1,
            Down = 1            
        }

    }
}
