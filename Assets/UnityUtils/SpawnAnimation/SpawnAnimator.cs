using DG.Tweening;
using UnityEngine;

namespace UnityUtils
{
    /// <summary>
    /// Animator class for tweening between positions specifically made for objects spawns. Depends on
    /// DOTween library. http://dotween.demigiant.com/
    /// </summary>
    public class SpawnAnimator : MonoBehaviour
    {
        //all states to tween between
        [SerializeField]
        private SpawnTransformState[] states;

        //cached position and scale
        private Vector3 originPosition;
        private Vector3 originScale;

        private void Start()
        {
            //cache position and scale
            originPosition = transform.position;
            originScale = transform.localScale;
        }

        /// <summary>
        /// Helper class for getting transform states from owner SpawnAnimatorGroup.
        /// </summary>
        /// <param name="newStates"></param>
        public void SetTransformStates(SpawnTransformState[] newStates)
        {
            states = newStates;
        }

        /// <summary>
        /// Starts tweening between assigned states.
        /// </summary>
        public void StartSpawnSequence()
        {
            transform.position = originPosition;
            transform.localScale = originScale;

            //create the sequence
            Sequence spawnSequence = DOTween.Sequence();

            //add all tweens to the sequence, including delays
            foreach (SpawnTransformState transformState in states)
            {
                spawnSequence
                    .AppendInterval(transformState.StartingDelay)
                    .Append(transform.DOMove(originPosition + transformState.PositionOffset, transformState.TweenDuration))
                    .Join(transform.DOScale(transformState.Scale, transformState.TweenDuration));
            }

            //set function to call on completion
            spawnSequence.OnComplete(SpawnSequenceCompleted);

            spawnSequence.Play();
        }

        /// <summary>
        /// Virtual function, called on sequence completion.
        /// </summary>
        protected virtual void SpawnSequenceCompleted()
        {

        }
    }
}