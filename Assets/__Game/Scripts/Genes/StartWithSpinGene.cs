using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class StartWithSpinGene : MonoBehaviour {

	public Vector3 torque;

	void Start () {
		GetComponent<Rigidbody>().AddTorque(torque, ForceMode.VelocityChange);
	}
}
