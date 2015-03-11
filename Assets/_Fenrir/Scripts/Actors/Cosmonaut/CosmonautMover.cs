using UnityEngine;
using System.Collections;

public class CosmonautMover : CosmonautComponent {

	public Planet planet;
	public float moveForce = 1000f;

	float localYRotation;

	protected override void Awake() {
		base.Awake();
		localYRotation = transform.localEulerAngles.y;
		planet = GameObject.Find("Planet").GetComponent<Planet>();
	}

	void Start () {
	
	}

	void Update() {
		Vector3 vectorToPlanetCenter = planet.transform.position - transform.position;
		// Make capsule upright
		// Rotate to make y axis point in the opposite direction of vectorToPlanetCenter
		transform.rotation = (Quaternion.LookRotation(-vectorToPlanetCenter));
		transform.eulerAngles += new Vector3(90, 0, 0);
		LookThink();
		transform.Rotate(Vector3.up, localYRotation, Space.Self);
	}

	void FixedUpdate() {

		if (cosmonaut.humanMove.forth) {
			GetComponent<Rigidbody>().AddForce(transform.forward * moveForce);
		} else if (cosmonaut.humanMove.back) {
			GetComponent<Rigidbody>().AddForce(-transform.forward * moveForce);
		}
		if (cosmonaut.humanMove.left) {
			GetComponent<Rigidbody>().AddForce(-transform.right * moveForce);
		} else if (cosmonaut.humanMove.right) {
			GetComponent<Rigidbody>().AddForce(transform.right * moveForce);
		}
		if (cosmonaut.humanMove.jump) {
			GetComponent<Rigidbody>().AddForce(transform.up * moveForce);
		}
		GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, 8f);


		cosmonaut.humanMove.forth = false;
		cosmonaut.humanMove.back = false;
		cosmonaut.humanMove.left = false;
		cosmonaut.humanMove.right = false;
		cosmonaut.humanMove.jump = false;
	}

	void LookThink() {
		localYRotation += cosmonaut.humanMove.lookHorizontal;

		cosmonaut.humanMove.lookHorizontal = 0;
	}
}
