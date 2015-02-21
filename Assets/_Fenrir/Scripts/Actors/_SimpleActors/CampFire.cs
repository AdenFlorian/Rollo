using UnityEngine;
using System.Collections;

public class CampFire : Actor {

	public bool isLit = false;
	public GameObject fx;
	public ParticleSystem[] particleSystems;
	public int switchedNumTimes = 0;

	void Awake () {
	}

	void Start() {
		if (isLit) {
			Activate();
		} else {
			Deactivate();
		}
	}
	
	void Update () {
	
	}

	public void Switch() {
		if (isLit) {
			Deactivate();
		} else {
			Activate();
		}
		switchedNumTimes++;
		if (switchedNumTimes == 6) {
			GameJoltAPIManager.Inst.AwardTrophy(GJTrophyEx.StartFire);
		} else if (switchedNumTimes == 1) {
			UnityAnalyticsIntegration.Inst.SwitchedFire();
		}
	}

	public void Activate() {
		fx.SetActive(true);
		foreach (ParticleSystem partSys in particleSystems) {
			partSys.enableEmission = true;
		}
		isLit = true;
	}

	public void Deactivate() {
		fx.SetActive(false);
		foreach (ParticleSystem partSys in particleSystems) {
			partSys.enableEmission = false;
		}
		isLit = false;
	}

	public override void Interact() {
		Switch();
	}
}