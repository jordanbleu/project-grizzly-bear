using Assets.Source.Data;
using UnityEngine;

namespace Assets.Source.Components.Data
{
    internal class GameDataResetter : MonoBehaviour
    {
        private void Start()
        {
            InMemoryGameData.LastCheckpoint = Checkpoint.Frame1_Start;
            InMemoryGameData.Deaths = 0;
            InMemoryGameData.Jumps = 0;
            InMemoryGameData.Throws = 0;
            InMemoryGameData.Pickups = 0;
            InMemoryGameData.Drops = 0;
            InMemoryGameData.ButtonsPressed = 0;
            InMemoryGameData.Damage = 0;
            InMemoryGameData.IsBadEnding = false;
        }
    }
}
