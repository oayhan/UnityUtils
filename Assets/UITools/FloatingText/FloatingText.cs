using System.Collections;
using TMPro;
using UnityEngine;

namespace UnityUtils
{
    public class FloatingText : MonoBehaviour
    {
        private TMP_Text textComponent;
        private RectTransform rectTransform;

        public void SetProperties(string text, Color color, Vector3 raiseDistance, float raiseDuration,
            Vector3 lowerDistance, float lowerDuration, Vector3 raiseScale)
        {
            textComponent = GetComponentInChildren<TMP_Text>();
            rectTransform = GetComponent<RectTransform>();

            textComponent.text = text;
            textComponent.color = color;

            StartCoroutine(StartAnimation(raiseDistance, raiseDuration, lowerDistance, lowerDuration, raiseScale));
        }

        private IEnumerator StartAnimation(Vector3 raiseDistance, float raiseDuration, Vector3 lowerDistance,
            float lowerDuration, Vector3 raiseScale)
        {
            float elapsedTime = 0;
            Vector3 originalPos = rectTransform.anchoredPosition3D;

            while (elapsedTime < raiseDuration)
            {
                elapsedTime += Time.deltaTime;
                rectTransform.anchoredPosition3D =
                    Vector3.Lerp(originalPos, raiseDistance, 1 - (raiseDuration - elapsedTime));
                rectTransform.localScale = Vector3.Lerp(Vector3.one, raiseScale, 1 - (raiseDuration - elapsedTime));

                yield return null;
            }

            elapsedTime = 0;
            while (elapsedTime < lowerDuration)
            {
                elapsedTime += Time.deltaTime;
                rectTransform.anchoredPosition3D =
                    Vector3.Lerp(raiseDistance, lowerDistance, 1 - (lowerDuration - elapsedTime));
                rectTransform.localScale = Vector3.Lerp(raiseScale, Vector3.one, 1 - (lowerDuration - elapsedTime));

                textComponent.alpha = Mathf.Lerp(1, 0, 1 - (lowerDuration - elapsedTime));

                yield return null;
            }

            Destroy(gameObject, 0.1f);
        }

    }
}