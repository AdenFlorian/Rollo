using UnityEngine;
using System.Collections;

public class CosmonautComponent : ActorComponent {

	protected Cosmonaut cosmonaut;

	protected override void Awake() {
		base.Awake();

		cosmonaut = actor as Cosmonaut;
	}
}
