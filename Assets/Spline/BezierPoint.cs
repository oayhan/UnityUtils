using System;
using UnityEngine;

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

    public Vector3 OutgoingTangent
    {
        get { return outgoingTangent; }
        set { outgoingTangent = value; }//todo: check values before setting (handle mode etc..)
    }
    private Vector3 outgoingTangent;
    

    //todo: return actual world pos
    public Vector3 WorldPosition
    {
        get { return Position; }
    }

    public enum HandleMode
    {
        Free,
        Mirrored,
        Linear
    }
}