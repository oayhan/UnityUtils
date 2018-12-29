using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PackageExporter : MonoBehaviour
{
    public static class ProjectInitializer
    {
        private static readonly string[] ListOfFolders =
        {
            "Assets/UnityUtils/CameraUtilities", "Assets/UnityUtils/ConstantGenerator", "Assets/UnityUtils/LazyLoadBehaviour", "Assets/UnityUtils/ProjectInitializer", "Assets/UnityUtils/ScreenFlasher",
            "Assets/UnityUtils/ScriptReplacer", "Assets/UnityUtils/SpawnAnimation", "Assets/UnityUtils/Spline", "Assets/UnityUtils/TimerUtility", "Assets/UnityUtils/UITools", "Assets/UnityUtils/UnityExtensions", 
            "Assets/UnityUtils/UnityHelpers"
        };

        [MenuItem("Window/Unity Utils/Export Package")]
        public static void ExportPackages()
        {
            AssetDatabase.ExportPackage(ListOfFolders, Application.dataPath + "/UnityUtils.unitypackage", ExportPackageOptions.Recurse);
        }
    }
}