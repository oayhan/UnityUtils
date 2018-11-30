using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image currentHpFill;

    [SerializeField]
    private Image hpChangeFill;

    [SerializeField]
    private Gradient healthGradient;

    [SerializeField]
    private Transform followTransform;
    
    [SerializeField]
    private Vector3 positionalOffset;

    [SerializeField]
    private Vector3 scaleChange = Vector3.one;

    [SerializeField]
    private float scaleIncreaseDuration = 0.1f;
    
    [SerializeField]
    private float scaleDereaseDuration = 0.1f;

    [SerializeField]
    private Color flashColor = Color.white;

    [SerializeField]
    [Range(0, 1)]
    private float flashDuration = 0.1f;

    [SerializeField]
    [Range(0.1f, 0.9f)]
    private float lostHpColorMultiplier = 0.6f;

    [SerializeField]
    [Range(0, 1)]
    private float lostHpFadeDelay = 0.05f;

    [SerializeField]
    [Range(0, 1)]
    private float lostHpFadeDuration = 0.1f;

    //hp variables
    private int maxHealth;
    private int currentHealth;
    private float currentHealthPercentage;
    private int latestChangeAmount;
    private float latestChangePercentage;

    private RectTransform parentCanvasTransform;
    private RectTransform rectTransform;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentCanvasTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }
    
    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;

        HealthChange(0, maxHealth, true);
    }

    public void HealthChange(int changeAmount, int currentAmount, bool cancelAnimations = false)
    {
        StopAllCoroutines();
        
        latestChangeAmount = changeAmount;
        latestChangePercentage = Mathf.Abs((float) latestChangeAmount / maxHealth);

        currentHealth = currentAmount;
        currentHealthPercentage = (float) currentHealth / maxHealth;

        currentHpFill.fillAmount = currentHealthPercentage;
        
        if (cancelAnimations)
        {
            SetFillColor();
            return;
        }

        StartCoroutine(FlashHelper());
        StartCoroutine(ScaleHelper());
        StartCoroutine(ChangeHelper());
    }

    private IEnumerator FlashHelper()
    {
        currentHpFill.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        SetFillColor();
    }

    private IEnumerator ScaleHelper()
    {
        float elapsedTime = 0;
        while (elapsedTime < scaleIncreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            
            rectTransform.localScale = Vector3.Lerp(Vector3.one, scaleChange, 1 - (scaleIncreaseDuration - elapsedTime));
            
            yield return null;
        }
        
        elapsedTime = 0;
        while (elapsedTime < scaleDereaseDuration)
        {
            elapsedTime += Time.deltaTime;
            
            rectTransform.localScale = Vector3.Lerp(scaleChange, Vector3.one, 1 - (scaleDereaseDuration - elapsedTime));
            
            yield return null;
        }
    }

    private IEnumerator ChangeHelper()
    {
        hpChangeFill.fillAmount = currentHealthPercentage + latestChangePercentage;
        hpChangeFill.color = healthGradient.Evaluate(currentHealthPercentage) * lostHpColorMultiplier;

        yield return new WaitForSeconds(lostHpFadeDelay);

        float elapsedTime = 0;
        while (elapsedTime < lostHpFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            hpChangeFill.fillAmount = currentHealthPercentage + latestChangePercentage * (lostHpFadeDuration - elapsedTime) / lostHpFadeDuration;
            
            yield return null;
        }
    }

    private void SetFillColor()
    {
        currentHpFill.color = healthGradient.Evaluate(currentHealthPercentage);
    }

    private void LateUpdate()
    {
        if (followTransform == null)
            return;

        if (Camera.current == null)
            return;
        
        // Translate our anchored position into world space.
        Vector3 worldPoint = followTransform.TransformPoint(positionalOffset);

        // Translate the world position into viewport space.
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(worldPoint);

        // Canvas local coordinates are relative to its center, 
        // so we offset by half. We also discard the depth.
        viewportPoint -= 0.5f * Vector3.one; 
        viewportPoint.z = 0;

        // Scale our position by the canvas size, 
        // so we line up regardless of resolution & canvas scaling.
        Rect rect = parentCanvasTransform.rect;
        viewportPoint.x *= rect.width;
        viewportPoint.y *= rect.height;

        // Add the canvas space offset and apply the new position.
        transform.localPosition = viewportPoint;
    }
}