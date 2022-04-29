using UnityEngine;

namespace Assets.Source.Components.Behavior
{
    /// <summary>
    /// Objects implementing this interface will be able to capture damage broadcasts and respond.  
    /// More than one of these components can be on a given object.
    /// </summary>
    public interface IDamageReceiver
    {
        void RecieveDamage(GameObject sender, int amount, Vector2 force);
    }
}
