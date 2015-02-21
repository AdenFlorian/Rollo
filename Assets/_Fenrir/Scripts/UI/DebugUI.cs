using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DebugUI : MonoBehaviour {

	public static DebugUI Inst;
	public UnityEngine.UI.Text uiText;
	public static bool display = false;
	public static string uiTextStr;

	void Awake () {
		Inst = this;

		if (!Debug.isDebugBuild) {
			display = false;
		}
	}
	
	void Start () {
	}

	void Update() {
		uiTextStr = "";
	}

	void LateUpdate() {
		uiText.text = uiTextStr;
	}

	public static void AddLine(string line) {
		if (display) {
			uiTextStr += line + "\n";
	    }
	}
}
