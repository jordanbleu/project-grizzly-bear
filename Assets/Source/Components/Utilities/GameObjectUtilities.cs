using UnityEngine;

namespace Assets.Source.Components.Utilities
{
    public class GameObjectUtilities : MonoBehaviour
    {
        private Vector3 originalPosition;

        private void Start()
        {
            originalPosition = transform.position;
        }

        /// <summary>
        /// Destroys the game object this component is attached to
        /// </summary>
        public void KillSelf()
        {
            Destroy(gameObject);
        }

        public void DeactivateSelf()
        {
            gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Instantiates a game object at this object's position
        /// </summary>
        /// <param name="gameObj"></param>
        public void InstantiateAtMe(GameObject gameObj)
        {
            var inst = Instantiate(gameObj, transform.parent);
            
            inst.transform.position = transform.position;
        }

        public void InstantiateAtMeAndEnable(GameObject gameObj)
        {
            var inst = Instantiate(gameObj, transform.parent);

            inst.transform.position = transform.position;
            inst.SetActive(true);
        }

        /// <summary>
        /// Instantiates the object but leaves its position at the default prefab location
        /// </summary>
        /// <param name="gameObj"></param>
        public void InstantiateAtDefault(GameObject gameObj)
        {
            Instantiate(gameObj);
        }

        public void ResetPosition() => transform.position = originalPosition;

        public void SetPosition(Vector2 position) => transform.position = position;
    }
}
