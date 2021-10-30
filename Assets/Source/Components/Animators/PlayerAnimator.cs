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


        private void Update()
        {
            HandleFlip();

            animator.SetFloat("horizontal-speed", HorizontalSpeed);
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

    }
}
