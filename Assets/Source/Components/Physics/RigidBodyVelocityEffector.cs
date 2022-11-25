using UnityEngine;

namespace Assets.Source.Components.Physics
{
    /// <summary>
    /// This just reports a velocity to add to your rigid body.  It
    /// doesn't really do anything on its own
    /// </summary>
    public class RigidBodyVelocityEffector : MonoBehaviour
    {

        [SerializeField]
        private Vector2 effectVelocity;
        
        public Vector2 EffectVelocity => effectVelocity;
    }
}