using System;
using Assets.Source.Components.Finders;
using UnityEngine;

namespace Assets.Source.Components.Utilities
{
    [RequireComponent((typeof(PlayerAware)))]
    public class PlayerTeleporter : MonoBehaviour
    {
        private PlayerAware playerAware;
        
        [SerializeField]
        private Vector2 positionForPlayer;

        private void Start()
        {
            playerAware = GetComponent<PlayerAware>();
        }

        public void Teleport() => playerAware.Player.transform.position = positionForPlayer;

        
        
    }
}