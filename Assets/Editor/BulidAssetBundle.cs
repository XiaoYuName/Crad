using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BulidAssetBundle
{
    [@MenuItem("ExportAsset/BulidBundle")]
    public static void BulidBundle()
    {
        string dir = "AssetBundle";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}
