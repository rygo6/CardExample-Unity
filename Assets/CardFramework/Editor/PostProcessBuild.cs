#define postprocess

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using System.IO;

public class PostProcessBuild 
{
	
	#if postprocess
	
	/// <summary>
	/// Copies the ExternalAssets Directory into the build.
	/// </summary>
	[PostProcessBuild]
	static public void Process(BuildTarget target, string pathToBuiltProject) 
	{
		if (target == BuildTarget.iOS)
		{
			DirectoryCopy(DirectoryUtility.ExternalAssets(), pathToBuiltProject + "/Data/ExternalAssets", true);
		}
		else if (target == BuildTarget.StandaloneOSXIntel || target == BuildTarget.StandaloneOSXIntel64 || target == BuildTarget.StandaloneOSXUniversal)
		{
			DirectoryCopy(DirectoryUtility.ExternalAssets(), pathToBuiltProject + "/Contents/ExternalAssets", true);
		}	
		else if (target == BuildTarget.StandaloneWindows || target == BuildTarget.StandaloneWindows64)
		{
			string dataFolder = System.IO.Path.GetFileNameWithoutExtension(pathToBuiltProject) + "_Data";
			DirectoryCopy(DirectoryUtility.ExternalAssets(), pathToBuiltProject + "/../" + dataFolder + "/ExternalAssets", true);
		}
	}
	
	static public void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
	{
		DirectoryInfo dir = new DirectoryInfo(sourceDirName);
		DirectoryInfo[] dirs = dir.GetDirectories();
		
		// If the source directory does not exist, throw an exception.
		if (!dir.Exists)
		{
			throw new DirectoryNotFoundException(
				"Source directory does not exist or could not be found: "
				+ sourceDirName);
		}
		
		// If the destination directory does not exist, create it.
		if (!Directory.Exists(destDirName))
		{
			Directory.CreateDirectory(destDirName);
		}
		
		// Get the file contents of the directory to copy.
		FileInfo[] files = dir.GetFiles();
		
		foreach (FileInfo file in files)
		{
			// Create the path to the new copy of the file.
			string temppath = Path.Combine(destDirName, file.Name);
			Debug.Log("Copying: " + temppath);
			// Copy the file.
			file.CopyTo(temppath, false);
		}
		
		// If copySubDirs is true, copy the subdirectories.
		if (copySubDirs)
		{
			foreach (DirectoryInfo subdir in dirs)
			{
				// Create the subdirectory.
				string temppath = Path.Combine(destDirName, subdir.Name);
				
				// Copy the subdirectories.
				DirectoryCopy(subdir.FullName, temppath, copySubDirs);
			}
		}
	}		
	
	#endif
	
}
