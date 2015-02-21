using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Game : MonoBehaviour {

	GameState gameState;
	public Dictionary<string, GameState> gameStates { get; private set; }

#region MonoBehaviourFunctions
	void Awake() {
		LoadStates();
	}

	void Start() {
		SwitchToState(gameStates["Main"]);
	}

	void Update () {
		gameState.StateUpdate();
	}
#endregion

	void LoadStates() {
		gameStates = new Dictionary<string, GameState>();
		GameState[] loadedGameStates = GetComponentsInChildren<GameState>();
		foreach (GameState loadedGameState in loadedGameStates) {
			gameStates.Add(loadedGameState.GetType().Name, loadedGameState);
			loadedGameState.gameContext = this;
		}
	}

	public void SwitchToState(GameState newState) {
		if (gameState != null) {
			gameState.Exit();
		}
		gameState = newState;
		gameState.Enter();
	}

	public void Pause() {
		Time.timeScale = 0;
	}

	public void Resume() {
		Time.timeScale = 1;
	}
}
