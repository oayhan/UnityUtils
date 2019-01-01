using UnityEngine;

namespace UnityUtils
{
    public class FloatingTextUtility : MonoBehaviour
    {
        //prefab object
        [SerializeField]
        private GameObject floatingTextPrefab;

        //how much the text will raise (local)
        [SerializeField]
        private Vector3 raiseDistance = new Vector3(0, 100, 0);

        //how long to reach top position
        [SerializeField]
        [Range(0.1f, 5)]
        private float raiseDuration = 0.3f;

        //how much the text will fall (local)
        [SerializeField]
        private Vector3 lowerDistance = new Vector3(0, -100, 0);

        //how long to reach bottom position
        [SerializeField]
        [Range(0.1f, 5)]
        private float lowerDuration = 0.6f;

        //text scale when raised
        [SerializeField]
        [Range(1, 3)]
        private float raiseScale = 1.2f;

        //random position limit for text spawn
        [SerializeField]
        private Vector3 randomizeOffset = new Vector3(20, 0, 0);

        /// <summary>
        /// Spawns a floating text that follows given transform.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="followTransform"></param>
        /// <param name="transformOffset"></param>
        public void SpawnText(string text, Color color, Transform followTransform, Vector3 transformOffset)
        {
            if (floatingTextPrefab == null)
                return;

            //spawn prefab and parent it
            GameObject floatingTextObject = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            floatingTextObject.transform.SetParent(transform, true);

            //set all properties of text and start animations
            FloatingText floatingText = floatingTextObject.GetComponent<FloatingText>();
            floatingText.SetProperties(followTransform, transformOffset, randomizeOffset, text, color, raiseDistance,
                raiseDuration, lowerDistance, lowerDuration, new Vector3(raiseScale, raiseScale, 1));
        }
    }
}