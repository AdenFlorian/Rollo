using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Launch : MonoBehaviour {

	public Vector3 launchVector;
	public float startSpeed;

	void Start() {
		GetComponent<Rigidbody>().AddForce(launchVector.normalized * startSpeed,
			ForceMode.VelocityChange);
	}
}
