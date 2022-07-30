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
        
        protected override void Apply()
        {
            Destroy(cutsceneObject);
            mainMenuObject.SetActive(false);
            playerController.ToggleMovementLock(false);
        }
    }
}