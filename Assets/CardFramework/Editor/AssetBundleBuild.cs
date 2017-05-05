using UnityEditor;
using UnityEngine;

public class AssetBundleBuild
{

	[MenuItem("Assets/Build All Bundles OSX")]	
	static public void BuildAllBundlesOSX() 
	{
		BuildAssetBundleOptions assetBundleOptions = BuildAssetBundleOptions.UncompressedAssetBundle;
		BuildPipeline.BuildAssetBundles(DirectoryUtility.ExternalAssets(), assetBundleOptions, BuildTarget.StandaloneOSXIntel);
	}
	
}
