using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityUtils
{
    public class SpawnAnimatorGroup : MonoBehaviour
    {
        
        [SerializeField]
        private bool useRandomParameters = true;

        [SerializeField]
        private AnimatorRangeParameter riseParameters;

        [SerializeField]
        private AnimatorRangeParameter lowerParameters;

        //cached SpawnAnimator components
        private SpawnAnimator[] spawnAnimators;

        /// <summary>
        /// Starts spawn animation for this group. Finds all SpawnAnimator components in child objects and triggers their spawn animation.
        /// </summary>
        public void StartAnimation()
        {
            //find all children
            spawnAnimators = GetComponentsInChildren<SpawnAnimator>(true);

            foreach (SpawnAnimator spawnAnimator in spawnAnimators)
            {
                //if true, assign random spawn parameters to each SpawnAnimator
                if (useRandomParameters)
                {
                    SpawnTransformState[] states = new[]
                        {riseParameters.GetRandomSpawnTransformState(), lowerParameters.GetRandomSpawnTransformState()};
                    spawnAnimator.SetTransformStates(states);
                }
                
                //start spawn animation
                spawnAnimator.StartSpawnSequence();
            }
        }

        //wrapper class for random spawn animation parameters (min/max ranges for spawn states)
        [Serializable]
        class AnimatorRangeParameter
        {
            public SpawnTransformState MinValue;
            public SpawnTransformState MaxValue;

            /// <summary>
            /// Returns a SpawnTransformState with random values between MinValue and MaxValue.
            /// </summary>
            /// <returns></returns>
            public SpawnTransformState GetRandomSpawnTransformState()
            {
                SpawnTransformState randomState = new SpawnTransformState
                {
                    PositionOffset = UnityHelpers.GetRandomVector3(MinValue.PositionOffset, MaxValue.PositionOffset),
                    Scale = UnityHelpers.GetRandomVector3(MinValue.Scale, MaxValue.Scale),
                    TweenDuration = Random.Range(MinValue.TweenDuration, MaxValue.TweenDuration),
                    StartingDelay = Random.Range(MinValue.StartingDelay, MaxValue.StartingDelay)
                };

                return randomState;
            }
        }
    }
}