using Assets.Source.Math;
using UnityEngine;

namespace Assets.Source.Components.Triggers
{
    /// <summary>
    /// Manages seamless transitions between logical portions of a scene.
    /// 
    /// The box collider is used to trigger the transition.  On Exiting the collider, the player's position will be evaluated
    /// against this object's world position (the green line in the editor).  For this reason make sure the collider surrounds
    /// the green line or wierd things will happen (aka it won't work)    ///
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class HorizontalFrameTransition : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Frame to enable if the player exist to the left of the transition")]
        private GameObject leftFrame;


        [SerializeField]
        [Tooltip("Frame to enable if the player exist to the right of the transition")]
        private GameObject rightFrame;


        // Note that the transition width should be wider than the player or this code causes issues.
        private void OnTriggerExit2D(Collider2D collision)
        {
            
            if (collision.gameObject.CompareTag("Player")) {

                var transitionWorldCenterPoint = transform.GetWorldPosition();
                var playerWorldPosition = collision.gameObject.transform.GetWorldPosition();

                if (playerWorldPosition.x < transitionWorldCenterPoint.x)
                {
                    leftFrame.SetActive(true);
                    rightFrame.SetActive(false);
                }
                else {
                    leftFrame.SetActive(false);
                    rightFrame.SetActive(true);
                }

            }            
        }

        private void OnDrawGizmosSelected()
        {
            // draw an epic line that represents the transition
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                new Vector3(transform.position.x, transform.position.y - 100, transform.position.z), 
                new Vector3(transform.position.x, transform.position.y + 100, transform.position.z)
            );
            
        }

    }
}
