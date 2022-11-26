using Assets.Source.Data;
using UnityEngine;

namespace Assets.Source.Components.Data
{

    /// <summary>
    /// This gets added to checkpoints and will delete the checkpoint if the player is past that checkpoint.
    /// </summary>
    public class CheckpointSetter : MonoBehaviour
    {

        [SerializeField]
        private Checkpoint checkpoint;

        private void Start()
        {
            var lastCheckpoint = (int)InMemoryGameData.LastCheckpoint;

            if ((int)checkpoint <= lastCheckpoint)
            { 
                Destroy(gameObject);            
            }

        }

        public void SetCheckpoint()
        {
            InMemoryGameData.LastCheckpoint = checkpoint;
        }


    }
}
