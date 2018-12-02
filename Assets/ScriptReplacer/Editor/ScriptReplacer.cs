using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace UnityUtils
{
	public class ScriptReplacer : UnityEditor.AssetModificationProcessor
	{
		public static void OnWillCreateAsset(string assetPath)
		{
			//get file name without .meta
			string processedAssetPath = assetPath.Replace(".meta", "");
			//get file extension
			string assetExtension = Path.GetExtension(processedAssetPath);

			//if the file isn't C#, cancel process
			if (assetExtension != ".cs")
				return;

			//remove Assets/ from dataPath because assetPath already has it
			int index = Application.dataPath.LastIndexOf("Assets", StringComparison.Ordinal);
			string filePath = Application.dataPath.Substring(0, index) + processedAssetPath;

			//read text file
			string textFile = File.ReadAllText(filePath);

			//replace tags
			textFile = textFile.Replace("#PROJECTNAME#", new string(PlayerSettings.productName.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray()));

			//write text file
			File.WriteAllText(filePath, textFile);
			AssetDatabase.Refresh();
		}
	}
}