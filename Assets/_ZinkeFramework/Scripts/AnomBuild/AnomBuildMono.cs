using System.Diagnostics;
using UnityEngine;

public class AnomBuildMono : MonoBehaviour {

	public string exeName = "MyGame";
	public string buildsFolder = "C:\\";
	public int buildNumber = 1;
	public NetworkBuilds networkBuilds;
	public string[] clientScenes = { "Assets/__Game/Scenes/client.unity" };
	public string[] serverScenes = { "Assets/__Game/Scenes/server.unity" };
	public BuildTargetAnom[] buildClientTargets = { BuildTargetAnom.WebPlayer, BuildTargetAnom.StandaloneWindows };
	public BuildTargetAnom[] buildServerTargets = { BuildTargetAnom.StandaloneWindows };

}

public enum NetworkBuilds {
	ClientOnly,
	ServerOnly,
	ClientAndServer
}


public enum BuildTargetAnom {
	StandaloneWindows = 5,
	WebPlayer = 6,
}

/*public enum BuildTargetAnom {
	StandaloneOSXUniversal = 2,
	StandaloneOSXIntel = 4,
	StandaloneWindows = 5,
	WebPlayer = 6,
	WebPlayerStreamed = 7,
	iPhone = 9,
	PS3 = 10,
	XBOX360 = 11,
	Android = 13,
	StandaloneGLESEmu = 14,
	NaCl = 16,
	StandaloneLinux = 17,
	FlashPlayer = 18,
	StandaloneWindows64 = 19,
	MetroPlayer = 21,
	StandaloneLinux64 = 24,
	StandaloneLinuxUniversal = 25,
	WP8Player = 26,
	StandaloneOSXIntel64 = 27,
	BlackBerry = 28,
	Tizen = 29,
	PSP2 = 30,
	PS4 = 31,
	PSM = 32,
	XboxOne = 33,
	SamsungTV = 34,
}*/