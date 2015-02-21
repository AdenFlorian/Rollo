using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ZoneLocator {
	public int x, y, z;
	public ZoneLocator(int x, int y, int z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}
	public static ZoneLocator operator +(ZoneLocator zl1, ZoneLocator zl2) {
		return new ZoneLocator(zl1.x + zl2.x, zl1.y + zl2.y, zl1.z + zl2.z);
	}
	public static ZoneLocator operator -(ZoneLocator zl1, ZoneLocator zl2) {
		return new ZoneLocator(zl1.x - zl2.x, zl1.y - zl2.y, zl1.z - zl2.z);
	}
	public Vector3 ToVector3() {
		return new Vector3(x, y, z);
	}
	public override string ToString() {
		return x.ToString() + y.ToString() + z.ToString();
	}
}

public class Zone {

	public const float ZONE_WIDTH = 1000;
	public const float ZONE_HALF = ZONE_WIDTH / 2;

	public ZoneLocator zoneLocator;
	public List<ZoneMember> zoneMembers = new List<ZoneMember>();
	public GameObject zoneView;

	public void Init(ZoneLocator newZoneLocator) {
		zoneLocator = newZoneLocator;
	}

	void Awake () {
	
	}
	
	void Start () {
	
	}
	
	void Update () {
	
	}
}
