using Assets.Editor.Attributes;
using Assets.Source.Components.Animators;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Components.Items
{
    public class PlayerPickupTrigger : MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimator playerAnimator;

        /// <summary>
        /// Items that are currently in the collider
        /// </summary>
        [SerializeField]
        [ReadOnly]
        private List<GameObject> currentItems;
        public List<GameObject> CurrentItems { get => currentItems; private set => currentItems = value; }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Movable>(out _))
            {
                CurrentItems.Add(collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (CurrentItems.Contains(collision.gameObject))
            {
                CurrentItems.Remove(collision.gameObject);
            }
        }
    }
}
