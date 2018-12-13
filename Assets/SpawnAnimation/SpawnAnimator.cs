using DG.Tweening;
using UnityEngine;

namespace UnityUtils
{
    public class SpawnAnimator : MonoBehaviour
    {
        [SerializeField]
        private SpawnTransformState[] states;

        private Vector3 originPosition;
        private Vector3 originScale;

        private void Start()
        {
            originPosition = transform.position;
            originScale = transform.localScale;
        }

        public void SetTransformStates(SpawnTransformState[] newStates)
        {
            states = newStates;
        }

        public void StartSpawnSequence()
        {
            transform.position = originPosition;
            transform.localScale = originScale;

            Sequence spawnSequence = DOTween.Sequence();

            foreach (SpawnTransformState transformState in states)
            {
                spawnSequence
                    .AppendInterval(transformState.StartingDelay)
                    .Append(transform.DOMove(originPosition + transformState.PositionOffset,
                        transformState.TweenDuration))
                    .Join(transform.DOScale(transformState.Scale, transformState.TweenDuration));
            }

            spawnSequence.OnComplete(SpawnSequenceCompleted);

            spawnSequence.Play();
        }

        private void SpawnSequenceCompleted()
        {

        }
    }
}