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

        [SerializeField]
        [Tooltip("drag the face animator here")]
        private Animator faceAnimator;

        public float HorizontalSpeed { get; set; }

        public bool IsHoldingItem { get; set; }

        /// <summary>Values greater than 0 are treated as facing right, less than zero are left.</summary>
        public float Direction { get; set; } = 1;

        /// <summary>
        /// Returns true if the skeleton is flipped to the left
        /// </summary>
        public bool IsFlipped { get; private set; }

        public bool IsGrounded { get; set; }

        public void Jump() => animator.SetTrigger("jump");

        public bool IsFaceGlitched { get; set; }

        public bool IsDead { get; set; }

        private void Update()
        {
            
            HandleFlip();

            animator.SetFloat("horizontal-speed", HorizontalSpeed);
            // we use floats as a hack so we can use blend trees in our animator controller, which simplifies the animation logic a ton
            animator.SetFloat("is-holding-item", BoolToFloat(IsHoldingItem));
            animator.SetBool("is-grounded", IsGrounded);
            animator.SetBool("is-dead", IsDead);
            faceAnimator.SetBool("is-glitched", IsFaceGlitched);
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

        private float BoolToFloat(bool val) => val ? 1 : 0;

        public void Pickup() =>        
            animator.SetTrigger("pickup");

        public void Throw() =>
            animator.SetTrigger("throw");

        public void PutDown() =>
            animator.SetTrigger("put-down");

    }
}
