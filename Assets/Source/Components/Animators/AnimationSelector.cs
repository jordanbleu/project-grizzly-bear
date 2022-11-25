using Assets.Source.Unity;
using UnityEngine;

namespace Assets.Source.Components.Animators
{
    /// <summary>
    /// To use this, attach an animator with a int paramter called 'index' and have the animation be selected based on that.
    /// Animator will be set on start (not during gameplay).
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimationSelector : MonoBehaviour
    {

        [Tooltip("This is the name of the int field that should be named 'index'")]
        [SerializeField]
        private int animationIndex = 0;


        private void Start()
        {
            GetComponent<Animator>().SetInteger("index", animationIndex);            
        }



    }
}
