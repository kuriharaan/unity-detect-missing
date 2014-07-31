using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MissingDetection : EditorWindow
{
	[MenuItem("Tools/MissingReference")]
	static void Init()
	{
		SearchDirectories();
	}

	static void SearchDirectories()
	{
		string folderPath = Application.dataPath;

		string[] fs = Directory.GetFiles(@folderPath, "*.prefab", System.IO.SearchOption.AllDirectories);
		foreach (string s in fs)
		{
			DetectMissingInMetaFile(s);
		}
	}

	static void DetectMissingInMetaFile(string filename)
	{
		System.IO.StreamReader reader = new System.IO.StreamReader(filename);

		while (reader.Peek() > -1)
		{
			string[] arr = reader.ReadLine().Split(' ');

			if (2 >= arr.Length)
			{
				continue;
			}

			for (int i = 0; i < arr.Length - 1; ++i)
			{
				if ("guid:" == arr[i])
				{
					string guid = arr[i + 1].Trim(',');

					if (guid.StartsWith("0000"))
					{
						//exception  e.g. Sprites-Default material
						continue;
					}

					string assetPath = AssetDatabase.GUIDToAssetPath(guid);

					if (string.IsNullOrEmpty(assetPath))
					{
						Debug.LogWarning(filename + " has Missing reference  ( guild:" + guid + " )");
						break;
					}
				}
			}
		}

		reader.Close();
	}
}
