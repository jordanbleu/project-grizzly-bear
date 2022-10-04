using Assets.Editor.Attributes;
using Assets.Source.Components.Animators;
using Assets.Source.Components.Platforms;
using UnityEngine;

namespace Assets.Source.Components.PuzzleControllers
{
    /// <summary>
    /// This puzzle is solved by having two buttons pressed at the same time.  
    /// </summary>
    internal class DoubleButtonPuzzleController : MonoBehaviour
    {
        private bool isCompleted = false;

        [SerializeField]
        [ReadOnly]
        [Tooltip("How many buttons are active currently")]
        private int totalToggles = 0;


        [SerializeField]
        private AnimatorTriggerHook[] elevatorIndicatorAnimators;

        [SerializeField]
        [Tooltip("Drag platform here")]
        private MovingPlatform platform;

        private void Update()
        {
            if (!isCompleted)
            {
                totalToggles = Mathf.Clamp(totalToggles, 0, 2);
                
                // if both buttons are pressed, go to the bottom
                if (totalToggles == 2)
                {
                    platform.CycleIndex(2);
                }
                // if only one button is pressed, go to the top
                else if (totalToggles == 1)
                {
                    platform.CycleIndex(0);
                }
                // otherwise go to the middle
                else
                {
                    platform.CycleIndex(1);
                }
            }

            foreach (var elevatorIndicatorAnimator in elevatorIndicatorAnimators)
            {
                elevatorIndicatorAnimator.SetInt("level", platform.Index);
            }
        }

        public void AddToggle() => totalToggles++;

        public void RemoveToggle() => totalToggles--;


    }
}
