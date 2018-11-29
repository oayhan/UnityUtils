using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUtility : MonoBehaviour
{
    //singleton instance
    public static TimerUtility Instance { get; private set; }

    //elapsed time for timer in seconds
    public float ElapsedTimerSeconds { get; private set; }
    
    //total duration for countdown
    public float CountdownDuration { get; private set; }
    //currently elapsed seconds for countdown
    public float ElapsedCountdownSeconds { get; private set; }
    //remaining seconds for countdown
    public float RemainingCountdownSeconds
    {
        get { return Mathf.Max(CountdownDuration - ElapsedCountdownSeconds, 0); }
    }

    //is timer counting down
    public bool IsCountdownActive { get; private set; }
    //is timer active
    public bool IsTimerActive { get; private set; }
    
    //callback function when countdown is finished
    private Action countdownFinishCallback;
    
    private void Awake()
    {
        //check for singleton component
        if (Instance != null && Instance != this)
        {
            Debug.LogError("There are two TimerUtility scripts in scene!");
            enabled = false;
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// Starts countdown timer and invokes callback when finished.
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="finishCallback"></param>
    public void StartCountdown(int seconds, Action finishCallback)
    {
        CountdownDuration = seconds;
        ElapsedCountdownSeconds = 0;
        IsCountdownActive = true;
        countdownFinishCallback = finishCallback;
    }
    
    /// <summary>
    /// Resets previous elapsed seconds and starts a new timer.
    /// </summary>
    public void StartTimer()
    {
        ElapsedTimerSeconds = 0;
        IsTimerActive = true;
    }

    /// <summary>
    /// Stops timer.
    /// </summary>
    public void StopTimer()
    {
        IsTimerActive = false;
    }

    private void Update()
    {
        if (IsCountdownActive)
        {
            //increment countdown seconds (capped at total duration)
            ElapsedCountdownSeconds = Mathf.Min(ElapsedCountdownSeconds + Time.deltaTime, CountdownDuration);

            //the countdown is finished
            if (ElapsedCountdownSeconds >= CountdownDuration)
            {
                IsCountdownActive = false;
                
                if(countdownFinishCallback != null)
                    countdownFinishCallback();

                countdownFinishCallback = null;
            }
        }

        if (IsTimerActive)
        {
            //increment timer seconds
            ElapsedTimerSeconds += Time.deltaTime;
        }
    }
}