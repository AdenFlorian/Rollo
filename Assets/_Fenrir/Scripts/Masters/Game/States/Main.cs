using System.Collections;
using UnityEngine;

public class Main : GameState {

	public GameObject mainMenu;

	// Called when entering state
	public override void Enter() {
		// Activate main menu
		mainMenu.SetActive(true);
	}

	// Called when leaving state
	public override void Exit() {
		// Deactivate main menu
		mainMenu.SetActive(false);
	}

	// Return a GameState object to switch to that state
	// Get the GameState object from the gameContext's gameStates array
	public override void StateUpdate() {
	}

	public void OnClickPlay() {
		ReturnState(gameStates["Playing"]);
	}
}