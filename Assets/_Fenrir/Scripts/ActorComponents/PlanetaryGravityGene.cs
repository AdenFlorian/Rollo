using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlanetaryGravityGene : MonoBehaviour {

	Planet planet;
	Planet[] planets;

	void Awake() {
	
	}

	void Start () {
		planet = GameObject.Find("Planet").GetComponent<Planet>();
	}

	void Update() {
	}

	void FixedUpdate() {
		// Fall towards planet
		if (planet != null) {
			Vector3 vectorToPlanetCenter = planet.transform.position - transform.position;
			rigidbody.AddForce(vectorToPlanetCenter.normalized * planet.gravityAcceleration, ForceMode.Acceleration);
		}
	}
}
