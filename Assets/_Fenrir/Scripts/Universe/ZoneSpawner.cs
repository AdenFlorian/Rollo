using UnityEngine;
using System.Collections;

public class ZoneSpawner : MonoBehaviour {

	public static ZoneSpawner Inst;
	public GameObject zonePrefab;
	public GameObject playerPrefab;
	public GameObject spawnedPlayer;
	public ZoneLocator focusedZone { get; private set; }

	// Number of zones in each direction to have active at any one time
	// If set to one, then there will be a cube of 27 zones
	int zoneBuffer = 2;
	int zoneBufferWidth {
		get {
			return zoneBuffer * 2 + 1;
		}
	}
	int totalSpawnedZones {
		get {
			return (int)Mathf.Pow(zoneBufferWidth, 3);
		}
	}
	Zone[,,] spawnedZones;
	int totalZonesAcross = 50;
	public static Zone[,,] zoneCatalog;

	void Awake () {
		Inst = this;
		spawnedZones = new Zone[zoneBufferWidth, zoneBufferWidth, zoneBufferWidth];
		zoneCatalog = new Zone[totalZonesAcross, totalZonesAcross, totalZonesAcross];
	}
	
	void Start () {
		for (int x = 0; x < totalZonesAcross; x++) {
			for (int y = 0; y < totalZonesAcross; y++) {
				for (int z = 0; z < totalZonesAcross; z++) {
					zoneCatalog[x, y, z] = new Zone();
					zoneCatalog[x, y, z].Init(new ZoneLocator(x, y, z));
					// Fill it with some randomly placed members
					for (int i = 0; i < 5; i++) {
						ZoneMember newZoneMember = new ZoneMember();
						newZoneMember.innerZonePosition = Rand.RangedVector3(-Zone.ZONE_HALF, Zone.ZONE_HALF);
						zoneCatalog[x, y, z].zoneMembers.Add(newZoneMember);
					}
				}
			}
		}

		// Spawn Player
		UniversePlayerDesc newUniPlayerDesc = new UniversePlayerDesc();
		// Picking kind of random zone somewhere in the middle of available zones
		newUniPlayerDesc.zoneParent = zoneCatalog[10, 8, 7];
		newUniPlayerDesc.innerZonePosition = Rand.RangedVector3(-Zone.ZONE_HALF, Zone.ZONE_HALF);
		SpawnUniPlayer(newUniPlayerDesc);
	}
	
	void Update () {
	
	}

	void SpawnUniPlayer(UniversePlayerDesc uniPlayerDesc) {
		focusedZone = uniPlayerDesc.zoneParent.zoneLocator;
		SpawnZones(GetSurroundingZones(uniPlayerDesc.zoneParent.zoneLocator));
		spawnedPlayer = Instantiate(playerPrefab, uniPlayerDesc.innerZonePosition, Quaternion.identity) as GameObject;
		spawnedPlayer.AddComponent<UniversePlayer>();
		spawnedPlayer.GetComponent<UniversePlayer>().Init(uniPlayerDesc.zoneParent);
		spawnedPlayer.transform.parent = new GameObject("Actors").transform;
	}

