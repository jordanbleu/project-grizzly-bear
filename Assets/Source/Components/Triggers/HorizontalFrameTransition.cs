using UnityEngine;

namespace Assets.Source.Components.Triggers
{
    /// <summary>
    /// Handles seamless transition from one frame to another.
    /// While the player is inside the attached trigger, both frames are enabled.  Upon exiting the left or right 
    /// frame will be enabled depending on the position of the player.  
    /// 
    /// For this reason, the frames should overlap a tiny bit.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class HorizontalFrameTransition : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Frame to enable if the player exist to the left of the transition")]
        private GameObject leftFrame;


        [SerializeField]
        [Tooltip("Frame to enable if the player exist to the right of the transition")]
        private GameObject rightFrame;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (collision.gameObject.CompareTag("Player"))
            //{
            //    leftFrame.gameObject.SetActive(false);
            //    rightFrame.gameObject.SetActive(false);
            //}
        }

        // Note that the transition width should be wider than the player or this code causes issues.
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) {

                if (collision.gameObject.transform.position.x < transform.position.x)
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


    }
}
