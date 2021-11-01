using UnityEngine;

namespace Assets.Source.Components.Items
{
    public class Movable : MonoBehaviour
    {
        [SerializeField]
        private bool isCarried;
        public bool IsCarried { get => isCarried; set => isCarried = value;  }



    }
}
