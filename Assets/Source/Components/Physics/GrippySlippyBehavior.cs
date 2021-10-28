using Assets.Source.Components.ActorControllers.Interfaces;
using UnityEngine;

namespace Assets.Source.Components.Physics
{
    /// <summary>
    /// This is an awful hack I found online and implemented.  Basically, it will detect if the player is physically trying to move.
    /// If they are moving, it applies the 'grippy material' which basically forces the rigid body to have max friction.  Otherwise it 
    /// switches to a material that has lower friction allowing the player to move horizontally.  This prevents the player from sliding down
    /// slopes.
    /// </summary>
    [RequireComponent(typeof(IActorController))]
    public class GrippySlippyBehavior : MonoBehaviour
    {
        [SerializeField]
        public MonoBehaviour actorControllerBehavior;
        private IActorController actorController; // Todo:  Is there a smart way to require this interface implementation in the inspector?  I don't think so but it'd be nice.

        [SerializeField]
        private Rigidbody2D rigidBody;

        [SerializeField]
        private PhysicsMaterial2D slippyMaterial;

        [SerializeField]
        private PhysicsMaterial2D grippyMaterial;


        private void Start()
        {
            actorController = actorControllerBehavior as IActorController;
            if (actorController == null) {
                throw new UnityException("Actor controller must implement IActorController and is also required!");
            }
        }

        private void Update()
        {
            if (actorController.HorizontalSpeed == 0f)
            {
                rigidBody.sharedMaterial = grippyMaterial;
            }
            else {
                rigidBody.sharedMaterial = slippyMaterial;
            }
        }



    }
}
