using UnityEngine;
using System.Collections;

public class ServerScene : MonoBehaviour {

	public ServerNetController serverNetController;

	void Awake() {
	
	}

	void Start () {
		serverNetController.Init();
	}
	
	void Update () {
	
	}
}
