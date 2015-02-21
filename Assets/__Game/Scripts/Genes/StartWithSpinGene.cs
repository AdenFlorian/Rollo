using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class StartWithSpinGene : MonoBehaviour {

	public Vector3 torque;

	void Start () {
		rigidbody.AddTorque(torque, ForceMode.VelocityChange);
	}
}
