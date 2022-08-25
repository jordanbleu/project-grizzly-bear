using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;
using Random = UnityEngine.Random;


namespace Assets.Source.Components.ActorControllers
{
    [RequireComponent(typeof(SkeletonAnimation))]
    public class RockThrowerActorController : MonoBehaviour
    {
        private const float MIN_TOSS_VELOCITY = 3;
        private const float MAX_TOSS_VELOCITY = 5;
        
        [SerializeField] private GameObject rockPrefab;

        private SkeletonAnimation skeletonAnim;

        private void Start()
        {
            skeletonAnim = GetComponent<SkeletonAnimation>();
            skeletonAnim.AnimationState.Event += HandleAnimationEvent;
        }

        private void HandleAnimationEvent(TrackEntry trackentry, Event e)
        {
            if ("Throw" == e.Data.Name)
            {
                ThrowRock();
            }
        }
        
        private void ThrowRock()
        {
            // skeleton is facing left
            var tossVelocityX = skeletonAnim.initialFlipX ? 
                Random.Range(-MAX_TOSS_VELOCITY, -MIN_TOSS_VELOCITY) : 
                Random.Range(MIN_TOSS_VELOCITY, MAX_TOSS_VELOCITY);

            var rocc = Instantiate(rockPrefab, transform.parent);
            var myPos = transform.position;
            rocc.transform.position = new Vector3(myPos.x, myPos.y + 2, myPos.z);
            rocc.GetComponent<Rigidbody2D>().velocity = new Vector2(tossVelocityX, 2);
        }
    }
}