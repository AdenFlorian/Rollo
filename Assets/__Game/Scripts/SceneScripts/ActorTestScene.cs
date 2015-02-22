using UnityEngine;
using System.Collections;

public class ActorTestScene : MonoBehaviour {

	public GameObject spawnLocation;

	void Awake() {
	
	}

	void Start() {
		Rand.RandomizeSeed();
		//SpawnMaster.SpawnActor<Cosmonaut>(ControlledBy.PlayerLocal, spawnLocation.transform.position);
		Cosmonaut newCosmonaut = SpawnMaster.SpawnActor<Cosmonaut>(spawnPos: spawnLocation.transform.position);
		newCosmonaut.InitController(ControlledBy.PlayerLocal);
		newCosmonaut.SetNameTag(Rand.StrName());
	}
	
	void Update () {
	
	}
}
