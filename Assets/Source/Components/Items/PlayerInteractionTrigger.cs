using Assets.Editor.Attributes;
using Assets.Source.Components.Animators;
using Assets.Source.Components.Switches;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Components.Items
{
    public class PlayerInteractionTrigger : MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimator playerAnimator;

        /// <summary>
        /// Items that are currently in the collider
        /// </summary>
        [SerializeField]
        [ReadOnly]
        private List<GameObject> carryableItems = new List<GameObject>();
        public List<GameObject> CarryableItems { get => carryableItems; private set => carryableItems = value; }


        [SerializeField]
        [ReadOnly]
        private List<GameObject> interactibleItems = new List<GameObject>();
        public List<GameObject> InteractibleItems { get => interactibleItems; private set => interactibleItems = value; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Note, an object can be both movable and interactible.              

            if (collision.gameObject.TryGetComponent<Movable>(out _))
            {
                CarryableItems.Add(collision.gameObject);
            }
            
            if (collision.gameObject.TryGetComponent<IInteract>(out _)) {
                InteractibleItems.Add(collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (CarryableItems.Contains(collision.gameObject))
            {
                CarryableItems.Remove(collision.gameObject);
            }

            if (InteractibleItems.Contains(collision.gameObject)) {
                InteractibleItems.Remove(collision.gameObject);
            }
        }
    }
}