	void SpawnZone(Zone zone) {
		Vector3 zoneOffset = zone.zoneLocator.ToVector3() - focusedZone.ToVector3();
		spawnedZones[(int)zoneOffset.x + zoneBuffer, (int)zoneOffset.y + zoneBuffer, (int)zoneOffset.z + zoneBuffer] = zone;
		zone.zoneView = Instantiate(zonePrefab,
				Zone.ZONE_WIDTH * zoneOffset,
				Quaternion.identity) as GameObject;
		zone.zoneView.transform.localScale = Vector3.one * Zone.ZONE_WIDTH;
		zone.zoneView.GetComponentInChildren<UnityEngine.UI.Text>().text = zone.zoneLocator.ToString();
		foreach (ZoneMember member in zone.zoneMembers) {
			Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere),
				member.innerZonePosition + (Zone.ZONE_WIDTH * zoneOffset),
				Quaternion.identity);
		}
	}

	void SpawnZones(Zone[] zones) {
		for (int i = 0; i < zones.Length; i++) {
			SpawnZone(zones[i]);
		}
	}

	// Returns array of surrounding zones including center zone
	Zone[] GetSurroundingZones(ZoneLocator zoneLocator) {
		Zone[] zonesToSpawn = new Zone[totalSpawnedZones];
		int i = 0;
		for (int x = -zoneBuffer; x <= zoneBuffer; x++) {
			for (int y = -zoneBuffer; y <= zoneBuffer; y++) {
				for (int z = -zoneBuffer; z <= zoneBuffer; z++) {
					zonesToSpawn[i] = zoneCatalog[zoneLocator.x + x, zoneLocator.y + y, zoneLocator.z + z];
					i++;
				}
			}
		}
		return zonesToSpawn;
	}

	public void ChangeFocusedZone(Zone newZone) {
		ZoneLocator zoneOffset = newZone.zoneLocator - focusedZone;
		ZoneLocator oldCenterZone = focusedZone;
		// Change focused zone
		focusedZone = newZone.zoneLocator;
		// Spawn and Despawn far away zones
		// Shift spawned zones array
		Zone[, ,] oldSpawnedZones = spawnedZones.Clone() as Zone[, ,];
		Zone[, ,] newSpawnedZones = new Zone[zoneBufferWidth, zoneBufferWidth, zoneBufferWidth];
		for (int x = 0; x < zoneBufferWidth; x++) {
			for (int y = 0; y < zoneBufferWidth; y++) {
				for (int z = 0; z < zoneBufferWidth; z++) {
					if (x + zoneOffset.x < 0 || x + zoneOffset.x >= zoneBufferWidth ||
						y + zoneOffset.y < 0 || y + zoneOffset.y >= zoneBufferWidth ||
						z + zoneOffset.z < 0 || z + zoneOffset.z >= zoneBufferWidth) {
						newSpawnedZones[x, y, z] = zoneCatalog[oldCenterZone.x + zoneOffset.x + x - zoneBuffer,
															   oldCenterZone.y + zoneOffset.y + y - zoneBuffer,
															   oldCenterZone.z + zoneOffset.z + z - zoneBuffer];
						SpawnZone(newSpawnedZones[x, y, z]);
					} else if (x + -zoneOffset.x < 0 || x + -zoneOffset.x >= zoneBufferWidth ||
							   y + -zoneOffset.y < 0 || y + -zoneOffset.y >= zoneBufferWidth ||
							   z + -zoneOffset.z < 0 || z + -zoneOffset.z >= zoneBufferWidth) {
						DespawnZone(oldSpawnedZones[x, y, z]);
						newSpawnedZones[x, y, z] = oldSpawnedZones[x + zoneOffset.x, y + zoneOffset.y, z + zoneOffset.z];
						ShiftZone(newSpawnedZones[x, y, z], zoneOffset);
					} else {
						newSpawnedZones[x, y, z] = oldSpawnedZones[x + zoneOffset.x, y + zoneOffset.y, z + zoneOffset.z];
						ShiftZone(newSpawnedZones[x, y, z], zoneOffset);
					}
				}
			}
		}
		spawnedZones = newSpawnedZones;
		// Move remaining zones
		// Spawn new zones
	}

	void DespawnZone(Zone oldZone) {
		foreach (ZoneMember member in oldZone.zoneMembers) {
			Destroy(member.gameObject);
		}
		Destroy(oldZone.zoneView);
	}

	void ShiftZone(Zone shiftedZone, ZoneLocator zoneOffset) {
		shiftedZone.zoneView.transform.position += -zoneOffset.ToVector3() * Zone.ZONE_WIDTH;
		foreach (ZoneMember member in shiftedZone.zoneMembers) {
			member.gameObject.transform.position += -zoneOffset.ToVector3() * Zone.ZONE_WIDTH;
		}
	}
}
