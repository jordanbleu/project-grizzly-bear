using System;
using Assets.Source.Math;
using UnityEngine;

namespace Source.Components.Presentation
{
    /// <summary>
    /// Picks a sprite from the specified array and applies it to the sprite renderer.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRandomizer : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;

        private void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var sprite = RandomUtils.Choose(sprites);
            spriteRenderer.sprite = sprite;
        }
    }
}