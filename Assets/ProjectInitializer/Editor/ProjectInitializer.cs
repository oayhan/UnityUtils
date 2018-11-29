using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityUtils
{
    public static class ProjectInitializer
    {
        //modify this array to change folders to create
        private static readonly string[] InitialFolders =
        {
            "Animations", "Audio", "Models", "Packages", "Plugins", "Prefabs", "Resources", "Scenes", "Scripts",
            "Shaders", "Skyboxes", "Terrains", "Textures"
        };

        private static readonly string DefaultCompanyName = "Omer";

        [MenuItem("Window/Unity Utils/Initialize Project")]
        public static void InitializeProject()
        {
            CreateDirectories();
            SetPlayerSettings();
            SetEditorSettings();
        }

        //checks if each folder is created, creates if not
        private static void CreateDirectories()
        {
            string initialPath = Application.dataPath + "/";

            foreach (string initialFolder in InitialFolders)
            {
                if (!Directory.Exists(initialPath + initialFolder))
                    Directory.CreateDirectory(initialPath + initialFolder);
            }

            AssetDatabase.Refresh();
            Debug.Log("Missing directories created.");
        }

        //sets company name to default company name
        private static void SetPlayerSettings()
        {
            PlayerSettings.companyName = DefaultCompanyName;
        }

        //sets source control settings to conform to git
        private static void SetEditorSettings()
        {
            EditorSettings.externalVersionControl = "Visible Meta Files";
            EditorSettings.serializationMode = SerializationMode.ForceText;
        }
    }
}