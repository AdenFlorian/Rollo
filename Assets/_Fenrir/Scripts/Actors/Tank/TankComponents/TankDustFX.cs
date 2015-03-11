using UnityEngine;

public class TankDustFX : TankComponent {
	private float startEmissionRate;

	protected void Start() {
		startEmissionRate = GetComponent<ParticleSystem>().emissionRate;
	}

	protected void Update() {
		GetComponent<ParticleSystem>().emissionRate = startEmissionRate * Mathf.Abs(tank.mover.speedNormalized);
	}
}
