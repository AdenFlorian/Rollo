using UnityEngine;
using System.Collections;

public class CosmonautCameraController : CosmonautComponent {

	float cameraXRotation = 0f;

	void Start () {
	
	}

	void Update() {
		cameraXRotation += cosmonaut.humanMove.lookVertical;
		cosmonaut.camera.transform.localRotation = Quaternion.Euler(new Vector3(cameraXRotation, 0, 0));

		cosmonaut.humanMove.lookVertical = 0;
	}
}
