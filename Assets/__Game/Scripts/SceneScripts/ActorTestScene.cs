using UnityEngine;
using System.Collections;

public class ActorTestScene : MonoBehaviour {

	public GameObject spawnLocation;

	void Awake() {
	
	}

	void Start() {
		//SpawnMaster.SpawnActor<Cosmonaut>(ControlledBy.PlayerLocal, spawnLocation.transform.position);
		SpawnMaster.SpawnActor<Cosmonaut>(spawnPos: spawnLocation.transform.position);
	}
	
	void Update () {
	
	}
}
