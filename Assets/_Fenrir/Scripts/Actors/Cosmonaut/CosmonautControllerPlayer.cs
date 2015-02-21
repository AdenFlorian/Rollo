using UnityEngine;
using System.Collections;

public class CosmonautControllerPlayer : CosmonautController {

	void Update() {
		// Actions
		if (ActionMaster.GetAction(ActionCode.MoveForward)) {
			cosmonaut.MoveForward();
		}
		if (ActionMaster.GetAction(ActionCode.MoveBackward)) {
			cosmonaut.MoveBackward();
		}
		if (ActionMaster.GetAction(ActionCode.TurnLeft)) {
			cosmonaut.StrafeLeft();
		}
		if (ActionMaster.GetAction(ActionCode.TurnRight)) {
			cosmonaut.StrafeRight();
		}
		if (ActionMaster.GetAction(ActionCode.Jump)) {
			cosmonaut.Jump();
		}

		// Axes
		cosmonaut.LookHorizontal(ActionMaster.GetAxis(AxisCode.LookHorizontal));
		cosmonaut.LookVertical(ActionMaster.GetAxis(AxisCode.LookVertical));
	}
}
