using UnityEngine;
using UnityEngine.UI;

namespace UnityUtils
{
    public class ScreenFlasher : MonoBehaviour
    {
        [Tooltip(
            "If true, a separate UI canvas will be created. Else this will use the first UI canvas found in scene.")]
        [SerializeField]
        private bool createSeperateCanvas = true;

        //singleton instance
        public static ScreenFlasher Instance { get; private set; }

        //color to fade in
        private Color fadeColor;

        //fade in/out variables
        private float fadeInDuration;
        private float fadeOutDuration;
        private float currentFadeInTime;
        private float currentFadeOutTime;

        //fade in/out flags
        private bool isFadingIn;
        private bool isFadingOut;

        //cached flash Image component
        private Image flashImage;

        private void Awake()
        {
            //check for singleton component
            if (Instance != null && Instance != this)
            {
                Debug.LogError("There are two ScreenFlasher scripts in scene!");
                enabled = false;
                return;
            }

            Instance = this;
        }

        /// <summary>
        /// Flashes screen with given color. Half duration is used for fade in, other half is used for fade out.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="flashColor"></param>
        public void FlashScreen(float duration, Color flashColor)
        {
            FlashScreen(duration / 2, duration / 2, flashColor);
        }

        /// <summary>
        /// Flashes screen with given color, fading in and out with given durations.
        /// </summary>
        /// <param name="fadeIn"></param>
        /// <param name="fadeOut"></param>
        /// <param name="flashColor"></param>
        public void FlashScreen(float fadeIn, float fadeOut, Color flashColor)
        {
            //cache flash image
            SetFlashImage();

            //set durations
            fadeInDuration = fadeIn;
            fadeOutDuration = fadeOut;

            //set timer variables
            currentFadeInTime = 0;
            currentFadeOutTime = 0;

            //flash color
            fadeColor = flashColor;

            //set fade flags
            isFadingOut = false;
            isFadingIn = true;
        }

        /// <summary>
        /// Helper for creating/finding a canvas and caching the Image component.
        /// </summary>
        private void SetFlashImage()
        {
            //if the flash image is already cached, return
            if (flashImage != null)
                return;

            Canvas canvas = null;

            //create a new canvas if the flag is set
            if (createSeperateCanvas)
            {
                GameObject canvasObject = new GameObject("FlashUI");

                canvas = canvasObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }
            else //find a canvas in scene
            {
                canvas = FindObjectOfType<Canvas>();
            }

            if (canvas == null)
            {
                Debug.LogError("Error finding/creating a Canvas object!!!");
                return;
            }

            //create Image component, set anchor, pivot and size to stretch to screen
            GameObject imageObject = new GameObject("FlashImage");
            imageObject.AddComponent<CanvasRenderer>();
            flashImage = imageObject.AddComponent<Image>();
            flashImage.rectTransform.SetParent(canvas.transform);
            flashImage.rectTransform.anchorMin = new Vector2(0, 0);
            flashImage.rectTransform.anchorMax = new Vector2(1, 1);
            flashImage.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            flashImage.rectTransform.anchoredPosition3D = Vector3.zero;
            flashImage.rectTransform.sizeDelta = Vector3.zero;

            //set image color to transparent
            flashImage.color = Color.clear;
        }

        private void Update()
        {
            //if fade in is started
            if (isFadingIn)
            {
                //increase timer
                currentFadeInTime += Time.deltaTime;

                //lerp color from transparent to desired fade color
                flashImage.color = Color.Lerp(flashImage.color, fadeColor, currentFadeInTime / fadeInDuration);

                //set flags when fade in is finished
                if (currentFadeInTime >= fadeInDuration)
                {
                    isFadingIn = false;
                    isFadingOut = true;
                }
            }
            //if fade out is started
            else if (isFadingOut)
            {
                //increase timer
                currentFadeOutTime += Time.deltaTime;

                //lerp color from flash color to transparent
                flashImage.color = Color.Lerp(flashImage.color, Color.clear, currentFadeOutTime / fadeOutDuration);

                //set flags when fade out is finished
                if (currentFadeOutTime >= fadeOutDuration)
                {
                    isFadingOut = false;
                }
            }
        }
    }
}