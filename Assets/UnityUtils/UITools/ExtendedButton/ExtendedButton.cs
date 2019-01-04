using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils;

[RequireComponent(typeof(Image))]
public class ExtendedButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Transition type for button.")]
    public ExtendedButtonTransitionType Transition;
    
    [Header("Color Transition")]
    [Tooltip("Default color for button.")]
    public Color NormalColor = Color.white;
    [Tooltip("Used when the button is hovered and not disabled.")]
    public Color HighlightedColor = Color.white;
    [Tooltip("Used when the button is pressed and not disabled.")]
    public Color PressedColor = Color.gray;
    [Tooltip("Used when the button is disabled.")]
    public Color DisabledColor = Color.gray;
    [Tooltip("Fade time between color changes.")]
    [Range(0, 2)]
    public float FadeTime = 0.1f;
    
    [Header("Text Color")]
    [Tooltip("Should text color be changed when button is enabled/disabled.")]
    public bool DisableAffectsText = true;
    [Tooltip("Default text color.")]
    public Color NormalTextColor = Color.black;
    [Tooltip("Disabled text color.")]
    public Color DisabledTextColor = Color.gray;
    
    [Header("Sprite Transition")]
    [Tooltip("Default sprite for button.")]
    public Sprite NormalSprite;
    [Tooltip("Highlighted sprite for button.")]
    public Sprite HighlightedSprite;
    [Tooltip("Pressed sprite for button.")]
    public Sprite PressedSprite;
    [Tooltip("Disabled sprite for button.")]
    public Sprite DisabledSprite;

    [Header("Audio Clips")]
    [Tooltip("Played when button is pressed.")]
    public AudioClip PressClip;
    [Tooltip("Played when button is hovered.")]
    public AudioClip HighlightClip;
    [Tooltip("Played when button is pressed while disabled.")]
    public AudioClip DisabledPressClip;

    [Header("Scale Changes")]
    [Tooltip("Scale of button when highlighted.")]
    public Vector3 HighlightedScale = Vector3.one;
    [Tooltip("Scale of button when pressed.")]
    public Vector3 PressedScale = Vector3.one;
    [Tooltip("How long it takes to finish scale change.")]
    [Range(0, 1)]
    public float ScaleTime = 0.1f;
    
    [Header("Events")]
    public UnityEvent OnHighlight;
    public UnityEvent OnPress;

    //click and hover events doesn't register when disabled
    public bool IsDisabled { get; private set; }
    //is button currently hovered over
    public bool IsHovered { get; private set; }
    //current state of button (normal, hovered, pressed or disabled)
    public ExtendedButtonState CurrentState { get; private set; }

    //size constants
    private const float ButtonWidth = 160;
    private const float ButtonHeight = 30;
    private const float TextWidth = 150;
    private const float TextHeight = 30;
    private const string ButtonText = "Button";
    private static readonly Color TextColor = Color.black;
    
    //cached components
    private Image buttonImage;
    private TMP_Text buttonText;
    private RectTransform rectTransform;
    
    //cached values
    private Vector3 defaultScale;
    private Vector3 defaultOffset;
    
    private void Start()
    {
        //cache components
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();
        
        //cache default values
        defaultScale = rectTransform.localScale;
        defaultOffset = rectTransform.anchoredPosition3D;
    }
    
    /// <summary>
    /// Enables/disables the button.
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleDisable(bool toggle)
    {
        IsDisabled = toggle;

        CurrentState = IsDisabled ? ExtendedButtonState.Disabled : ExtendedButtonState.Normal;
        
        SetButtonStyle();
    }
    
    /// <summary>
    /// Implemented for IPointerClickHandler interface. Deals with button clicks.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //if the button is disabled
        if (IsDisabled)
        {
            //play disabled clip
            if(DisabledPressClip != null)
                DisabledPressClip.PlayOneShot();
            
            //exit without doing anything
            return;
        }

        //play normal press clip
        if (PressClip != null)
            PressClip.PlayOneShot();
        
        //call OnPress UnityEvent
        if(OnPress != null)
            OnPress.Invoke();
    }

    /// <summary>
    /// Implemented for IPointerDownHandler interface. Used for setting button style when button is pressed down.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //if button is disabled, exit function
        if (IsDisabled)
            return;
        
        //set new state
        CurrentState = ExtendedButtonState.Pressed;
        
        //set button style
        SetButtonStyle();
    }

    /// <summary>
    /// Implemented for IPointerUpHandler interface. Used for setting button style when button press is finished.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //if button is disabled, exit function
        if (IsDisabled)
            return;

        //if the mouse is still over the button set the state to Highlighted, else set to Normal
        CurrentState = IsHovered ? ExtendedButtonState.Highlighted : ExtendedButtonState.Normal;
        
        //set button style
        SetButtonStyle();
    }

    /// <summary>
    /// Implemented for IPointerEnterHandler interface. Used for setting button style when button is hovered.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //if button is disabled, exit function
        if (IsDisabled)
            return;

        //set flag and state
        IsHovered = true;
        CurrentState = ExtendedButtonState.Highlighted;
        
        //play audio clip
        if(HighlightClip != null)
            HighlightClip.PlayOneShot();
        
        //call OnHighlight UnityEvent
        if(OnHighlight != null)
            OnHighlight.Invoke();
        
        //set button style
        SetButtonStyle();
    }
    
    /// <summary>
    /// Implemented for IPointerExitHandler interface. Used for setting button style when button hover is finished.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //if button is disabled, exit function
        if (IsDisabled)
            return;

        //set flag and state
        IsHovered = false;
        CurrentState = ExtendedButtonState.Normal;
        
        //set button style
        SetButtonStyle();
    }

    /// <summary>
    /// Sets visual style of button depending on Transition Type and current Button State.
    /// </summary>
    private void SetButtonStyle()
    {
        //if transition type is Color
        if (Transition == ExtendedButtonTransitionType.Color)
        {
            //change Image color to state assigned values using fade time
            switch (CurrentState)
            {
                case ExtendedButtonState.Normal:
                    buttonImage.CrossFadeColor(NormalColor, FadeTime, false, true);
                    break;
                case ExtendedButtonState.Highlighted:
                    buttonImage.CrossFadeColor(HighlightedColor, FadeTime, false, true);
                    break;
                case ExtendedButtonState.Pressed:
                    buttonImage.CrossFadeColor(PressedColor, FadeTime, false, true);
                    break;
                case ExtendedButtonState.Disabled:
                    buttonImage.CrossFadeColor(DisabledColor, FadeTime, false, true);
                    break;
            }
        }
        //if transition type is Sprite
        else if (Transition == ExtendedButtonTransitionType.Sprite)
        {
            //change Image sprite to the state assigned values
            switch (CurrentState)
            {
                case ExtendedButtonState.Normal:
                    buttonImage.sprite = NormalSprite;
                    break;
                case ExtendedButtonState.Highlighted:
                    buttonImage.sprite = HighlightedSprite;
                    break;
                case ExtendedButtonState.Pressed:
                    buttonImage.sprite = PressedSprite;
                    break;
                case ExtendedButtonState.Disabled:
                    buttonImage.sprite = DisabledSprite;
                    break;
            }
        }

        //if disabling the button is set to affect the text
        if (DisableAffectsText)
        {
            //change text color depending on button state
            if (CurrentState == ExtendedButtonState.Normal)
            {
                buttonText.color = NormalTextColor;
            }
            else if (CurrentState == ExtendedButtonState.Disabled)
            {
                buttonText.color = DisabledTextColor;
            }
        }

        //stop previous Coroutines and restart with new state 
        StopAllCoroutines();
        StartCoroutine(ChangeScale());
    }

    /// <summary>
    /// Changes button scale depending on CurrentState in ScaleTime.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeScale()
    {
        Vector3 targetScale = Vector3.one;
        Vector3 currentScale = rectTransform.localScale;
        
        switch (CurrentState)
        {
            case ExtendedButtonState.Normal:
            case ExtendedButtonState.Disabled:
                targetScale = defaultScale;
                break;
            case ExtendedButtonState.Highlighted:
                targetScale = HighlightedScale;
                break;
            case ExtendedButtonState.Pressed:
                targetScale = PressedScale;
                break;
        }
        
        float elapsedTime = 0;
        while (elapsedTime <= ScaleTime)
        {
            rectTransform.localScale = Vector3.Lerp(currentScale, targetScale, Mathf.Clamp01(1 - (ScaleTime - elapsedTime)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    //default values for fields
    private void Reset()
    {
        //set default values for sprites
        NormalSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        HighlightedSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        PressedSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        DisabledSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");

        //get file path of this script
        MonoScript monoScript = MonoScript.FromMonoBehaviour(this);
        string scriptPath = AssetDatabase.GetAssetPath(monoScript);
        //get folder path
        scriptPath = scriptPath.Replace("ExtendedButton.cs", "");
        //get Audio folder path
        string audioPath = scriptPath + "Audio/";
        
        //set default values for audio clips
        HighlightClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath + "button_highlight.mp3");
        PressClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath + "button_press.mp3");
        DisabledPressClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath + "button_disabled.mp3");
    }

    //Unity menu for adding this button
    [MenuItem("GameObject/UI/ExtendedButton",false,2002)]
    static void AddExtendedButton(MenuCommand menuCommand)
    {
        //try to get GameObject if right click menu is used
        GameObject parent = menuCommand.context as GameObject;
        //if there is no parent
        if (parent == null)
        {
            //try to find a Canvas object in scene
            Canvas canvasInScene = FindObjectOfType<Canvas>();
            //use found canvas as parent
            if (canvasInScene != null)
            {
                parent = canvasInScene.gameObject;
            }
            else
            {
                Debug.LogError("No parent object or Canvas found in scene!");
                return;
            }
        }
        
        //create parent object
        GameObject extendedButtonObject = new GameObject("ExtendedButton");
        extendedButtonObject.transform.SetParent(parent.transform, false);
        RectTransform extendedButtonTransform = extendedButtonObject.AddComponent<RectTransform>();
        extendedButtonTransform.sizeDelta = new Vector2(ButtonWidth, ButtonHeight);
        
        //add image
        Image extendedButtonImage = extendedButtonObject.AddComponent<Image>();
        extendedButtonImage.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        extendedButtonImage.type = Image.Type.Sliced;
        extendedButtonImage.color = Color.white;
        
        //create TMP_Text as a child
        GameObject childTextObject = new GameObject("Text");
        RectTransform childTextTransform = childTextObject.AddComponent<RectTransform>();
        childTextObject.transform.SetParent(extendedButtonObject.transform, false);
        TextMeshProUGUI childText = childTextObject.AddComponent<TextMeshProUGUI>();
        childText.text = ButtonText;
        childText.alignment = TextAlignmentOptions.Center;
        childText.color = TextColor;
        childTextTransform.sizeDelta = new Vector2(TextWidth, TextHeight);
        
        //add ExtendedButton
        extendedButtonObject.AddComponent<ExtendedButton>();
    }
    
    public enum ExtendedButtonState
    {
        Normal,
        Highlighted,
        Pressed,
        Disabled
    }

    public enum ExtendedButtonTransitionType
    {
        Color,
        Sprite
    }
}