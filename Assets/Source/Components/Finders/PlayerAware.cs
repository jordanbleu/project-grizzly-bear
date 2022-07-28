using Assets.Source.Components.Animators;
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
        [Tooltip("Set this to false to allow runtime lookups.  This should be avoided usually but sometimes is needed.")]
        private bool requirePlayerReference = true;

        [SerializeField]
        [Tooltip("You can drag the player object here to make lookup faster.")]
        private GameObject player;

        private void Start()
        {
            if (requirePlayerReference && !UnityUtils.Exists(player)) {
                throw new UnityException($"PlayerAware object is missing a reference to the player object please add that to object '{gameObject.name}'.");
            }
        }

        public GameObject Player { 
            get
            {
                if (!UnityUtils.Exists(player)) {
                    player = GameObject.FindGameObjectWithTag("Player");
                }
                return player;
            }
        }


    }
}
