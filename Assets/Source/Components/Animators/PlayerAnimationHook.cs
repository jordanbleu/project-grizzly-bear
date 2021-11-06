using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.Animators
{
    /// <summary>
    /// Responds to Skeleton animations and invokes unity events
    /// </summary>
    public class PlayerAnimationHook : MonoBehaviour
    {


        [SerializeField]
        private UnityEvent onAnimationPickup = new UnityEvent();


        [SerializeField]
        private UnityEvent onAnimationThrow = new UnityEvent();

        public void AnimatorPickup() => onAnimationPickup?.Invoke();

        public void AnimatorThrow() => onAnimationThrow?.Invoke();
        


    }
}
