using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Diagnostics;
using System;

[CustomEditor(typeof(AnomBuildMono))]
public class AnomBuildEditor : Editor {

	string exeExt;

	 public override void OnInspectorGUI () {
		//Called whenever the inspector is drawn for this object.
		DrawDefaultInspector();
		//This draws the default screen. You don't need this if you want
		//to start from scratch, but I use this when I'm just adding a button or
		//some small addition and don't feel like recreating the whole inspector.

		AnomBuildMono myScript = (AnomBuildMono)target;
 
		if (GUILayout.Button("BUILD", GUILayout.Height(25))) {
			BuildGame(myScript);
		}
	}

	void BuildGame(AnomBuildMono buildDesc) {

		// Build game(s)
		foreach (BuildTargetAnom buildTargetAnom in buildDesc.buildTargets) {
			BuildTarget buildTarget = (BuildTarget)Enum.Parse(typeof(BuildTarget), buildTargetAnom.ToString());
			if (buildTarget == BuildTarget.StandaloneWindows || buildTarget == BuildTarget.StandaloneWindows64) {
				exeExt = ".exe";
			} else {
				exeExt = "";
			}
			BuildPipeline.BuildPlayer(buildDesc.levels, buildDesc.buildsFolder + "/" +
				buildDesc.buildNumber + "/" + buildTarget.ToString() + "/" + buildDesc.exeName + exeExt,
				buildTarget, BuildOptions.None);
		}

		// Copy a file from the project folder to the build folder, alongside the built game.
		//FileUtil.CopyFileOrDirectory("Assets/WebPlayerTemplates/Readme.txt", path + "Readme.txt");

		// Run the game
		if (buildDesc.buildThenRun) {
			Process proc = new Process();
			proc.StartInfo.FileName = buildDesc.buildsFolder + "/" + buildDesc.exeName;
			proc.Start();
		}

		buildDesc.buildNumber++;
	}


}
