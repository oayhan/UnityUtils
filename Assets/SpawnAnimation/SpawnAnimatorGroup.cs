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

        private SpawnAnimator[] spawnAnimators;

        public void StartAnimation()
        {
            spawnAnimators = GetComponentsInChildren<SpawnAnimator>(true);

            foreach (SpawnAnimator spawnAnimator in spawnAnimators)
            {
                if (useRandomParameters)
                {
                    SpawnTransformState[] states = new[]
                        {riseParameters.GetRandomSpawnTransformState(), lowerParameters.GetRandomSpawnTransformState()};
                    spawnAnimator.SetTransformStates(states);
                }
                
                spawnAnimator.StartSpawnSequence();
            }
        }

        [Serializable]
        class AnimatorRangeParameter
        {
            public SpawnTransformState MinValue;
            public SpawnTransformState MaxValue;

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