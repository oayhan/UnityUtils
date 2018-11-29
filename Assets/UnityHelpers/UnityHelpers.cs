using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
