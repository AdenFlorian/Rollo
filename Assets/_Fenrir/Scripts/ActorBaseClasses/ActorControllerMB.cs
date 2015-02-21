using UnityEngine;
using System.Collections;

public class ActorControllerMB : MonoBehaviour {

	protected Actor actor;

	protected virtual void Awake() {
		actor = GetComponent<Actor>();
		System.Diagnostics.Debug.Assert(actor != null);
	}
}
