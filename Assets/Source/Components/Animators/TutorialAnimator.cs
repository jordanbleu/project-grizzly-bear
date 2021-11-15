using TMPro;
using UnityEngine;

namespace Assets.Source.Components.Animators
{
    public class TutorialAnimator : MonoBehaviour
    {

        [SerializeField]
        private Animator animator;


        [SerializeField]
        private TextMeshProUGUI textMesh;

        //[SerializeField]
        //private TextMeshProUGUI fieldName;

        public void Show(string text) {
            textMesh.SetText(text);
            animator.SetTrigger("show");        
        }

        public void Hide() {
            animator.SetTrigger("hide");
        }




    }
}
