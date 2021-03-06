using System;
using UnityEngine;

namespace UnityUtils
{
    /// <summary>
    /// Wrapper class for spawn animation variables
    /// </summary>
    [Serializable]
    public class SpawnTransformState
    {
        public float StartingDelay;
        public Vector3 PositionOffset;
        public Vector3 Scale = Vector3.one;
        public float TweenDuration;
    }
}