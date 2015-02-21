using UnityEngine;
using System.Collections;

public class Interactor : ActorComponent {

	public float rayLength = 3.5f;
	Actor focusedObject;
	
	void Update () {
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, rayLength)) {
			focusedObject = hitInfo.collider.GetComponent<Actor>();
		} else {
			focusedObject = null;
		}
		if (ActionMaster.GetActionDown(ActionCode.Interact)) {
			if (focusedObject) {
				focusedObject.Interact();
			}
		}
	}
}
