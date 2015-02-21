using System.Collections;
using UnityEngine;

public class Playing : GameState {

	public GameObject playerPrefab;
	public GameObject hud;
	public Transform actorsParent;
	GameObject player;

	public override void Enter() {
		// Spawn player
		player = Instantiate(playerPrefab) as GameObject;
		player.transform.parent = actorsParent;
		hud.SetActive(true);
	}

	public override void Exit() {
		// Destroy player
		Destroy(player);
		hud.SetActive(false);
	}

	public override void StateUpdate() {
		if (Input.GetKeyDown(KeyCode.Q)) {
			ReturnState(gameStates["Main"]);
		}
	}
}