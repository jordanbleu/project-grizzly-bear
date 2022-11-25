using System;
using Assets.Source.Components.ActorControllers;
using Codice.Client.BaseCommands.Merge;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class SkipIntroHack : TestingHackBase
    {
        [SerializeField] private GameObject cutsceneObject;
        [SerializeField] private GameObject mainMenuObject;
        [SerializeField] private PlayerController playerController;

        [SerializeField]
        private bool shouldUnlockPlayer = true;
        
        protected override void Apply()
        {
            Destroy(cutsceneObject);
            mainMenuObject.SetActive(false);
            if (shouldUnlockPlayer)
            {
                playerController.ToggleMovementLock(false);
            }
        }
    }
}