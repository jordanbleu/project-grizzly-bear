using System;
using UnityEngine;
using System.Collections;
namespace Assets.Source.Components.Triggers
{
	/// <summary>
    /// On Collision / Trigger enter and exit will spawn an object at the colliding object's location.
    /// </summary>
	public class CollisionDetectorSpawner : MonoBehaviour
	{

		[SerializeField] private GameObject[] objectsToSpawn;


        public void OnTriggerEnter2D(Collider2D collision) => SpawnObject(collision.gameObject.transform.position);
        public void OnTriggerExit2D(Collider2D other) => SpawnObject(other.transform.position);

        private void SpawnObject(Vector2 location) {
            foreach (var obj in objectsToSpawn) {

                var inst = Instantiate(obj);
                inst.transform.position = location;

            }
        }


    }

}