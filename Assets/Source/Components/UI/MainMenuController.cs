using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.UI
{
    public class MainMenuController : MonoBehaviour
    {

        private Animator animator;

        [SerializeField]
        private UnityEvent onMenuDismissed = new UnityEvent();

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        // This is actually only the jump button that invokes this, not any key
        public void OnAnyKey() {
            onMenuDismissed?.Invoke();
            animator.SetTrigger("dismiss");
        }


    }

}