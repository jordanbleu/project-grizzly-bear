using UnityEngine;

namespace Assets.Source.Components.Animators
{
    [RequireComponent(typeof(Animator))]
    public class DoorAnimator : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        [SerializeField]
        private bool isOpen = false;

        public void Open() => isOpen = true;

        public void Close() => isOpen = false;

        private void Update()
        {
            animator.SetBool("is-open", isOpen);
        }


    }
}
