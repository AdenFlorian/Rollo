using UnityEngine;
using System.Collections;
using System;

public enum CosmonautControllerType {
	Player,
	AI
}

public class Cosmonaut : Actor {

	public ControlledBy overrideControllerIfEmpty;

	public GameObject cameraSlot;
	public UnityEngine.UI.Text nametagUIText;

	public CosmonautController controller { get; private set; }
	public new Camera camera { get; private set; }
	public HumanoidMove humanMove { get; private set; }

	void Awake() {
		humanMove = new HumanoidMove();
	}

	void Start () {
		if (controller == null) {
			InitController();
		}
	}

	public override void InitController(ControlledBy controllerType) {
		if (controllerType == ControlledBy.Empty) {
			controllerType = overrideControllerIfEmpty;
		}
		switch (controllerType) {
			case ControlledBy.PlayerLocal:
				controller = gameObject.AddComponent<CosmonautControllerPlayer>();
				AttachCamera();
				break;
			case ControlledBy.AI:
				throw new NotImplementedException();
				//break;
			case ControlledBy.Empty:
				controller = gameObject.AddComponent<CosmonautController>();
				break;
			case ControlledBy.PlayerNetwork:
				controller = gameObject.AddComponent<CosmonautController>();
				break;
			default:
				controller = gameObject.AddComponent<CosmonautController>();
				break;
		}
		System.Diagnostics.Debug.Assert(controller != null);
	}

	void Update() {

	}

	void LateUpdate() {
	}

	void AttachCamera() {
		GameObject cosmoCameraPrefab = Resources.Load<GameObject>("CosmoCamera");
		GameObject newCameraGO = GameObject.Instantiate(cosmoCameraPrefab) as GameObject;
		camera = newCameraGO.GetComponent<Camera>();
		newCameraGO.transform.parent = cameraSlot.transform;
		newCameraGO.transform.ZeroLocalPosition();
	}

	#region actions
	public void MoveForward() {
		humanMove.forth = true;
	}

	public void MoveBackward() {
		humanMove.back = true;
	}

	public void StrafeLeft() {
		humanMove.left = true;
	}

	public void StrafeRight() {
		humanMove.right = true;
	}

	public void Jump() {
		humanMove.jump = true;
	}

	public void LookHorizontal(float degrees) {
		humanMove.lookHorizontal = degrees;
	}

	public void LookVertical(float degrees) {
		humanMove.lookVertical = degrees;
	}

	[RPC]
	void SetNameTagRPC(string nametag) {
		nametagUIText.text = nametag;
	}

	public void SetNameTag(string nametag) {
		networkView.RPC("SetNameTagRPC", RPCMode.AllBuffered, nametag);
	}
	#endregion
}

[Serializable]
public class HumanoidMove {
	public HumanoidMove() {
		Clear();
	}

	public bool forth;
	public bool back;
	public bool left;
	public bool right;
	public bool jump;
	public float lookHorizontal;
	public float lookVertical;

	public void Clear() {
		forth = false;
		back = false;
		left = false;
		right = false;
		jump = false;
		lookHorizontal = 0;
		lookVertical = 0;
	}
}
