using System;
using UnityEngine;

/// <summary>
/// Calls action functions on an Actor (Jump, Move, Shoot)
/// Make controllers for specific types of Actors to be used
/// by different things like Player, AI, Network
/// </summary>
[Serializable]
public abstract class ActorController {

	protected Actor actor;

	public ActorController(Actor actor) {
		this.actor = actor;
		ControllerMaster.controllerUpdateActions.Add(ControllerUpdate);
	}

	protected virtual void ControllerUpdate() { }

	public void OnActorDeath() {
		ControllerMaster.controllerUpdateActions.Remove(ControllerUpdate);
	}
}
