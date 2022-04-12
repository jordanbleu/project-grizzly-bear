using UnityEngine;

namespace Assets.Source.Components.Behavior
{
    public class PointAtObjectBehavior : MonoBehaviour
    {

        [SerializeField]
        private GameObject objectToPointAt;

        private void Update()
        {
            //https://answers.unity.com/questions/1350050/how-do-i-rotate-a-2d-object-to-face-another-object.html
            Vector3 targ = objectToPointAt.transform.position;

            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x -= objectPos.x;
            targ.y -= objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }


    }
}
