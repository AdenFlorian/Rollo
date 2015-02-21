using UnityEngine;
using System.Collections;

public class DisableSelf : MonoBehaviour {
#region MonoBehaviourFunctions
	void Awake () {
		gameObject.SetActive(false);
	}
	void Start () {
	
	}
	void Update () {
	
	}
#endregion MonoBehaviourFunctions
}
