using UnityEngine;

namespace UnityUtils
{
    public class FloatingTextUtility : MonoBehaviour
    {
        [SerializeField]
        private GameObject floatingTextPrefab;

        [SerializeField]
        private Vector3 raiseDistance = new Vector3(0, 100, 0);

        [SerializeField]
        [Range(0.1f, 5)]
        private float raiseDuration = 0.2f;

        [SerializeField]
        private Vector3 lowerDistance = new Vector3(0, -100, 0);

        [SerializeField]
        [Range(0.1f, 5)]
        private float lowerDuration = 0.2f;

        [SerializeField]
        [Range(1, 3)]
        private float raiseScale = 1.2f;

        [SerializeField]
        private Vector3 randomizeOffset = new Vector3(20, 0, 0);

        public void SpawnText(string text, Color color, Transform followTransform, Vector3 transformOffset)
        {
            if (floatingTextPrefab == null)
                return;

            GameObject floatingTextObject = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            floatingTextObject.transform.SetParent(transform, true);

            FloatingText floatingText = floatingTextObject.GetComponent<FloatingText>();
            floatingText.SetProperties(text, color, raiseDistance, raiseDuration, lowerDistance, lowerDuration,
                new Vector3(raiseScale, raiseScale, 1));
        }
    }
}