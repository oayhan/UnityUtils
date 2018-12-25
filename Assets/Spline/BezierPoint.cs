using System;
using UnityEngine;

[Serializable]
public class BezierPoint
{
    public BezierPoint PreviousPoint;

    public BezierPoint NextPoint;
    
    public Vector3 Position;

    public HandleMode Handle
    {
        get { return handle; }
        set
        {
            handle = value;

            if (handle == HandleMode.Linear)
            {
                
            }
            else if (handle == HandleMode.Mirrored)
            {
                
            }
        }
    }
    private HandleMode handle;
    
    public Vector3 IncomingTangent
    {
        get { return incomingTangent; }
        set { incomingTangent = value; }//todo: check values before setting (handle mode etc..)
    }
    private Vector3 incomingTangent;

    public Vector3 IncomingTangentWorldPos
    {
        get { return Position + incomingTangent; }
        set { IncomingTangent = value - Position; }
    }

    public Vector3 OutgoingTangent
    {
        get { return outgoingTangent; }
        set { outgoingTangent = value; }//todo: check values before setting (handle mode etc..)
    }
    private Vector3 outgoingTangent;

    public Vector3 OutgoingTangentWorldPos
    {
        get { return Position + outgoingTangent; }
        set { OutgoingTangent = value - Position; }
    }

   
    public Vector3 WorldPosition
    {
        get { return ownerSpline.transform.TransformPoint(Position); }
        set { Position = ownerSpline.transform.InverseTransformPoint(value); }
    }

    private BezierSpline ownerSpline;

    public BezierPoint(BezierSpline ownerSpline, Vector3 position, Vector3 incTangent, Vector3 outTangent, BezierPoint prevPoint, BezierPoint nextPoint)
    {
        this.ownerSpline = ownerSpline;
        
        Position = position;
        IncomingTangent = incTangent;
        OutgoingTangent = outTangent;

        PreviousPoint = prevPoint;
        NextPoint = nextPoint;
    }

    public void RepositionTangents()
    {
        if (handle == HandleMode.Linear)
        {
            if (NextPoint != null)
            {
                OutgoingTangent = (NextPoint.Position - Position).normalized;
            }

            if (PreviousPoint != null)
            {
                IncomingTangent = (PreviousPoint.Position - Position).normalized;
            }
        }
        else if (handle == HandleMode.Mirrored)
        {
            OutgoingTangent = IncomingTangent * -1;
        }
    }

    public enum HandleMode
    {
        Free,
        Mirrored,
        Linear
    }
}