using Assets.Editor.Attributes;
using Assets.Source.Components.Timer;
using Assets.Source.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Source.Components.Platforms
{

    /// <summary>
    /// Moving Platforms simply loop among multiple platforming instructions
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class MovingPlatform : MonoBehaviour
    {
        private const float POSITION_TOLERANCE = 0.1f;

        [SerializeField]
        private MoveMode MovementBehavior;

        [SerializeField]
        private List<PlatformInstruction> instructions;

        [SerializeField]
        [ReadOnly]
        private PlatformInstruction current;

        [SerializeField]
        private Rigidbody2D rigidBody;

        private GameTimer timer;


        private int index;
        public int Index { get => index; } 

        private int indexDirection = 1;
        private bool wasAtDestination = false;

        private void Start()
        {
            timer = gameObject.AddComponent<GameTimer>();
            
            timer.onTimerReachZero = new UnityEngine.Events.UnityEvent();
            timer.onTimerReachZero.AddListener(GetNextInstruction);
            timer.Label = "~ Auto-Generated Moving Platform Timer ~";
            index = 0;
        }

        private void Update()
        {
            // Make sure index isn't out of bounds (can happen if manipulating values in inspector)
            index = Mathf.Min(index, instructions.Count() - 1);

            var currentInstruction = instructions[index];
            current = currentInstruction; // for the unity inspector

            if (!timer.IsActive)
            {
                if (IsNearDestination(currentInstruction))
                {
                    if (!wasAtDestination)
                    {
                        currentInstruction.onArrive?.Invoke();
                    }
                    wasAtDestination = true;
                    timer.StartTime = currentInstruction.WaitTime;
                    timer.StartTimer();
                }
                else
                {
                    wasAtDestination = false;
                }

            }

            UpdateVelocity(currentInstruction);
            
        }

        private void GetNextInstruction()
        {
            if (MovementBehavior != MoveMode.Manual)
            {
                if (MovementBehavior == MoveMode.Random)
                {
                    index = UnityEngine.Random.Range(0, (instructions.Count()));
                }
                index += indexDirection;

                if (index > (instructions.Count() - 1) || index < 0)
                {
                    if (MovementBehavior == MoveMode.Cycle)
                    {
                        index = 0;
                    }
                    else
                    {
                        indexDirection = -indexDirection;
                        index += indexDirection;
                    }
                }
            }
        }

        public void CycleNext()
        {

            index += 1;

            if (index > (instructions.Count() - 1))
            {
                index = 0;
            }

        }

        public void CyclePrev()
        {
            index -= 1;

            if (index < 0)
            {
                index = instructions.Count() - 1;
            }
        }

        public void CycleFirst()
        {
            index = 0;
        }

        public void CycleLast()
        {
            index = instructions.Count() - 1;
        }

        public void CycleIndex(int index) => this.index = index;

        private bool IsNearDestination(PlatformInstruction currentInstruction) =>
            (transform.position.x.IsWithin(POSITION_TOLERANCE, currentInstruction.Position.x)) &&
            (transform.position.y.IsWithin(POSITION_TOLERANCE, currentInstruction.Position.y));


        private void UpdateVelocity(PlatformInstruction currentInstruction)
        {
            float xv = 0f;
            float yv = 0f;
            float xp = rigidBody.position.x;
            float yp = rigidBody.position.y;


            // Move x towards instruction's x position
            if (!transform.position.x.IsWithin(POSITION_TOLERANCE, currentInstruction.Position.x))
            {
                if (transform.position.x < currentInstruction.Position.x)
                {
                    xv = currentInstruction.MovementSpeed;
                }
                else if (transform.position.x > currentInstruction.Position.x)
                {
                    xv = -currentInstruction.MovementSpeed;
                }
            }
            else
            {
                xp = currentInstruction.Position.x;
            }

            // Move y towards y position
            if (!transform.position.y.IsWithin(POSITION_TOLERANCE, currentInstruction.Position.y))
            {
                if (transform.position.y < currentInstruction.Position.y)
                {
                    yv = currentInstruction.MovementSpeed;
                }
                else if (transform.position.y > currentInstruction.Position.y)
                {
                    yv = -currentInstruction.MovementSpeed;
                }
            }
            else
            {
                yp = currentInstruction.Position.y;
            }

            rigidBody.position = new Vector2(xp, yp);
            rigidBody.velocity = new Vector2(xv, yv);
        }

        [Serializable]
        public struct PlatformInstruction
        {
            [Tooltip("The world space position to move the platform to")]
            public Vector2 Position;

            [Tooltip("The time to wait (in milliseconds) after the platform reaches its destination")]
            public float WaitTime;

            [Tooltip("The speed at which the platform moves to that position")]
            public float MovementSpeed;

            [Tooltip("The color to show for the gizmo.  Used for debugging / visualizing paths.")]
            public Color GizmoColor;

            [Tooltip("Event thrown when the platform arrives at this location")]
            public UnityEvent onArrive;

        }
        public enum MoveMode
        {
            [Tooltip("Platform moves positions in order.  After it reaches the last position, it loops back to the first.")]
            Cycle,
            [Tooltip("Platform moves forward through all positions in order.  Once it reaches the last position, it cycles backwards back through the positions")]
            Alternate,
            [Tooltip("Platform chooses a position randomly each time")]
            Random,
            [Tooltip("Platform will only move when the 'CycleNext' method is called externally.")]
            Manual
        }

        private void OnDrawGizmosSelected()
        {
            if (instructions != null && instructions.Any())
            {

                var collider = GetComponent<Collider2D>();

                foreach (var inst in instructions)
                {
                    Gizmos.color = inst.GizmoColor;
                    Gizmos.DrawWireCube(inst.Position, collider.bounds.size);
                }
            }
        }

    }
}

