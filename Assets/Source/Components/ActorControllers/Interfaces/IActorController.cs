namespace Assets.Source.Components.ActorControllers.Interfaces
{
    public interface IActorController
    {
        /// <summary> The speed of the actor, not including external forces </summary>
        float HorizontalSpeed { get;  }
    }
}
