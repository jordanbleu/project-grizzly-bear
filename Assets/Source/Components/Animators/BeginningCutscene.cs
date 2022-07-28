using UnityEngine;

namespace Assets.Source.Components.Animators
{
	/// <summary>
    /// A mostly useless class but its just how it's gotta be.
    /// </summary>
    public class BeginningCutscene : MonoBehaviour
	{

		[SerializeField]
		private GameObject menuObject;

		public void ShowMenu() =>
			menuObject.SetActive(true);
		

	}

}