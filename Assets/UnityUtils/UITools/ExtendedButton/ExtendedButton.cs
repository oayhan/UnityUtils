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
    
    [Header("Position Offsets")]
    [Tooltip("Positional offset of button when highlighted.")]
    public Vector3 HighlightedOffset = Vector3.zero;
    [Tooltip("Positional offset of button when pressed.")]
    public Vector3 PressedOffset = Vector3.zero;

    [Header("Events")]
    public UnityEvent OnHighlight;
    public UnityEvent OnPress;

    public bool IsDisabled { get; private set; }
    public bool IsHovered { get; private set; }
    public ExtendedButtonState CurrentState { get; private set; }

    //size constants
    private const float ButtonWidth = 160;
    private const float ButtonHeight = 30;
    private const float TextWidth = 150;
    private const float TextHeight = 30;
    private const string ButtonText = "Button";
    private static readonly Color TextColor = Color.black;
    
    private Image buttonImage;
    private TMP_Text buttonText;
    
    private void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<TMP_Text>();
    }
    
    public void ToggleDisable(bool toggle)
    {
        IsDisabled = toggle;

        CurrentState = IsDisabled ? ExtendedButtonState.Disabled : ExtendedButtonState.Normal;
        
        SetButtonStyle();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsDisabled)
        {
            if(DisabledPressClip != null)
                DisabledPressClip.PlayOneShot();
            
            return;
        }

        if (PressClip != null)
            PressClip.PlayOneShot();
        
        if(OnPress != null)
            OnPress.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsDisabled)
            return;
        
        CurrentState = ExtendedButtonState.Pressed;
        
        SetButtonStyle();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsDisabled)
            return;

        if (!IsHovered)
            CurrentState = ExtendedButtonState.Normal;
        else
            CurrentState = ExtendedButtonState.Highlighted;
        
        SetButtonStyle();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsDisabled)
            return;

        IsHovered = true;
        CurrentState = ExtendedButtonState.Highlighted;
        
        if(HighlightClip != null)
            HighlightClip.PlayOneShot();
        
        SetButtonStyle();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsDisabled)
            return;

        IsHovered = false;
        CurrentState = ExtendedButtonState.Normal;
        
        SetButtonStyle();
    }

    private void SetButtonStyle()
    {
        if (Transition == ExtendedButtonTransitionType.Color)
        {
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
        else if (Transition == ExtendedButtonTransitionType.Sprite)
        {
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

        if (DisableAffectsText)
        {
            if (CurrentState == ExtendedButtonState.Normal)
            {
                buttonText.color = NormalTextColor;
            }
            else if (CurrentState == ExtendedButtonState.Disabled)
            {
                buttonText.color = DisabledTextColor;
            }
        }
        
    }

    private void Reset()
    {
        NormalSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        HighlightedSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        PressedSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        DisabledSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");

        MonoScript monoScript = MonoScript.FromMonoBehaviour(this);
        string scriptPath = AssetDatabase.GetAssetPath(monoScript);
        scriptPath = scriptPath.Replace("ExtendedButton.cs", "");
        string audioPath = scriptPath + "Audio/";
        
        HighlightClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath + "button_highlight.mp3");
        PressClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath + "button_press.mp3");
        DisabledPressClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath + "button_disabled.mp3");
    }

    [MenuItem("GameObject/UI/ExtendedButton",false,2002)]
    static void AddExtendedButton(MenuCommand menuCommand)
    {
        GameObject parent = menuCommand.context as GameObject;
        if (parent == null)
        {
            Canvas canvasInScene = FindObjectOfType<Canvas>();
            if (canvasInScene != null)
                parent = canvasInScene.gameObject;
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