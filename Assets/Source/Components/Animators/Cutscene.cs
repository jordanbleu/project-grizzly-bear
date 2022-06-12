using UnityEngine;

namespace Assets.Source.Components.Animators
{
    public class Cutscene : MonoBehaviour
    {

        [SerializeField]
        private AnimatorTriggerHook cinematicBarObject;

        public void EnableCinematicBars() =>
            cinematicBarObject.SetAnimatorTrigger("enable");

        public void DisableCinematicBars() =>
            cinematicBarObject.SetAnimatorTrigger("disable");


    }
}
