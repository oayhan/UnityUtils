using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    //singleton instance
    public static CameraShaker Instance { get; private set; }

    public bool IsShaking { get; private set; }
    public Vector3 PositionalShakeStrength { get; private set; }
    public Vector3 RotationalShakeStrength { get; private set; }
    public bool IsTimedShake { get; private set; }
    public float DampenRatio { get; private set; }
    public float ShakeDuration { get; private set; }
    public float ElapsedShakeDuration { get; private set; }

    public float RemainingShakeDuration
    {
        get { return ShakeDuration - ElapsedShakeDuration; }
    }

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("There are two CameraShaker scripts in scene!");
            enabled = false;
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// Starts camera shake (only positional) for given duration.
    /// </summary>
    /// <param name="duration">Duration of shake</param>
    /// <param name="strength">Positional shake strength</param>
    /// <param name="dampenRatio">Percentage of duration to dampen the shake</param>
    public void ShakeOnce(float duration, float strength, float dampenRatio = 0)
    {
        ShakeOnce(duration, Vector3.one * strength, Vector3.zero, dampenRatio);
    }

    /// <summary>
    /// Starts camera shake (both positional and rotational) for given duration
    /// </summary>
    /// <param name="duration">Duration of shake</param>
    /// <param name="positionStrength">Positional shake strength (uniform)</param>
    /// <param name="rotationStrength">Rotational shake strength (uniform)</param>
    /// <param name="dampenRatio">Percentage of duration to dampen the shake</param>
    public void ShakeOnce(float duration, float positionStrength, float rotationStrength, float dampenRatio)
    {
        ShakeOnce(duration, Vector3.one * positionStrength, Vector3.one * rotationStrength, dampenRatio);
    }

    /// <summary>
    /// Starts camera shake (both positional and rotational) for given duration
    /// </summary>
    /// <param name="duration">Duration of shake</param>
    /// <param name="positionStrength">Positional shake strength</param>
    /// <param name="rotationStrength">Rotational shake strength</param>
    /// <param name="dampenRatio">Percentage of duration to dampen the shake</param>
    public void ShakeOnce(float duration, Vector3 positionStrength, Vector3 rotationStrength, float dampenRatio)
    {
        StartShaking(positionStrength, rotationStrength, true, duration, dampenRatio);
    }

    /// <summary>
    /// Starts camera shake (both positional and rotational). Continues until StopShaking is called.
    /// </summary>
    /// <param name="positionStrength">Positional shake strength</param>
    /// <param name="rotationStrength">Rotational shake strength</param>
    public void ShakeContinuously(Vector3 positionStrength, Vector3 rotationStrength)
    {
        StartShaking(positionStrength, rotationStrength, false);
    }

    /// <summary>
    /// Helper method for all shakes.
    /// </summary>
    /// <param name="positionStrength">Positional shake strength</param>
    /// <param name="rotationStrength">Rotational shake strength</param>
    /// <param name="isTimed">Set true if the shake is tied to a timer</param>
    /// <param name="duration">Duration of the shake for timed shakes</param>
    /// <param name="dampenRatio">Percentage of duration to dampen the shake</param>
    private void StartShaking(Vector3 positionStrength, Vector3 rotationStrength, bool isTimed, float duration = 0,
        float dampenRatio = 0)
    {
        //stop previous shake
        StopShaking();
        
        //reset timer values
        ElapsedShakeDuration = 0;

        //cache original values
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;

        //set shake strength
        PositionalShakeStrength = positionStrength;
        RotationalShakeStrength = rotationStrength;

        //set shake variables
        ShakeDuration = duration;
        DampenRatio = dampenRatio;
        IsTimedShake = isTimed;

        //set shake flag
        IsShaking = true;
    }

    public void StopShaking()
    {
        //set shake flag
        IsShaking = false;

        //reset transform back to original values
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
    }

    private void Update()
    {
        if (!IsShaking)
            return;

        //how much the shake strength will be dampened
        float dampenRate = 1;

        if (IsTimedShake)
        {
            //count elapsed time
            ElapsedShakeDuration += Time.deltaTime;
            
            //apply dampen after the threshold is passed
            if (ElapsedShakeDuration / ShakeDuration > DampenRatio)
                dampenRate = Mathf.Clamp01(1 - ElapsedShakeDuration / ShakeDuration);
            
            //if shake duration is passed, stop the shake
            if(ElapsedShakeDuration >= ShakeDuration)
                StopShaking();
        }

        //calculate each shake separately
        float xPosShake = Random.Range(-1, 1) * PositionalShakeStrength.x;
        float yPosShake = Random.Range(-1, 1) * PositionalShakeStrength.y;
        float zPosShake = Random.Range(-1, 1) * PositionalShakeStrength.z;
        float xRotShake = Random.Range(-1, 1) * RotationalShakeStrength.x;
        float yRotShake = Random.Range(-1, 1) * RotationalShakeStrength.y;
        float zRotShake = Random.Range(-1, 1) * RotationalShakeStrength.z;

        //apply shakes
        transform.localPosition = new Vector3(xPosShake, yPosShake, zPosShake) * dampenRate;
        transform.localRotation = Quaternion.Euler(new Vector3(xRotShake, yRotShake, zRotShake) * dampenRate);
    }
}