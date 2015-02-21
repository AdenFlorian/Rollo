using UnityEngine;
using System.Collections;

public class NetPlayerController : MonoBehaviour {

	MonoBehaviour[] disableIfNotMine;

	void Awake() {
	
	}

	void Start () {
		if (!networkView.isMine) {
			
		}
	}
	
	void Update () {
	
	}
}
