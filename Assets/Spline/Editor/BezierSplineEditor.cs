using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierSpline))]
public class BezierSplineEditor : Editor
{
    private BezierSpline spline;
    private Transform splineTransform;
//    private PivotRotation rotationMode;
//
//    private const float handleSize = 0.04f;
//    private const float pickSize = 0.06f;
//
//    private int selectedIndex = -1;

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

//    private void OnSceneGUI()
//    {
//        spline = target as BezierSpline;
//        if (spline == null)
//            return;
//
//        splineTransform = spline.transform;
//        rotationMode = Tools.pivotRotation;
//
//        ShowPoints();
//
//        Vector3 p0 = ShowPoint(0);
//        for (int i = 1; i < spline.Points.Length; i += 3)
//        {
//            Vector3 p1 = ShowPoint(i);
//            Vector3 p2 = ShowPoint(i + 1);
//            Vector3 p3 = ShowPoint(i + 2);
//
//            Handles.color = Color.gray;
//            Handles.DrawLine(p0, p1);
//            Handles.DrawLine(p2, p3);
//
//            Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
//            p0 = p3;
//        }
//
//        ShowDirections();
//    }
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
}