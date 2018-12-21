using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSpline : MonoBehaviour
{
    public BezierPoint[] Points;

    public Vector3 GetPoint(float t)
    {
        if (t > Points.Length - 1)
        {
            Debug.LogError("Value t: " + t + " is out of bounds.");
            return Vector3.zero;
            //if looped
            //t = t - (Points.Length - 1);
        }

        int startIndex = Mathf.FloorToInt(t - 1);
        BezierPoint startPoint = Points[startIndex];
        BezierPoint endPoint = startPoint.NextPoint;

        return Bezier.GetPoint(startPoint.Position, startPoint.OutgoingTangent, endPoint.IncomingTangent, endPoint.Position, t - startIndex);
    }

    public Vector3 GetPointNormalized(float t)
    {
        t = Mathf.Clamp01(t);

        if (Math.Abs(t) < float.Epsilon)
            return Points[0].Position;
        else if (Math.Abs(t - 1) < float.Epsilon)
            return Points[Points.Length - 1].Position;
        
        t *= (Points.Length - 1);
        int startIndex = Mathf.FloorToInt(t);
        
        BezierPoint startPoint = Points[startIndex];
        BezierPoint endPoint = startPoint.NextPoint;

        float curveT = t - startIndex; 
        
        return Bezier.GetPoint(startPoint.Position, startPoint.OutgoingTangent, endPoint.IncomingTangent, endPoint.Position, curveT);
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