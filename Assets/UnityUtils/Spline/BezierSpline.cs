using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSpline : MonoBehaviour
{
    public BezierPoint[] Points;

    public Vector3 GetPoint(float t)
    {
        t = Mathf.Clamp(t, 0, Points.Length - 1);

        int startIndex = GetStartingPoint(t);
        BezierPoint startPoint = Points[startIndex];
        BezierPoint endPoint = startPoint.NextPoint;

        return Bezier.GetPoint(startPoint.Position, startPoint.OutgoingTangent, endPoint.IncomingTangent, endPoint.Position, t - startIndex);
    }

    public Vector3 GetPointNormalized(float t)
    {
        t = Mathf.Clamp01(t);
        
        int startIndex = GetStartingPointNormalized(t);
        
        BezierPoint startPoint = Points[startIndex];
        BezierPoint endPoint = startPoint.NextPoint;

        t *= (Points.Length - 1);
        float curveT = t - startIndex; 
        
        return Bezier.GetPoint(startPoint.Position, startPoint.OutgoingTangent, endPoint.IncomingTangent, endPoint.Position, curveT);
    }

    public Vector3 GetTangent(float t)
    {
        t = Mathf.Clamp(t, 0, Points.Length - 1);

        int startIndex = GetStartingPoint(t);
        BezierPoint startPoint = Points[startIndex];
        BezierPoint endPoint = startPoint.NextPoint;

        return Bezier.GetFirstDerivative(startPoint.Position, startPoint.OutgoingTangent, endPoint.IncomingTangent, endPoint.Position, t - startIndex);
    }

    public Vector3 GetTangentNormalized(float t)
    {
        t = Mathf.Clamp01(t);
        
        int startIndex = GetStartingPointNormalized(t);
        
        BezierPoint startPoint = Points[startIndex];
        BezierPoint endPoint = startPoint.NextPoint;

        t *= (Points.Length - 1);
        float curveT = t - startIndex; 
        
        return Bezier.GetFirstDerivative(startPoint.Position, startPoint.OutgoingTangent, endPoint.IncomingTangent, endPoint.Position, curveT);
    }

    private int GetStartingPoint(float t)
    {
        t = Mathf.Clamp(t, 0, Points.Length - 1);
        
        return Mathf.FloorToInt(t);
    }
    
    private int GetStartingPointNormalized(float t)
    {
        t = Mathf.Clamp01(t);
        
        if (Math.Abs(t) < float.Epsilon)
            return 0;
        else if (Math.Abs(t - 1) < float.Epsilon)
            return Points.Length - 1;
        
        t *= (Points.Length - 1);
        return Mathf.FloorToInt(t);
    }

    public void AddNewPoint()
    {
        BezierPoint lastPoint = Points[Points.Length - 1];

        BezierPoint newPoint = new BezierPoint(this, lastPoint.Position + lastPoint.OutgoingTangent.normalized,
            lastPoint.IncomingTangent, lastPoint.OutgoingTangent, lastPoint, null);
        Array.Resize(ref Points, Points.Length + 1);
        Points[Points.Length - 1] = newPoint;
    }

    private void Reset()
    {
        Points = new BezierPoint[2];
        
        Points[0] = new BezierPoint(this, new Vector3(-1, -1, 0), new Vector3(-0.2f, -0.5f, 0), new Vector3(0.2f, 0.5f, 0), null, null);
        Points[1] = new BezierPoint(this, new Vector3(1, 1, 0), new Vector3(-0.7f, -0.5f, 0), new Vector3(0.7f,0.5f,0), Points[0], null);
    }
}