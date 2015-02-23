using UnityEngine;
using System.Collections;

public class ClientScene : MonoBehaviour {

	public Transform spawnLocation;
	public ClientNetController clientNetController;

	public static ClientScene Inst;

	Cosmonaut localCosmonaut;

	void Awake() {
		Inst = this;
	}

	void Start() {
		clientNetController.Connect();
	}
	
	void Update () {
	
	}

	public void OnGJAPIVerifyUser() {
		SetLocalNameTag(GJAPI.User.Name);
	}

	void OnConnectedToServer() {
		Rand.RandomizeSeed();
		localCosmonaut = SpawnMaster.SpawnActor<Cosmonaut>(ControlledBy.PlayerLocal,
			spawnLocation.position, new Quaternion(), netSpawn: true);
		if (GJAPI.User != null && GJAPI.User.Name != "") {
			SetLocalNameTag(GJAPI.User.Name);
		} else {
			SetLocalNameTag(Rand.StrName());
		}
	}

	void SetLocalNameTag(string newname) {
		localCosmonaut.SetNameTag(newname);
	}
}
