using Assets.Editor.Attributes;
using Assets.Source.Data;
using System;
using UnityEngine;

namespace Assets.Source.Components.Testing
{
    internal class GlobalDataTester :MonoBehaviour
    {
        [SerializeField]
        [ReadOnly]
        private string checkpoint;

        [SerializeField]
        [ReadOnly]
        private string playtime;

        [SerializeField]
        [ReadOnly]
        private string deaths;

        [SerializeField]
        [ReadOnly]
        private string jumps;

        [SerializeField]
        [ReadOnly]
        private string throws;

        [SerializeField]
        [ReadOnly]
        private string pickups;

        [SerializeField]
        [ReadOnly]
        private string drops;

        [SerializeField]
        [ReadOnly]
        private string buttonsPressed;

        [SerializeField]
        [ReadOnly]
        private string damage;

        private void Update()
        {
            checkpoint = InMemoryGameData.LastCheckpoint.ToString();
            playtime = TimeSpan.FromSeconds(Time.realtimeSinceStartup).ToString("hh':'mm':'ss'.'fff");
            deaths = InMemoryGameData.Deaths.ToString();
            jumps = InMemoryGameData.Jumps.ToString();  
            throws = InMemoryGameData.Throws.ToString();
            pickups = InMemoryGameData.Pickups.ToString();  
            drops = InMemoryGameData.Drops.ToString();
            buttonsPressed = InMemoryGameData.ButtonsPressed.ToString();
            damage = InMemoryGameData.Damage.ToString();
        }


    }
}
