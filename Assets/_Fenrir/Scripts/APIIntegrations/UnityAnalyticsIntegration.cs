using UnityEngine;
using System.Collections;
using UnityEngine.Cloud.Analytics;
using System.Collections.Generic;

public class UnityAnalyticsIntegration : MonoBehaviour {

	public static UnityAnalyticsIntegration Inst;
	bool fireSwitched = false;

	void Awake () {
		Inst = this;
	}
	
	void Start () {
		const string projectId = "69699155-9f27-4f3c-9a2d-31dbc0b3f2a5";
		UnityAnalytics.StartSDK(projectId);
	}
	
	void Update () {
	
	}

	public void SwitchedFire() {
		if (!fireSwitched) {
			Debug.Log("Switched Fire!");
			UnityAnalytics.CustomEvent("SwitchedFire", new Dictionary<string, object>());
			fireSwitched = true;
		}
	}
}
