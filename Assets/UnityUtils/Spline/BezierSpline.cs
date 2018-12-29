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

//    public Vector3[] Points;
//
//    public int CurveCount
//    {
//        get { return (Points.Length - 1) / 3; }
//    }
//
//    private void Reset()
//    {
//        Points = new[]
//        {
//            new Vector3(0, 0, 0),
//            new Vector3(0, 1, 0),
//            new Vector3(0, 1, 1),
//            new Vector3(0, 0, 1)
//        };
//    }
//
//    public Vector3 GetPoint(float t)
//    {
//        int i;
//        if (t >= 1f) {
//            t = 1f;
//            i = Points.Length - 4;
//        }
//        else {
//            t = Mathf.Clamp01(t) * CurveCount;
//            i = (int)t;
//            t -= i;
//            i *= 3;
//        }
//
//        return transform.TransformPoint(Bezier.GetPoint(
//            Points[i], Points[i + 1], Points[i + 2], Points[i + 3], t));
//    }
//
//    public Vector3 GetVelocity(float t)
//    {
//        int i;
//        if (t >= 1f) {
//            t = 1f;
//            i = Points.Length - 4;
//        }
//        else {
//            t = Mathf.Clamp01(t) * CurveCount;
//            i = (int)t;
//            t -= i;
//            i *= 3;
//        }
//        return transform.TransformPoint(Bezier.GetFirstDerivative(
//                   Points[i], Points[i + 1], Points[i + 2], Points[i + 3], t)) - transform.position;
//    }
//
//    public Vector3 GetDirection(float t)
//    {
//        return GetVelocity(t).normalized;
//    }
//
//    public void AddCurve()
//    {
//        Vector3 point = Points[Points.Length - 1];
//        Array.Resize(ref Points, Points.Length + 3);
//        point.x += 1f;
//        Points[Points.Length - 3] = point;
//        point.x += 1f;
//        Points[Points.Length - 2] = point;
//        point.x += 1f;
//        Points[Points.Length - 1] = point;
//    }
}