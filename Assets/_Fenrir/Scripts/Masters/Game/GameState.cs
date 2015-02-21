using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameState : MonoBehaviour {

	Game _gameContext;
	public Game gameContext {
		get {
			return _gameContext;
		}
		set {
			gameStates = value.gameStates;
			_gameContext = value;
		}
	}

	protected Dictionary<string, GameState> gameStates { get; private set; }

	public abstract void Enter();
	public abstract void Exit();
	public abstract void StateUpdate();

	protected void ReturnState(GameState newState) {
		gameContext.SwitchToState(newState);
	}
}