using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class AnomBuildWindow : EditorWindow {

	string exeName = "MyGame";
	string buildsFolder = "C:\\";
	bool groupEnabled;
	bool buildThenRun = false;
	int buildNumber = 1;
	//public string[] levels = new string[buildNumber];
	//public List<string> levels = { "Assets/_Fenrir/Scenes/Main.unity" };
	public List<string> levels = new List<string>();
	BuildTarget[] buildTargets = { BuildTarget.WebPlayer, BuildTarget.StandaloneWindows };

	SerializedObject mySerObj;

	// Add menu item
	[MenuItem("Window/AnomBuild")]
	[MenuItem("Anomalus/AnomBuild")]
	public static void ShowWindow() {
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(AnomBuildWindow));
	}

	void OnEnable() {
		//mySerObj = new SerializedObject(levels);
	}

	void OnGUI() {
		GUILayout.Label("Build Number: " + buildNumber.ToString());

		GUILayout.Label("Scenes:", EditorStyles.boldLabel);
		for (int i = 0; i < levels.Count; i++) {
			levels[i] = GUILayout.TextField(levels[i]);
		}
		if (GUILayout.Button("+ Add Scene", GUILayout.Width(100))) {
			levels.Add("Assets/");
		}

		GUILayout.Label("Build Targets:", EditorStyles.boldLabel);
		for (int i = 0; i < buildTargets.Length; i++) {
			GUILayout.Label(buildTargets[i].ToString());
		}

		if (GUILayout.Button("Select Builds Folder", GUILayout.Width(120))) {
			buildsFolder = EditorUtility.SaveFolderPanel("Choose Builds Folder", "", "");
		}
		EditorGUILayout.TextField("Builds Folder", buildsFolder);

		if (GUILayout.Button("Build", GUILayout.Width(100))) {
			BuildGame();
		}
	}


	void BuildGame() {

		// Build game(s)
		foreach (BuildTarget buildTarget in buildTargets) {
			BuildPipeline.BuildPlayer(levels.ToArray(), buildsFolder + "/" +
				buildNumber + "/" + buildTarget.ToString() + "/" + exeName,
				buildTarget, BuildOptions.None);
		}

		// Copy a file from the project folder to the build folder, alongside the built game.
		//FileUtil.CopyFileOrDirectory("Assets/WebPlayerTemplates/Readme.txt", path + "Readme.txt");

		// Run the game
		if (buildThenRun) {
			Process proc = new Process();
			proc.StartInfo.FileName = buildsFolder + "/" + exeName;
			proc.Start();
		}

		buildNumber++;
	}

	void ReadBuildNumber() {

	}

	void WriteBuildNumber() {

	}
}