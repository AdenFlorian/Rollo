using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Diagnostics;
using System;

[CustomEditor(typeof(AnomBuildMono))]
public class AnomBuildEditor : Editor {

	string fileExtension;

	public override void OnInspectorGUI () {
		DrawDefaultInspector();

		AnomBuildMono myScript = (AnomBuildMono)target;
 
		if (GUILayout.Button("BUILD", GUILayout.Height(25))) {
			BuildGame(myScript);
		}
	}

	 void BuildGame(AnomBuildMono buildDesc) {

		 BuildClients(buildDesc);
		 BuildServers(buildDesc);

		 // Increase build number somehow...
	 }

	 void BuildClients(AnomBuildMono buildDesc) {
		 PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "GAMECLIENT");

		 if (buildDesc.networkBuilds == NetworkBuilds.ClientOnly ||
			 buildDesc.networkBuilds == NetworkBuilds.ClientAndServer) {

			 foreach (BuildTargetAnom buildTargetAnom in buildDesc.buildClientTargets) {
				 BuildTarget buildTarget = ParseBuildTarget(buildTargetAnom);
				 if (buildTarget == BuildTarget.StandaloneWindows ||
					 buildTarget == BuildTarget.StandaloneWindows64) {
					 fileExtension = ".exe";
				 } else {
					 fileExtension = "";
				 }
				 BuildPipeline.BuildPlayer(buildDesc.clientScenes, buildDesc.buildsFolder + "/" +
					 buildDesc.buildNumber + "/" + "Clients" + "/" + buildTarget.ToString() + "/" +
					 buildDesc.exeName + fileExtension,
					 buildTarget, BuildOptions.None);

				 // Copy a file from the project folder to the build folder, alongside the built game.
				 //FileUtil.CopyFileOrDirectory("Assets/WebPlayerTemplates/Readme.txt", path + "Readme.txt");
			 }
		 }
	 }

	 void BuildServers(AnomBuildMono buildDesc) {
		 PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "GAMESERVER");

		 if (buildDesc.networkBuilds == NetworkBuilds.ServerOnly ||
			 buildDesc.networkBuilds == NetworkBuilds.ClientAndServer) {

			 foreach (BuildTargetAnom buildTargetAnom in buildDesc.buildServerTargets) {
				 BuildTarget buildTarget = ParseBuildTarget(buildTargetAnom);
				 if (buildTarget == BuildTarget.StandaloneWindows ||
					 buildTarget == BuildTarget.StandaloneWindows64) {
					 fileExtension = ".exe";
				 } else {
					 fileExtension = "";
				 }
				 BuildPipeline.BuildPlayer(buildDesc.serverScenes, buildDesc.buildsFolder + "/" +
					 buildDesc.buildNumber + "/" + "Servers" + "/" + buildTarget.ToString() + "/" +
					 buildDesc.exeName + "Server" + fileExtension,
					 buildTarget, BuildOptions.None);

				 // Copy a file from the project folder to the build folder, alongside the built game.
				 //FileUtil.CopyFileOrDirectory("Assets/WebPlayerTemplates/Readme.txt", path + "Readme.txt");
			 }
		 }
	 }

	 BuildTarget ParseBuildTarget(BuildTargetAnom buildTargetAnom) {
		 return (BuildTarget)Enum.Parse(typeof(BuildTarget), buildTargetAnom.ToString());
	 }
}
