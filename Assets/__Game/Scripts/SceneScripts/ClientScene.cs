using UnityEngine;
using System.Collections;

public class ClientScene : MonoBehaviour {

	public Transform spawnLocation;
	public ClientNetController clientNetController;

	void Awake() {
	
	}

	void Start() {
		clientNetController.Connect();
	}
	
	void Update () {
	
	}

	void OnConnectedToServer() {
		SpawnMaster.SpawnActor<Cosmonaut>(ControlledBy.PlayerLocal, spawnLocation.position, new Quaternion(), netSpawn: true);
	}
}
