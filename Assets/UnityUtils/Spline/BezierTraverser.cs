using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTraverser : MonoBehaviour
{
    public TraverseMode Mode;
    public TraverseMethod Method;
    public BezierSpline Spline;

    private float progress;
    
    private void Update()
    {
        progress += Time.deltaTime;

        transform.position = Spline.GetPoint(progress);
    }

    public enum TraverseMode
    {
        Once,
        Loop,
        PingPong
    }

    public enum TraverseMethod
    {
        Time,
        Speed
    }
}