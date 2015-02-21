using UnityEngine;
using System.Collections;

public static class Rand {

	public static Vector3 RangedVector3(float min, float max) {
		Vector3 newVec3 = new Vector3();
		newVec3.x = Random.Range(min, max);
		newVec3.y = Random.Range(min, max);
		newVec3.z = Random.Range(min, max);
		return newVec3;
	}
}
