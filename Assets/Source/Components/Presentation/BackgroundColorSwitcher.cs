using UnityEngine;
using System.Collections;
using Assets.Source.Unity;

namespace Source.Components.Presentation
{

	public class BackgroundColorSwitcher : MonoBehaviour
	{
		[SerializeField]
        [Tooltip("Color to set the background to")]
		private Color color;

		[SerializeField]
		[Tooltip("Drag Main camera here")]
		private Camera mainCamera;

        // Use this for initialization
        void Awake()
		{
			if (!UnityUtils.Exists(mainCamera)) {
				Debug.LogError($"BackgroundColorSwitcher on frame {gameObject.name} needs a reference to the main camera");
				return;
			}
			mainCamera.backgroundColor = color;
		}

	}
}