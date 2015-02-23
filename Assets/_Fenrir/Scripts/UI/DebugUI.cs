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

	public bool toggleAll = true;
	public bool toggleWatches = true;
	public bool toggleDebugLogs = true;
	public string watchesText;

	void Awake () {
		Inst = this;

		if (!Debug.isDebugBuild) {
			toggleAll = false;
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
		watchesText = "";
	}

	void LateUpdate() {
		watchesUIText.text = watchesText;
	}

	public static void AddWatchLine(string line) {
		if (Inst.toggleAll && Inst.toggleWatches) {
			Inst.watchesText += line + "\n";
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
