using System;
using Assets.Source.Components.Finders;
using Assets.Source.Unity;
using UnityEngine;

namespace Assets.Source.Components.Behavior
{
    [RequireComponent(typeof(PlayerAware))]
    public class TeleportToPlayerOnAwake : MonoBehaviour
    {
        private PlayerAware playerAware;
        private void Awake()
        {
            if (!UnityUtils.Exists(playerAware))
            {
                playerAware = GetComponent<PlayerAware>();
            }

            transform.position = playerAware.Player.transform.position;
        }

        private void OnEnable()
        {
            if (!UnityUtils.Exists(playerAware))
            {
                playerAware = GetComponent<PlayerAware>();
            }

            transform.position = playerAware.Player.transform.position;
        }
    }
}