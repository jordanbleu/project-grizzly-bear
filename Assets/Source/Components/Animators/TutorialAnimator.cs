using Assets.Source.Data;
using Assets.Source.Strings;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Source.Components.Animators
{
    public class TutorialAnimator : MonoBehaviour
    {

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private TextMeshProUGUI textMesh;


        [SerializeField]
        [Tooltip("Just drag the player object here")]
        private PlayerInput playerInputSystem;
        
        private StringLoader stringLoader;

        private void Start()
        {
            // what is dependency injection?
            stringLoader = new StringLoader();
        }

        public void ShowRawText(string text) {
            textMesh.SetText(text);
            animator.SetTrigger("show");        
        }

        /// <summary>
        /// Loads a string from the string loader and displays it
        /// </summary>
        /// <param name="stringCode"></param>
        public void Show(string stringCode)
        {
            var text = stringLoader.LoadString(InMemoryGameData.Language, stringCode);
            textMesh.SetText(text);
            animator.SetTrigger("show");
        }

        /// <summary>
        /// This will load the proper string by its id with the current controller context.  Used for text that 
        /// will change depending on the current control scheme.
        /// So, for example, if the code is 'jump-tutorial':
        /// <para>- if the user is on keyboard / mouse the full code will be jump-tutorial-kbm </para>  
        /// <para>- if the user is on gamepad, the full code will be jump-tutorial-xbox </para>
        /// </summary>
        /// <param name="languageCode"></param>
        public void ShowWithControllerContext(string stringCode) {
            var controlContext = playerInputSystem.currentControlScheme;

            switch (controlContext) {
                case "Gamepad":
                    Show($"{stringCode}-xbox");
                    break;
                case "Keyboard":
                default:
                    Show($"{stringCode}-kbm");
                    break;
            }
        }

        public void Hide() {
            animator.SetTrigger("hide");
        }




    }
}
