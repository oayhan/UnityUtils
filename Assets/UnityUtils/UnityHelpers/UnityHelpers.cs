using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityUtils
{
    public static class UnityHelpers
    {
        /// <summary>
        /// Returns a formatted string HH:MM:SS or HH:MM:SS:mmm for given seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="useMilliseconds"></param>
        /// <returns></returns>
        public static string GetStringFromSeconds(float seconds, bool useMilliseconds = false)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);

            string timeString;
            
            if (useMilliseconds)
            {
                timeString = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                    t.Hours,
                    t.Minutes,
                    t.Seconds,
                    t.Milliseconds);
            }
            else
            {
                timeString = string.Format("{0:D2}:{1:D2}:{2:D2}",
                    t.Hours,
                    t.Minutes,
                    t.Seconds);
            }

            return timeString;
        }

        /// <summary>
        /// Converts a world position to a local position inside UI canvas.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="worldOffset"></param>
        /// <param name="canvasTransform"></param>
        /// <returns></returns>
        public static Vector3 WorldPositionToUiPos(Transform transform, Vector3 worldOffset, RectTransform canvasTransform)
        {
            // translate our anchored position into world space.
            Vector3 worldPoint = transform.TransformPoint(worldOffset);

            // translate the world position into viewport space.
            Vector3 viewportPoint = Camera.main.WorldToViewportPoint(worldPoint);

            // canvas local coordinates are relative to its center, 
            // so we offset by half. We also discard the depth.
            viewportPoint -= 0.5f * Vector3.one; 
            viewportPoint.z = 0;

            // scale our position by the canvas size, 
            // so we line up regardless of resolution & canvas scaling.
            Rect rect = canvasTransform.rect;
            viewportPoint.x *= rect.width;
            viewportPoint.y *= rect.height;

            // return local position according to canvas
            return viewportPoint;
        }

        /// <summary>
        /// Returns a Vector3 with random values. Limits are defined by -absLimitValue and +absLimitValue.
        /// </summary>
        /// <param name="absLimitValue"></param>
        /// <returns></returns>
        public static Vector3 GetRandomVector3(Vector3 absLimitValue)
        {
            Vector3 absoluteVector = new Vector3(Mathf.Abs(absLimitValue.x), Mathf.Abs(absLimitValue.y), Mathf.Abs(absLimitValue.z));

            return GetRandomVector3(-absoluteVector, absoluteVector);
        }
        
        /// <summary>
        /// Returns a Vector3 with random values between minValue and maxValue.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static Vector3 GetRandomVector3(Vector3 minValue, Vector3 maxValue)
        {
            Vector3 randomVector;

            randomVector.x = Random.Range(minValue.x, maxValue.x);
            randomVector.y = Random.Range(minValue.y, maxValue.y);
            randomVector.z = Random.Range(minValue.z, maxValue.z);

            return randomVector;
        }

        /// <summary>
        /// Returns value for a PlayerPref key. If the key isn't found, sets default value (0 for int) and returns it.
        /// </summary>
        /// <param name="playerPrefKey"></param>
        /// <returns></returns>
        public static int GetPlayerPrefsIntOrDefault(string playerPrefKey)
        {
            if (PlayerPrefs.HasKey(playerPrefKey))
            {
                return PlayerPrefs.GetInt(playerPrefKey);
            }
            else
            {
                PlayerPrefs.SetInt(playerPrefKey, 0);
                return 0;
            }
        }

        /// <summary>
        /// Returns value for a PlayerPref key. If the key isn't found, sets default value ("" for string) and returns it.
        /// </summary>
        /// <param name="playerPrefKey"></param>
        /// <returns></returns>
        public static string GetPlayerPrefsStringOrDefault(string playerPrefKey)
        {
            if (PlayerPrefs.HasKey(playerPrefKey))
            {
                return PlayerPrefs.GetString(playerPrefKey);
            }
            else
            {
                PlayerPrefs.GetString(playerPrefKey, "");
                return "";
            }
        }
        
        /// <summary>
        /// Returns value for a PlayerPref key. If the key isn't found, sets default value (0 for int) and returns it.
        /// </summary>
        /// <param name="playerPrefKey"></param>
        /// <returns></returns>
        public static float GetPlayerPrefsFloatOrDefault(string playerPrefKey)
        {
            if (PlayerPrefs.HasKey(playerPrefKey))
            {
                return PlayerPrefs.GetFloat(playerPrefKey);
            }
            else
            {
                PlayerPrefs.GetFloat(playerPrefKey, 0);
                return 0;
            }
        }
    }
}
