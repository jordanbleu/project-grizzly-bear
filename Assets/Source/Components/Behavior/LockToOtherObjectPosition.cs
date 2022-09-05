using UnityEngine;

namespace Assets.Source.Components.Behavior
{
	public class LockToOtherObjectPosition : MonoBehaviour
	{
		[SerializeField] 
		private GameObject other;

		[SerializeField]
		private bool lockToX;

		[SerializeField]
		private bool lockToY;

		// Update is called once per frame
		void Update()
		{
			var posX = (lockToX) ? other.transform.position.x : transform.position.x;
			var posY = (lockToY) ? other.transform.position.y : transform.position.y;

			transform.position = new Vector3(posX, posY, transform.position.z);
		}
	}
}
