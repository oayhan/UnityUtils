using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityUtils
{
    public static class UnityExtensions
    {
        #region Transform

        #region Position

        #region Set World Position

        /// <summary>
        /// Sets x coordinate for world position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="xPos"></param>
        public static void SetPosX(this Transform transform, float xPos)
        {
            Vector3 tempPos = transform.position;
            tempPos.x = xPos;
            transform.position = tempPos;
        }

        /// <summary>
        /// Sets y coordinate for world position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="yPos"></param>
        public static void SetPosY(this Transform transform, float yPos)
        {
            Vector3 tempPos = transform.position;
            tempPos.y = yPos;
            transform.position = tempPos;
        }

        /// <summary>
        /// Sets z coordinate for world position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="zPos"></param>
        public static void SetPosZ(this Transform transform, float zPos)
        {
            Vector3 tempPos = transform.position;
            tempPos.z = zPos;
            transform.position = tempPos;
        }

        /// <summary>
        /// Resets world coordinate for this transform.
        /// </summary>
        /// <param name="transform"></param>
        public static void ResetPosition(this Transform transform)
        {
            transform.position = Vector3.zero;
        }

        #endregion

        #region Shift World Pos

        /// <summary>
        /// Shifts x coordinate of world position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="offsetX"></param>
        public static void ShiftPositionX(this Transform transform, float offsetX)
        {
            Vector3 tempPos = transform.position;
            tempPos.x += offsetX;
            transform.position = tempPos;
        }

        /// <summary>
        /// Shifts y coordinate of world position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="offsetY"></param>
        public static void ShiftPositionY(this Transform transform, float offsetY)
        {
            Vector3 tempPos = transform.position;
            tempPos.y += offsetY;
            transform.position = tempPos;
        }

        /// <summary>
        /// Shifts z coordinate of world position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="offsetZ"></param>
        public static void ShiftPositionZ(this Transform transform, float offsetZ)
        {
            Vector3 tempPos = transform.position;
            tempPos.z += offsetZ;
            transform.position = tempPos;
        }

        #endregion

        #region Set Local Position

        /// <summary>
        /// Sets x coordinate for local position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="xPos"></param>
        public static void SetLocalPosX(this Transform transform, float xPos)
        {
            Vector3 tempPos = transform.localPosition;
            tempPos.x = xPos;
            transform.localPosition = tempPos;
        }

        /// <summary>
        /// Sets x coordinate for local position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="yPos"></param>
        public static void SetLocalPosY(this Transform transform, float yPos)
        {
            Vector3 tempPos = transform.localPosition;
            tempPos.y = yPos;
            transform.localPosition = tempPos;
        }
        
        /// <summary>
        /// Sets x coordinate for local position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="zPos"></param>
        public static void SetLocalPosZ(this Transform transform, float zPos)
        {
            Vector3 tempPos = transform.localPosition;
            tempPos.z = zPos;
            transform.localPosition = tempPos;
        }

        /// <summary>
        /// Resets local position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        public static void ResetLocalPosition(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
        }

        #endregion

        #region Shift Local Position

        /// <summary>
        /// Shifts x coordinate of local position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="offsetX"></param>
        public static void ShiftLocalPositionX(this Transform transform, float offsetX)
        {
            Vector3 tempPos = transform.localPosition;
            tempPos.x += offsetX;
            transform.localPosition = tempPos;
        }

        /// <summary>
        /// Shifts y coordinate of local position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="offsetY"></param>
        public static void ShiftLocalPositionY(this Transform transform, float offsetY)
        {
            Vector3 tempPos = transform.localPosition;
            tempPos.y += offsetY;
            transform.localPosition = tempPos;
        }

        /// <summary>
        /// Shifts z coordinate of local position of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="offsetZ"></param>
        public static void ShiftLocalPositionZ(this Transform transform, float offsetZ)
        {
            Vector3 tempPos = transform.localPosition;
            tempPos.z += offsetZ;
            transform.localPosition = tempPos;
        }

        #endregion

        #endregion

        #region Rotation

        #region Set World Rotation

        /// <summary>
        /// Sets x angle of euler angles (world rotation) for this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="xRot"></param>
        public static void SetRotX(this Transform transform, float xRot)
        {
            Vector3 tempRot = transform.eulerAngles;
            tempRot.x = xRot;
            transform.rotation = Quaternion.Euler(tempRot);
        }

        /// <summary>
        /// Sets y angle of euler angles (world rotation) for this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="yRot"></param>
        public static void SetRotY(this Transform transform, float yRot)
        {
            Vector3 tempRot = transform.eulerAngles;
            tempRot.y = yRot;
            transform.rotation = Quaternion.Euler(tempRot);
        }

        /// <summary>
        /// Sets z angle of euler angles (world rotation) for this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="zRot"></param>
        public static void SetRotZ(this Transform transform, float zRot)
        {
            Vector3 tempRot = transform.eulerAngles;
            tempRot.z = zRot;
            transform.rotation = Quaternion.Euler(tempRot);
        }

        /// <summary>
        /// Resets world rotation of this transform.
        /// </summary>
        /// <param name="transform"></param>
        public static void ResetWorldRotation(this Transform transform)
        {
            transform.rotation = Quaternion.identity;
        }

        #endregion

        #region Set Local Rotation

        /// <summary>
        /// Sets x angle of euler angles (local rotation) for this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="xRot"></param>
        public static void SetLocalRotX(this Transform transform, float xRot)
        {
            Vector3 tempRot = transform.localRotation.eulerAngles;
            tempRot.x = xRot;
            transform.localRotation = Quaternion.Euler(tempRot);
        }

        /// <summary>
        /// Sets y angle of euler angles (local rotation) for this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="yRot"></param>
        public static void SetLocalRotY(this Transform transform, float yRot)
        {
            Vector3 tempRot = transform.localRotation.eulerAngles;
            tempRot.y = yRot;
            transform.localRotation = Quaternion.Euler(tempRot);
        }

        /// <summary>
        /// Sets z angle of euler angles (local rotation) for this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="zRot"></param>
        public static void SetLocalRotZ(this Transform transform, float zRot)
        {
            Vector3 tempRot = transform.localRotation.eulerAngles;
            tempRot.z = zRot;
            transform.localRotation = Quaternion.Euler(tempRot);
        }
        
        /// <summary>
        /// Resets local rotation of this transform.
        /// </summary>
        /// <param name="transform"></param>
        public static void ResetLocalRotation(this Transform transform)
        {
            transform.localRotation = Quaternion.identity;
        }

        #endregion

        #endregion

        #region Scale

        /// <summary>
        /// Sets local x scale of this transform. 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="xScale"></param>
        public static void SetLocalScaleX(this Transform transform, float xScale)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.x = xScale;
            transform.localScale = tempScale;
        }

        /// <summary>
        /// Sets local y scale of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="yScale"></param>
        public static void SetLocalScaleY(this Transform transform, float yScale)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.y = yScale;
            transform.localScale = tempScale;
        }

        /// <summary>
        /// Sets local z scale of this transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="zScale"></param>
        public static void SetLocalScaleZ(this Transform transform, float zScale)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.z = zScale;
            transform.localScale = tempScale;
        }

        /// <summary>
        /// Resets local scale of this transform.
        /// </summary>
        /// <param name="transform"></param>
        public static void ResetLocalScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }

        #endregion

        #endregion

        #region Vector

        #region Vector2 to Vector3

        /// <summary>
        /// Returns a Vector3 by using a float for x and a Vector2 for y,z.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector3 GetVector3WithX(this Vector2 v, float x)
        {
            return new Vector3(x, v.x, v.y);
        }

        /// <summary>
        /// Returns a Vector3 by using a float for y and a Vector2 for z,z.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Vector3 GetVector3WithY(this Vector2 v, float y)
        {
            return new Vector3(v.x, y, v.y);
        }
        
        /// <summary>
        /// Returns a Vector3 by using a float for z and a Vector2 for x,y.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 GetVector3WithZ(this Vector2 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        #endregion

        #region Vector3 Set

        /// <summary>
        /// Returns a Vector3 with a new x value.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector3 WithX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }

        /// <summary>
        /// Returns a Vector3 with a new y value.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Vector3 WithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }

        /// <summary>
        /// Returns a Vector3 with a new z value.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 WithZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        #endregion

        #region Closest Point on Line

        /// <summary>
        /// Returns the closest point on a finite line from this point.
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="lineStart">Start position for line</param>
        /// <param name="lineEnd">End position for line</param>
        /// <returns></returns>
        public static Vector3 ClosestPointOnFiniteLine(this Vector3 originPoint, Vector3 lineStart, Vector3 lineEnd)
        {
            Vector3 line = lineEnd - lineStart;
            float lineLength = line.magnitude;
            Vector3 normalizedLine = line.normalized;

            Vector3 vectorFromOriginToStart = originPoint - lineStart;
            float projection = Vector3.Dot(vectorFromOriginToStart, normalizedLine);
            float distanceOnLine = Mathf.Clamp(projection, 0, lineLength);
            
            return lineStart + distanceOnLine * normalizedLine;
        }
        
        /// <summary>
        /// Returns the closest point on an infinite line from this point. 
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="pointOnLine">A random point on line</param>
        /// <param name="lineDirection">Direction of the line</param>
        /// <returns></returns>
        public static Vector3 ClosestPointOnInfiniteLine(this Vector3 originPoint, Vector3 pointOnLine, Vector3 lineDirection)
        {
            Vector3 normalizedLineDir = lineDirection.normalized;
            Vector3 vectorFromPointToOrigin = originPoint - pointOnLine;

            float projection = Vector3.Dot(vectorFromPointToOrigin, normalizedLineDir);
            
            return pointOnLine + projection * normalizedLineDir;
        }

        #endregion

        #endregion

        #region Generic List

        /// <summary>
        /// Shuffles this list randomly.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int randomIndex = Random.Range(0, n);
                T value = list[randomIndex];
                list[randomIndex] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Returns a random item from this list.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static T GetRandomItem<T>(this IList<T> list)
        {
            if (list.Count == 0)
                throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
            
            return list[Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Returns a random item after removing it from the list.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static T PopRandomItem<T>(this IList<T> list)
        {
            if (list.Count == 0)
                throw new System.IndexOutOfRangeException("Cannot pop a random item from an empty list");

            int randomIndex = Random.Range(0, list.Count);
            T randomItem = list[randomIndex];
            list.RemoveAt(randomIndex);

            return randomItem;
        }

        #endregion

        #region Enum

        /// <summary>
        /// Return description string from attribute [Description("")] on enum values
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            DescriptionAttribute[] da =
                (DescriptionAttribute[]) (value.GetType().GetField(value.ToString())).GetCustomAttributes(
                    typeof(DescriptionAttribute), false);
            return da.Length != 0 ? da[0].Description : value.ToString();
        }

        #endregion
    }
}