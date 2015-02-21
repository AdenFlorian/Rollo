using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMaster : MonoBehaviour {

	public static List<Action> controllerUpdateActions = new List<Action>();

	void Awake() {

	}

	void Update() {
		foreach (Action controllerUpdateAction in controllerUpdateActions) {
			controllerUpdateAction.Invoke();
		}
	}
}
