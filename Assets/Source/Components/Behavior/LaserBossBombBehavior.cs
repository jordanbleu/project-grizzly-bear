using UnityEngine;

namespace Assets.Source.Components.Behavior
{
    public class LaserBossBombBehavior : MonoBehaviour
    {

        [SerializeField]
        private GameObject explosionTemplate;

        public void Explode()
        {
            var inst = Instantiate(explosionTemplate, transform.position, Quaternion.identity);
            inst.SetActive(true);
        }
    }
}