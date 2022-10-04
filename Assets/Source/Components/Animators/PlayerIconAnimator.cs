using UnityEngine;
using System.Collections;

namespace Assets.Source.Components.Animators
{
	public class PlayerIconAnimator : MonoBehaviour
	{
        public bool ShowButtonPrompt { get; set; }
		public bool ShowPickupPrompt { get; set; }

        private Animator animator;

		// Use this for initialization
		void Start()
		{
			animator = GetComponent<Animator>();
		}

		// Update is called once per frame
		void Update()
		{

			animator.SetBool("show-button-prompt", ShowButtonPrompt);
			animator.SetBool("show-pickup-prompt", ShowPickupPrompt);

		}
	}

}