using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DebugUI : MonoBehaviour {

	public static DebugUI Inst;

	public UnityEngine.UI.Text watchesUIText;
	public UnityEngine.UI.Text debugLogsUIText;
	Animator debugLogsUITextAnimator;

	public Color normalColor;
	public Color errorColor;

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

	void OnEnable() {
		Application.RegisterLogCallback(HandleLog);
	}
	void OnDisable() {
		Application.RegisterLogCallback(null);
	}

	void Update() {
		uiTextStr = "";
	}

	void LateUpdate() {
		watchesUIText.text = uiTextStr;
	}

	public static void AddWatchLine(string line) {
		if (display) {
			uiTextStr += line + "\n";
	    }
	}

	void HandleLog(string logString, string stackTrace, LogType type) {
		debugLogsUIText.text = logString;
		Color newColor = normalColor;
		switch (type) {
			case LogType.Assert:
				break;
			case LogType.Error:
				newColor = errorColor;
				break;
			case LogType.Exception:
				break;
			case LogType.Log:
				break;
			case LogType.Warning:
				break;
			default:
				break;
		}
		debugLogsUIText.color = newColor;
		if (debugLogsUITextAnimator == null) {
			debugLogsUITextAnimator = debugLogsUIText.GetComponent<Animator>();
		}
		debugLogsUITextAnimator.SetTrigger("fadeout");
	}
}
