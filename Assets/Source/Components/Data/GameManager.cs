using System;
using Assets.Editor.Attributes;
using Assets.Source.Components.ActorControllers;
using Assets.Source.Components.Finders;
using Assets.Source.Data;
using UnityEngine;

namespace Source.Components.Data
{
    [RequireComponent(typeof(PlayerAware))]
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerAware playerAware;

        [SerializeField]
        private GameObject parallaxObject;

        [SerializeField]
        private GameObject frameStart;
        
        [SerializeField]
        private GameObject frameUnderground;

        [SerializeField]
        private GameObject frameUnderground2;
        
        [SerializeField]
        private GameObject framePrison;

        [SerializeField] private GameObject cutsceneObject;
        [SerializeField] private GameObject mainMenuObject;
        [SerializeField] private GameObject birdEmitter;
        [SerializeField] private GameObject tumbleweedEmitter;


        [SerializeField]
        [ReadOnly]
        private float totalElapsedTime;
        
        private void Start()
        {

            var isOnFirstFrame = (InMemoryGameData.LastCheckpoint == Checkpoint.Frame1_Start || InMemoryGameData.LastCheckpoint == Checkpoint.Frame1_AfterLaser);

            // if the player has died before, disable the intro cutscene
            // this is hacky but it works 
            if (isOnFirstFrame && InMemoryGameData.Deaths > 0)
            {
                Destroy(cutsceneObject);
                mainMenuObject.SetActive(false);
                playerAware.Player.GetComponent<PlayerController>().ToggleMovementLock(false);
                
            }

            if (!isOnFirstFrame)
            {
                birdEmitter.SetActive(false);
                tumbleweedEmitter.SetActive(false);
            }

            ApplyCheckpoint();
            
        }

        // this method is called when the scene begins or is restarted.
        private void ApplyCheckpoint()
        {
            var checkpoint = InMemoryGameData.LastCheckpoint;
            var player = playerAware.Player;
            
            switch (checkpoint)
            {
                case Checkpoint.Frame1_Start:
                default:
                    break;
                
                case Checkpoint.Frame1_AfterLaser:
                    player.transform.position = new Vector3(309, -3.64f, 0);
                    break;
                
                case Checkpoint.Frame2_Start:
                    parallaxObject.transform.Find("Desert").gameObject.SetActive(false);
                    parallaxObject.transform.Find("Underground").gameObject.SetActive(true);
                    player.transform.position = new Vector3(540f, -65f, 0);
                    frameStart.SetActive(false);
                    frameUnderground.SetActive(true);
                    break;
                
                case Checkpoint.Frame2_AfterWaterPlatforms:
                    parallaxObject.transform.Find("Desert").gameObject.SetActive(false);
                    parallaxObject.transform.Find("Underground").gameObject.SetActive(true);
                    player.transform.position = new Vector3(686, -28f, 0);
                    frameStart.SetActive(false);
                    frameUnderground.SetActive(true);
                    break;
                
                case Checkpoint.Frame2_AfterElevatorPuzzle:
                    parallaxObject.transform.Find("Desert").gameObject.SetActive(false);
                    parallaxObject.transform.Find("Underground").gameObject.SetActive(true);
                    player.transform.position = new Vector3(758, -18.7f, 0);
                    frameStart.SetActive(false);
                    frameUnderground.SetActive(true);
                    break;
                
                case Checkpoint.Frame2_BeforeHardPlatformingArea:
                    parallaxObject.transform.Find("Desert").gameObject.SetActive(false);
                    parallaxObject.transform.Find("Underground").gameObject.SetActive(true);
                    player.transform.position = new Vector3(931.75f, -63.65f, 0);
                    frameStart.SetActive(false);
                    frameUnderground.SetActive(true);
                    break;
                
                case Checkpoint.Frame3_Start:
                    parallaxObject.transform.Find("Desert").gameObject.SetActive(false);
                    parallaxObject.transform.Find("UndergroundPt2").gameObject.SetActive(true);
                    player.transform.position = new Vector3(0, -111, 0);
                    frameStart.SetActive(false);
                    frameUnderground2.SetActive(true);
                    break;
                
                case Checkpoint.Frame3_BeforeZoomoutPuzzle:
                    parallaxObject.transform.Find("Desert").gameObject.SetActive(false);
                    parallaxObject.transform.Find("UndergroundPt2").gameObject.SetActive(true);
                    player.transform.position = new Vector3(393f, -124.67f, 0);
                    frameStart.SetActive(false);
                    frameUnderground2.SetActive(true);
                    break;
                
                case Checkpoint.Frame3_AfterZoomoutPuzzle:
                    parallaxObject.transform.Find("Desert").gameObject.SetActive(false);
                    parallaxObject.transform.Find("UndergroundPt2").gameObject.SetActive(true);
                    player.transform.position = new Vector3(317f, -124.67f, 0);
                    frameStart.SetActive(false);
                    frameUnderground2.SetActive(true);
                    break;
                
                case Checkpoint.Frame3_AfterJunkPilePuzzle:
                    parallaxObject.transform.Find("Desert").gameObject.SetActive(false);
                    parallaxObject.transform.Find("UndergroundPt2").gameObject.SetActive(true);
                    player.transform.position = new Vector3(545.5f, -110.5f, 0);
                    frameStart.SetActive(false);
                    frameUnderground2.SetActive(true);
                    break;
                
                case Checkpoint.Frame3_BeforeMultiLevelPuzzle:
                    parallaxObject.transform.Find("Desert").gameObject.SetActive(false);
                    parallaxObject.transform.Find("UndergroundPt2").gameObject.SetActive(true);
                    player.transform.position = new Vector3(637.5f, -152.7f, 0);
                    frameStart.SetActive(false);
                    frameUnderground2.SetActive(true);
                    break;
                
                case Checkpoint.Frame3_AfterMultiLevelPuzzle:
                    parallaxObject.transform.Find("Desert").gameObject.SetActive(false);
                    parallaxObject.transform.Find("UndergroundPt2").gameObject.SetActive(true);
                    player.transform.position = new Vector3(669.5f, -104.5f, 0);
                    frameStart.SetActive(false);
                    frameUnderground2.SetActive(true);
                    break;
                
                case Checkpoint.Frame5_Beginning:
                    parallaxObject.transform.Find("Desert").gameObject.SetActive(false);
                    parallaxObject.transform.Find("city-underground").gameObject.SetActive(true);
                    player.transform.position = new Vector3(-7f, -197.65f, 0);
                    frameStart.SetActive(false);
                    framePrison.SetActive(true);
                    break;
            }
        }

        public void SetCheckpoint(int checkpoint) => InMemoryGameData.LastCheckpoint = (Checkpoint)checkpoint;

        public void AddDeath() => InMemoryGameData.Deaths++;

        private void Update()
        {
            totalElapsedTime = Time.realtimeSinceStartup;
        }
    }
}