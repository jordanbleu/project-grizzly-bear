using UnityEngine;

namespace Assets.Source.Math
{
    public static class TransformExtensions
    {

        /// <summary>
        /// Retrieves the current world position of the transform.
        /// </summary>
        /// <returns>Vector3 of the world position</returns>
        public static Vector3 GetWorldPosition(this Transform transform) => transform.TransformPoint(transform.position);

        /// <summary>
        /// Retrieves the current world position of the transform.
        /// </summary>
        /// <returns>Vector3 of the world position</returns>
        public static Vector3 GetWorldPosition(this Transform transform, Vector3 anotherPointRelativeToThisObject) => transform.TransformPoint(anotherPointRelativeToThisObject);



    }
}
