using Assets.Source.Unity;
using UnityEngine;

namespace Assets.Source.Components.Finders
{
    /// <summary>
    /// A centralized component for player references.  Makes it so I don't have to drag the player reference to a million places. 
    /// </summary>
    public class PlayerAware : MonoBehaviour
    {

        [SerializeField]
        private GameObject player;

        public GameObject Player { get => player; }

    }
}
