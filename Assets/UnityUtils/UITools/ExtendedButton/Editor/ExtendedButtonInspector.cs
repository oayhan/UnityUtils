using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExtendedButton))]
[CanEditMultipleObjects]
public class ExtendedButtonInspector : Editor
{
    public SerializedProperty
        TransitionProperty,
        NormalColorProperty,
        HighlightedColorProperty,
        PressedColorProperty,
        DisabledColorProperty,
        FadeTimeProperty,
        DisableAffectsTextProperty,
        NormalTextColorProperty,
        DisabledTextColorProperty,
        NormalSpriteProperty,
        HighlightedSpriteProperty,
        PressedSpriteProperty,
        DisabledSpriteProperty,
        PressClipProperty,
        HighlightClipProperty,
        DisabledPressClipProperty,
        HighlightedScaleProperty,
        PressedScaleProperty,
        ScaleTimeProperty,
        OnHighlightProperty,
        OnPressProperty;

    //cache all properties
    private void OnEnable()
    {
        TransitionProperty = serializedObject.FindProperty("Transition");
        
        NormalColorProperty = serializedObject.FindProperty("NormalColor");
        HighlightedColorProperty = serializedObject.FindProperty("HighlightedColor");
        PressedColorProperty = serializedObject.FindProperty("PressedColor");
        DisabledColorProperty = serializedObject.FindProperty("DisabledColor");
        FadeTimeProperty = serializedObject.FindProperty("FadeTime");
        
        DisableAffectsTextProperty = serializedObject.FindProperty("DisableAffectsText");
        NormalTextColorProperty = serializedObject.FindProperty("NormalTextColor");
        DisabledTextColorProperty = serializedObject.FindProperty("DisabledTextColor");
        
        NormalSpriteProperty = serializedObject.FindProperty("NormalSprite");
        HighlightedSpriteProperty = serializedObject.FindProperty("HighlightedSprite");
        PressedSpriteProperty = serializedObject.FindProperty("PressedSprite");
        DisabledSpriteProperty = serializedObject.FindProperty("DisabledSprite");
        
        PressClipProperty = serializedObject.FindProperty("PressClip");
        HighlightClipProperty = serializedObject.FindProperty("HighlightClip");
        DisabledPressClipProperty = serializedObject.FindProperty("DisabledPressClip");
        
        HighlightedScaleProperty = serializedObject.FindProperty("HighlightedScale");
        PressedScaleProperty = serializedObject.FindProperty("PressedScale");
        ScaleTimeProperty = serializedObject.FindProperty("ScaleTime");
        
        OnHighlightProperty = serializedObject.FindProperty("OnHighlight");
        OnPressProperty = serializedObject.FindProperty("OnPress");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //transition type
        EditorGUILayout.PropertyField(TransitionProperty);

        //get value of transition type
        ExtendedButton.ExtendedButtonTransitionType transitionType =
            (ExtendedButton.ExtendedButtonTransitionType) TransitionProperty.intValue;

        //display transition values according to type
        switch (transitionType)
        {
            case ExtendedButton.ExtendedButtonTransitionType.Color:
                EditorGUILayout.PropertyField(NormalColorProperty, new GUIContent("Normal Color"));
                EditorGUILayout.PropertyField(HighlightedColorProperty, new GUIContent("Highlighted Color"));
                EditorGUILayout.PropertyField(PressedColorProperty, new GUIContent("Pressed Color"));
                EditorGUILayout.PropertyField(DisabledColorProperty, new GUIContent("Disabled Color"));
                EditorGUILayout.PropertyField(FadeTimeProperty, new GUIContent("Fade Time"));
                break;
            case ExtendedButton.ExtendedButtonTransitionType.Sprite:
                EditorGUILayout.PropertyField(NormalSpriteProperty, new GUIContent("Normal Sprite"));
                EditorGUILayout.PropertyField(HighlightedSpriteProperty, new GUIContent("Highlighted Sprite"));
                EditorGUILayout.PropertyField(PressedSpriteProperty, new GUIContent("Pressed Sprite"));
                EditorGUILayout.PropertyField(DisabledSpriteProperty, new GUIContent("Disabled Sprite"));
                break;
        }
        
        //text color values
        EditorGUILayout.PropertyField(DisableAffectsTextProperty, new GUIContent("Disable Affects Text"));
        if (DisableAffectsTextProperty.boolValue)
        {
            EditorGUILayout.PropertyField(NormalTextColorProperty, new GUIContent("Normal Text Color"));
            EditorGUILayout.PropertyField(DisabledTextColorProperty, new GUIContent("Disabled Text Color"));
        }

        //audio clips
        EditorGUILayout.PropertyField(PressClipProperty, new GUIContent("Press Clip"));
        EditorGUILayout.PropertyField(HighlightClipProperty, new GUIContent("Highlight Clip"));
        EditorGUILayout.PropertyField(DisabledPressClipProperty, new GUIContent("Disabled Press Clip"));
        
        //scale changes
        EditorGUILayout.PropertyField(HighlightedScaleProperty, new GUIContent("Highlighted Scale"));
        EditorGUILayout.PropertyField(PressedScaleProperty, new GUIContent("Pressed Scale"));
        EditorGUILayout.PropertyField(ScaleTimeProperty, new GUIContent("Scale Time"));

        //UnityEvents
        EditorGUILayout.PropertyField(OnHighlightProperty, new GUIContent("On Highlight"));
        EditorGUILayout.PropertyField(OnPressProperty, new GUIContent("On Press"));
        
        serializedObject.ApplyModifiedProperties();
    }
}