using UnityEngine;
using System.Collections;

public struct UniversePlayerDesc {
	public Vector3 innerZonePosition;
	public Zone zoneParent;
}

public class UniversePlayer : MonoBehaviour {

	public Zone zoneParent;
	public ZoneLocator zoneLocator {
		get {
			return zoneParent.zoneLocator;
		}
	}
	float sensitivity = 6f;
	float moveSpeed = 25f;

	public void Init(Zone newZoneParent) {
		zoneParent = newZoneParent;
	}

	void Awake () {
		
	}
	
	void Start () {
	
	}
	
	void Update () {
		Vector3 moveVec3 = transform.localToWorldMatrix.MultiplyVector(new Vector3(0, 0, moveSpeed));
		if (Input.GetKey(KeyCode.W)) {
			transform.position += moveVec3 * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.S)) {
			transform.position += -moveVec3 * Time.deltaTime;
		}
		


		transform.Rotate(-Input.GetAxis("Mouse Y") * sensitivity,
			Input.GetAxis("Mouse X") * sensitivity,
			0, Space.Self);

		CheckIfChangedZones();
	}

	void CheckIfChangedZones() {
		if (transform.position.x > Zone.ZONE_HALF) {
			SetNewZone(new ZoneLocator(1, 0, 0));
		} else if (transform.position.x < -Zone.ZONE_HALF) {
			SetNewZone(new ZoneLocator(-1, 0, 0));
		} else if (transform.position.y > Zone.ZONE_HALF) {
			SetNewZone(new ZoneLocator(0, 1, 0));
		} else if (transform.position.y < -Zone.ZONE_HALF) {
			SetNewZone(new ZoneLocator(0, -1, 0));
		} else if (transform.position.z > Zone.ZONE_HALF) {
			SetNewZone(new ZoneLocator(0, 0, 1));
		} else if (transform.position.z < -Zone.ZONE_HALF) {
			SetNewZone(new ZoneLocator(0, 0, -1));
		}
	}

	void SetNewZone(ZoneLocator offset) {
		zoneParent = ZoneSpawner.zoneCatalog[zoneLocator.x + offset.x,
											   zoneLocator.y + offset.y,
											   zoneLocator.z + offset.z];
		transform.position += -offset.ToVector3() * Zone.ZONE_WIDTH;
		//transform.parent = zoneParent.gameObject.transform;
		ZoneSpawner.Inst.ChangeFocusedZone(zoneParent);
	}
}
