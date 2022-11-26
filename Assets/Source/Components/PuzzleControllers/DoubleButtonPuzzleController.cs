using System;
using Assets.Source.Components.Animators;
using Assets.Source.Components.Platforms;
using UnityEngine;

namespace Assets.Source.Components.PuzzleControllers
{
    public class DoubleButtonPuzzleController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Drag platform here")]
        private MovingPlatform platform;

        [SerializeField]
        private AnimatorTriggerHook[] elevatorIndicatorAnimators;

        private bool isTopButtonPressed = false;
        
        private bool isBottomButtonPressed = false;

        private void Update()
        {
            var level = 0;

            if (isBottomButtonPressed)
            {
                level++;
            }

            if (isTopButtonPressed)
            {
                level++;
            }
            
            foreach (var elevatorIndicatorAnimator in elevatorIndicatorAnimators)
            {
                elevatorIndicatorAnimator.SetInt("level", level);
            }
            
            // if both buttons are pressed, go to the bottom
            if (level == 2)
            {
                platform.CycleIndex(2);
            }
            // if only one button is pressed, go to the top
            else if (level == 1)
            {
                platform.CycleIndex(0);
            }
            // otherwise go to the middle
            else
            {
                platform.CycleIndex(1);
            }

        }
        
        public void TopButtonToggleTrue() => isTopButtonPressed = true;
        public void TopButtonToggleFalse() => isTopButtonPressed = false;
        public void BottomButtonToggleTrue() => isBottomButtonPressed = true;
        public void BottomButtonToggleFalse() => isBottomButtonPressed = false;

    }
}