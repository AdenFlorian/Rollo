using UnityEngine;

/// <summary>
/// Main class for an Actor
/// Has public functions for actions that the Actor can perform
/// e.g. MoveForward, Jump, Shoot, etc.
/// Actions are usually called from an ActorController
/// </summary>
public abstract class Actor : MonoBehaviour {
	protected bool isSpawned = false;
	public int actorID { get; private set; }

	private static int nextID = 1;

	public void OnSpawn() {
		actorID = Actor.nextID++;
		isSpawned = true;

	}

	public virtual void InitController() {
		InitController(ControlledBy.Empty);
	}

	public virtual void InitController(ControlledBy controllerType) { }

	public virtual void Damage(float amount) { }

	public virtual void Interact() { }
}
