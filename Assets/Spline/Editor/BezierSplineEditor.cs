using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierSpline))]
public class BezierSplineEditor : Editor
{
    private const float HandleSize = 0.04f;
    private const float PickSize = 0.06f;

    private BezierSpline spline;

    private Transform splineTransform;

    private int selectedPointIndex;

    private SplineEditorSelection handleSelection = SplineEditorSelection.PositionHandle;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        spline = target as BezierSpline;
        if (spline == null)
            return;

//        if (GUILayout.Button("Add Curve"))
//        {
//            Undo.RecordObject(spline, "Add Curve");
//            EditorUtility.SetDirty(spline);
//            spline.AddCurve();
//        }
    }

    private void OnSceneGUI()
    {
        spline = target as BezierSpline;
        if (spline == null)
            return;

        splineTransform = spline.transform;

        ShowBezierPoints();
        ShowBezierCurves();
    }

    private void ShowBezierCurves()
    {
        for (int i = 0; i < spline.Points.Length - 1; i++)
        {
            Handles.DrawBezier(spline.Points[i].Position, spline.Points[i + 1].Position,
                spline.Points[i].OutgoingTangent, spline.Points[i + 1].IncomingTangent, Color.white, null, 2f);
        }
    }

    private void ShowBezierPoints()
    {
        for (int i = 0; i < spline.Points.Length; i++)
        {
            ShowBezierPoint(i);
        }
    }

    private void ShowBezierPoint(int index)
    {
        Handles.color = Color.white;
        float size = HandleUtility.GetHandleSize(spline.Points[index].WorldPosition);

        Handles.DrawLine(spline.Points[index].WorldPosition, spline.Points[index].IncomingTangentWorldPos);
        if (Handles.Button(spline.Points[index].IncomingTangentWorldPos, Quaternion.identity, HandleSize * size, PickSize * size,
            Handles.DotHandleCap))
        {
            selectedPointIndex = index;
            handleSelection = SplineEditorSelection.IncomingTangentHandle;
        }
        
        if (Handles.Button(spline.Points[index].WorldPosition, Quaternion.identity, HandleSize * size, PickSize * size,
            Handles.DotHandleCap))
        {
            selectedPointIndex = index;
            handleSelection = SplineEditorSelection.PositionHandle;
        }
        
        Handles.DrawLine(spline.Points[index].WorldPosition, spline.Points[index].OutgoingTangentWorldPos);
        if (Handles.Button(spline.Points[index].OutgoingTangentWorldPos, Quaternion.identity, HandleSize * size, PickSize * size,
            Handles.DotHandleCap))
        {
            selectedPointIndex = index;
            handleSelection = SplineEditorSelection.OutgoingTangentHandle;
        }

        if (selectedPointIndex == index)
        {
            Vector3 handlePos = spline.Points[index].WorldPosition;
            if (handleSelection == SplineEditorSelection.IncomingTangentHandle)
                handlePos = spline.Points[index].IncomingTangentWorldPos;
            else if (handleSelection == SplineEditorSelection.OutgoingTangentHandle)
                handlePos = spline.Points[index].OutgoingTangentWorldPos;
            
            Quaternion handleRot =
                Tools.pivotRotation == PivotRotation.Local ? splineTransform.rotation : Quaternion.identity;
            
            EditorGUI.BeginChangeCheck();
            handlePos = Handles.DoPositionHandle(handlePos, handleRot);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Spline Point Change");
                EditorUtility.SetDirty(spline);
                
                if(handleSelection == SplineEditorSelection.PositionHandle)
                    spline.Points[index].WorldPosition = handlePos;
                else if(handleSelection == SplineEditorSelection.IncomingTangentHandle)
                    spline.Points[index].IncomingTangentWorldPos = handlePos;
                else if(handleSelection == SplineEditorSelection.OutgoingTangentHandle)
                    spline.Points[index].OutgoingTangentWorldPos = handlePos;
            }
        }
    }

//
//    private const int stepsPerCurve = 10;
//
//    private void ShowDirections()
//    {
//        Handles.color = Color.green;
//        Vector3 point = spline.GetPoint(0f);
//        Handles.DrawLine(point, point + spline.GetDirection(0f) * 1);
//        int steps = stepsPerCurve * spline.CurveCount;
//        for (int i = 1; i <= steps; i++)
//        {
//            point = spline.GetPoint(i / (float) steps);
//            Handles.DrawLine(point, point + spline.GetDirection(i / (float) steps) * 1);
//        }
//    }
//
//    private void ShowPoints()
//    {
//        for (int i = 0; i < spline.Points.Length; i++)
//        {
//            ShowPoint(i);
//        }
//    }
//
//    private Vector3 ShowPoint(int pointIndex)
//    {
//        Vector3 handlePos = splineTransform.TransformPoint(spline.Points[pointIndex]);
//        Quaternion handleRot = rotationMode == PivotRotation.Local ? splineTransform.rotation : Quaternion.identity;
//
//        Handles.color = Color.white;
//        float size = HandleUtility.GetHandleSize(handlePos);
//        if (Handles.Button(handlePos, handleRot, size * handleSize, size * pickSize, Handles.DotHandleCap))
//        {
//            selectedIndex = pointIndex;
//        }
//
//        if (selectedIndex == pointIndex)
//        {
//            EditorGUI.BeginChangeCheck();
//            handlePos = Handles.DoPositionHandle(handlePos, handleRot);
//            if (EditorGUI.EndChangeCheck())
//            {
//                Undo.RecordObject(spline, "Spline Point Change");
//                EditorUtility.SetDirty(spline);
//                spline.Points[pointIndex] = splineTransform.InverseTransformPoint(handlePos);
//            }
//        }
//
//        return handlePos;
//    }

    public enum SplineEditorSelection
    {
        PositionHandle,
        IncomingTangentHandle,
        OutgoingTangentHandle
    }
}