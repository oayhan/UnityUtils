using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //image to display current hp
    [SerializeField]
    private Image currentHpFill;

    //image to display recent hp change
    [SerializeField]
    private Image hpChangeFill;

    //gradient for changing bar color according to current hp
    [SerializeField]
    private Gradient healthGradient;

    //transform of the owner
    [SerializeField]
    private Transform followTransform;
    
    //offset for UI visual
    [SerializeField]
    private Vector3 positionalOffset;

    //scale change on health change
    [SerializeField]
    private Vector3 scaleChange = Vector3.one;

    //total duration for scale increase
    [SerializeField]
    private float scaleIncreaseDuration = 0.1f;
    
    //total duration for scale reverting back
    [SerializeField]
    private float scaleDereaseDuration = 0.1f;

    //bar color change of health change
    [SerializeField]
    private Color flashColor = Color.white;

    //how long the bar will stay on flash color
    [SerializeField]
    [Range(0, 1)]
    private float flashDuration = 0.1f;

    //color multiplier for health change bar
    [SerializeField]
    [Range(0.1f, 0.9f)]
    private float lostHpColorMultiplier = 0.6f;

    //seconds before health change bar tween starts
    [SerializeField]
    [Range(0, 1)]
    private float lostHpFadeDelay = 0.05f;

    //how long the health change bar tween takes
    [SerializeField]
    [Range(0, 1)]
    private float lostHpFadeDuration = 0.1f;

    //hp variables
    private int maxHealth;
    private int currentHealth;
    private float currentHealthPercentage;
    private int latestChangeAmount;
    private float latestChangePercentage;

    //cached transforms
    private RectTransform parentCanvasTransform;
    private RectTransform rectTransform;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentCanvasTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }
    
    /// <summary>
    /// Sets max health for percentage calculations. Also changes current health to max.
    /// </summary>
    /// <param name="maxHealth"></param>
    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;

        HealthChange(0, maxHealth, true);
    }

    /// <summary>
    /// Should be called everytime health changes. Changes fill amount and handles animation.
    /// </summary>
    /// <param name="changeAmount">Health change amount.</param>
    /// <param name="currentAmount">Current health amount.</param>
    /// <param name="cancelAnimations">Set true to skip animations (flash, scale, etc..)</param>
    public void HealthChange(int changeAmount, int currentAmount, bool cancelAnimations = false)
    {
        //stop animations from previous change
        StopAllCoroutines();
        
        //set hp change variables
        latestChangeAmount = changeAmount;
        latestChangePercentage = Mathf.Abs((float) latestChangeAmount / maxHealth);
        currentHealth = currentAmount;
        currentHealthPercentage = (float) currentHealth / maxHealth;

        //set fill amount for current HP
        currentHpFill.fillAmount = currentHealthPercentage;
        
        //if animation cancel is set
        if (cancelAnimations)
        {
            //just set the final color for bar and return
            SetFillColor();
            return;
        }

        //handle animations
        StartCoroutine(FlashHelper());
        StartCoroutine(ScaleHelper());
        StartCoroutine(HealthChangeHelper());
    }

    //flashes the bar for duration then sets normal color of bar
    private IEnumerator FlashHelper()
    {
        currentHpFill.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        SetFillColor();
    }

    //scales the bar up and down to normal
    private IEnumerator ScaleHelper()
    {
        //start scaling up until timer is reached
        float elapsedTime = 0;
        while (elapsedTime < scaleIncreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            
            //lerp the scale to finish exactly on timer
            rectTransform.localScale = Vector3.Lerp(Vector3.one, scaleChange, 1 - (scaleIncreaseDuration - elapsedTime));
            
            yield return null;
        }
        
        //start scaling down until timer is reached
        elapsedTime = 0;
        while (elapsedTime < scaleDereaseDuration)
        {
            elapsedTime += Time.deltaTime;
            
            //lerp the scale to finish exactly on timer
            rectTransform.localScale = Vector3.Lerp(scaleChange, Vector3.one, 1 - (scaleDereaseDuration - elapsedTime));
            
            yield return null;
        }
    }

    //sets health change bar fill amount and color, then disappears with tween
    private IEnumerator HealthChangeHelper()
    {
        //set health change fill amount and color
        hpChangeFill.fillAmount = currentHealthPercentage + latestChangePercentage;
        hpChangeFill.color = healthGradient.Evaluate(currentHealthPercentage) * lostHpColorMultiplier;

        yield return new WaitForSeconds(lostHpFadeDelay);

        //start disappear tween
        float elapsedTime = 0;
        while (elapsedTime < lostHpFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            hpChangeFill.fillAmount = currentHealthPercentage + latestChangePercentage * (lostHpFadeDuration - elapsedTime) / lostHpFadeDuration;
            
            yield return null;
        }
    }

    //set health bar color according to current hp and gradient
    private void SetFillColor()
    {
        currentHpFill.color = healthGradient.Evaluate(currentHealthPercentage);
    }

    private void LateUpdate()
    {
        if (followTransform == null)
            return;

        if (Camera.main == null)
            return;
        
        // translate our anchored position into world space.
        Vector3 worldPoint = followTransform.TransformPoint(positionalOffset);

        // translate the world position into viewport space.
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(worldPoint);

        // canvas local coordinates are relative to its center, 
        // so we offset by half. We also discard the depth.
        viewportPoint -= 0.5f * Vector3.one; 
        viewportPoint.z = 0;

        // scale our position by the canvas size, 
        // so we line up regardless of resolution & canvas scaling.
        Rect rect = parentCanvasTransform.rect;
        viewportPoint.x *= rect.width;
        viewportPoint.y *= rect.height;

        // add the canvas space offset and apply the new position.
        transform.localPosition = viewportPoint;
    }
}