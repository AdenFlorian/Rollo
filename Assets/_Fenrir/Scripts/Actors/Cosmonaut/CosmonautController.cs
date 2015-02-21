using UnityEngine;
using System.Collections;
using System;

public class CosmonautController : ActorControllerMB {

	protected Cosmonaut cosmonaut;

	protected override void Awake() {
		base.Awake();
		cosmonaut = actor as Cosmonaut;
	}
}
