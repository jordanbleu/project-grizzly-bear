using Assets.Editor.Attributes;
using Assets.Source.Components.EventHooks;
using Assets.Source.Components.Timer;
using Assets.Source.Math;
using Assets.Source.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Components.Items
{
    public class ItemSpawner : MonoBehaviour
    {
        private GameTimer timer;

        [SerializeField]
        [Tooltip("How often a new item will spawn")]
        private int spawnInterval;

        [SerializeField]
        [Tooltip("How many total items can be active in the scene at a time")]
        private int itemLimit = 5;

        [SerializeField]
        [Tooltip("The game objects to spawn.  Can either be a prefab or an inactive template object in the hierarchy.  One will be picked at random")]
        private List<GameObject> prefabs;


        [SerializeField]
        [Tooltip("[OPTIONAL]The parent object to spawn under.  If left empty the current spawner will be used as the parent.")]
        private GameObject spawnParent;

        //[SerializeField]
        //[Tooltip("Drag a box collider here, with 'trigger' set to true.  This is the zone that the object can spawn within. " +
        //    "This is optional, if not supplied the spawner's current position will be used.")]
        //private BoxCollider2D spawnArea;

                
        [SerializeField]
        [ReadOnly]
        private HashSet<GameObject> objects = new HashSet<GameObject>();


        private void Start()
        {
            SetupTimer();

        }

        private void SetupTimer()
        {
            timer = gameObject.AddComponent<GameTimer>();
            timer.StartTime = spawnInterval;
            timer.onTimerReachZero.AddListener(OnIntervalHit);
            timer.Label = "Spawn Timer";
            timer.ResetTimer();
            timer.StartTimer();
        }

        private void OnIntervalHit()
        {

            //
            // todo: you're probably wondering why the collision zone thing isn't working.  Its because it isn't implemented yet.
            //
            var pos = transform.position;

            var par = UnityUtils.Exists(spawnParent) ? spawnParent.transform : transform;

            if (objects.Count() < itemLimit) {

                var prefab = prefabs.PickOne();

                var obj = Instantiate(prefab, par);
                obj.transform.position = pos;
                obj.SetActive(true);
                // naming will be like {spawner name}:{prefab name}::{object hashcode}
                obj.name = $"{gameObject.name}:{prefab.name}::{obj.GetHashCode()}";

                // each spawned object has a Destroy event hook added so we can update our list once this object is destroyed
                var destroyHook = obj.AddComponent<DestroyEventHook>();
                destroyHook.onDestroy.AddListener(UpdateObjectList);

                objects.Add(obj);
            }

            timer.ResetTimer();
            timer.StartTimer();
        }

        private void UpdateObjectList(GameObject obj)
        {
            if (objects.Contains(obj)) {
                objects.Remove(obj);
            }            
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(transform.position,"square-scope-icon");
        }

    }
}
