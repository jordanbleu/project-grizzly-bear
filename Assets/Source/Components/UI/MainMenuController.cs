using UnityEngine;

namespace Assets.Source.Components.UI
{
    public class MainMenuController : MonoBehaviour
    {

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            
        }

        // called from Player Input component
        public void OnAnyKey() {
            animator.SetTrigger("dismiss");
        }


    }

}