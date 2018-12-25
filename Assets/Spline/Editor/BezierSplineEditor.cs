using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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

        //show reorderable list

        if (GUILayout.Button("Add Point"))
        {
            Undo.RecordObject(spline, "Add Curve");
            EditorUtility.SetDirty(spline);
            spline.AddNewPoint();
        }

        //show selected node values
        if (spline.Points[selectedPointIndex] != null)
        {
            EditorGUI.BeginChangeCheck();
            BezierPoint.HandleMode handleType = (BezierPoint.HandleMode) EditorGUILayout.EnumPopup("Tangent Handle",
                spline.Points[selectedPointIndex].Handle);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Handle Change");
                EditorUtility.SetDirty(spline);
                
                spline.Points[selectedPointIndex].Handle = handleType;
            }
        }
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
            Handles.DrawBezier(spline.Points[i].WorldPosition, spline.Points[i + 1].WorldPosition,
                spline.Points[i].OutgoingTangentWorldPos, spline.Points[i + 1].IncomingTangentWorldPos, Color.white,
                null, 2f);
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
        float size = HandleUtility.GetHandleSize(spline.Points[index].WorldPosition);

        Handles.color = Color.gray;
        Handles.DrawLine(spline.Points[index].WorldPosition, spline.Points[index].IncomingTangentWorldPos);

        Handles.color = Color.white;
        if (Handles.Button(spline.Points[index].IncomingTangentWorldPos, Quaternion.identity, HandleSize * size,
            PickSize * size,
            Handles.DotHandleCap))
        {
            selectedPointIndex = index;
            handleSelection = SplineEditorSelection.IncomingTangentHandle;
            Repaint();
        }

        if (Handles.Button(spline.Points[index].WorldPosition, Quaternion.identity, HandleSize * size, PickSize * size,
            Handles.DotHandleCap))
        {
            selectedPointIndex = index;
            handleSelection = SplineEditorSelection.PositionHandle;
            Repaint();
        }

        Handles.color = Color.gray;
        Handles.DrawLine(spline.Points[index].WorldPosition, spline.Points[index].OutgoingTangentWorldPos);

        Handles.color = Color.white;
        if (Handles.Button(spline.Points[index].OutgoingTangentWorldPos, Quaternion.identity, HandleSize * size,
            PickSize * size,
            Handles.DotHandleCap))
        {
            selectedPointIndex = index;
            handleSelection = SplineEditorSelection.OutgoingTangentHandle;
            Repaint();
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

                if (handleSelection == SplineEditorSelection.PositionHandle)
                    spline.Points[index].WorldPosition = handlePos;
                else if (handleSelection == SplineEditorSelection.IncomingTangentHandle)
                    spline.Points[index].IncomingTangentWorldPos = handlePos;
                else if (handleSelection == SplineEditorSelection.OutgoingTangentHandle)
                    spline.Points[index].OutgoingTangentWorldPos = handlePos;
            }
        }
    }

    public enum SplineEditorSelection
    {
        PositionHandle,
        IncomingTangentHandle,
        OutgoingTangentHandle
    }
}