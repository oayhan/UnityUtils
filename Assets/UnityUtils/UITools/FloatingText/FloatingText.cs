using System.Collections;
using TMPro;
using UnityEngine;

namespace UnityUtils
{
    public class FloatingText : MonoBehaviour
    {
        //cached components
        private TMP_Text textComponent;
        private RectTransform rectTransform;
        private RectTransform canvasTransform;

        //randomized offset for position
        private Vector3 randomOffset;
        //text positions
        private Vector3 originalPos;
        private Vector3 raisedPos;
        private Vector3 loweredPos;
        
        /// <summary>
        /// Sets all properties of floating text and starts animations.
        /// </summary>
        /// <param name="followTransform"></param>
        /// <param name="transformOffset"></param>
        /// <param name="randomOffsetLimit"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="raiseDistance"></param>
        /// <param name="raiseDuration"></param>
        /// <param name="lowerDistance"></param>
        /// <param name="lowerDuration"></param>
        /// <param name="raiseScale"></param>
        public void SetProperties(Transform followTransform, Vector3 transformOffset, Vector3 randomOffsetLimit,
            string text, Color color, Vector3 raiseDistance, float raiseDuration, Vector3 lowerDistance,
            float lowerDuration, Vector3 raiseScale)
        {
            //cache components
            textComponent = GetComponentInChildren<TMP_Text>();
            rectTransform = GetComponent<RectTransform>();
            canvasTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            
            //set text values
            textComponent.text = text;
            textComponent.color = color;

            //set random offset
            randomOffset = UnityHelpers.GetRandomVector3(randomOffsetLimit);
            
            //set text positions
            originalPos = UnityHelpers.WorldPositionToUiPos(followTransform, transformOffset, canvasTransform) + randomOffset;
            raisedPos = originalPos + raiseDistance;
            loweredPos = originalPos - lowerDistance;
            
            //place on original position
            rectTransform.localPosition = originalPos;
            
            //start all animations
            StartCoroutine(StartAnimation(raiseDistance, raiseDuration, lowerDistance, lowerDuration, raiseScale));
        }

        /// <summary>
        /// Helper Coroutine for animations.
        /// </summary>
        /// <param name="raiseDistance"></param>
        /// <param name="raiseDuration"></param>
        /// <param name="lowerDistance"></param>
        /// <param name="lowerDuration"></param>
        /// <param name="raiseScale"></param>
        /// <returns></returns>
        private IEnumerator StartAnimation(Vector3 raiseDistance, float raiseDuration, Vector3 lowerDistance,
            float lowerDuration, Vector3 raiseScale)
        {
            float elapsedTime = 0;

            //raise the position and increment scale
            while (elapsedTime < raiseDuration)
            {
                elapsedTime += Time.deltaTime;
                rectTransform.anchoredPosition3D =
                    Vector3.Lerp(originalPos, raisedPos, 1 - (raiseDuration - elapsedTime));
                rectTransform.localScale = Vector3.Lerp(Vector3.one, raiseScale, 1 - (raiseDuration - elapsedTime));

                yield return null;
            }

            elapsedTime = 0;
            //lower positions, decrement scale and reduce alpha
            while (elapsedTime < lowerDuration)
            {
                elapsedTime += Time.deltaTime;
                rectTransform.anchoredPosition3D =
                    Vector3.Lerp(raisedPos, loweredPos, 1 - (lowerDuration - elapsedTime));
                rectTransform.localScale = Vector3.Lerp(raiseScale, Vector3.one, 1 - (lowerDuration - elapsedTime));

                textComponent.alpha = Mathf.Lerp(1, 0, 1 - (lowerDuration - elapsedTime));

                yield return null;
            }

            //destroy object after animations are done
            Destroy(gameObject, 0.1f);
        }
    }
}