using UnityEngine;
using System.Collections;

public class Version : MonoBehaviour {

	public string versionStr;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		DebugUI.AddWatchLine(versionStr);
	}
}
