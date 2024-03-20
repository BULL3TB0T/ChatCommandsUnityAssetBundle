using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundles
{
    [MenuItem("Assets/Create Asset Bundles")]
    private static void BuildAllAssetBundles()
    {
        string target = "Export";
        string path = Application.dataPath + "/" + target;
        try
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            foreach (string pattern in new string[] { target, "*.manifest" })
            {
                foreach (string file in Directory.GetFiles(path, pattern)) File.Delete(file);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }
}
