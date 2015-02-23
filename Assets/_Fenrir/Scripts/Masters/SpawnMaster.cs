using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMaster : MonoBehaviour {

		static Transform actorsParent;
		static Dictionary<string, Transform> actorParents = new Dictionary<string, Transform>();

		void Awake() {
			actorsParent = GameObject.Find("Actors").transform;
			if (actorsParent == null) {
				actorsParent = new GameObject("Actors").transform;
			}
		}

		public static T SpawnActor<T>(ControlledBy controller = ControlledBy.Empty,
			Vector3 spawnPos = new Vector3(), Quaternion spawnRot = new Quaternion(),
			bool netSpawn = false) where T : Actor {

			GameObject actorPrefab = Resources.Load<GameObject>(typeof(T).ToString());
			GameObject spawnedActorGO = Instantiate(actorPrefab, spawnPos, spawnRot, netSpawn);
			T newActor = spawnedActorGO.GetComponent<T>();
			newActor.OnSpawn();
			newActor.InitController(controller);
			spawnedActorGO.name += newActor.actorID;
			return newActor;
		}

		public static T[] SpawnActors<T>(int count, ControlledBy controller = ControlledBy.Empty,
			Vector3[] spawnPos = null, Quaternion[] spawnRots = null,
			bool netSpawn = false) where T : Actor {

			T[] newActors = new T[count];

			if (spawnPos == null) {
					spawnPos = new Vector3[] { new Vector3 () };
			}
			if (spawnRots == null) {
					spawnRots = new Quaternion[] { new Quaternion () };
			}
			for (int i = 0; i < count; i++) {
					var j = (int)(i - Mathf.Floor (i / spawnPos.Length) * spawnPos.Length);
					var k = (int)(i - Mathf.Floor (i / spawnRots.Length) * spawnRots.Length);
					newActors[i] = SpawnActor<T>(controller, spawnPos[j], spawnRots[k], netSpawn);
			}

			return newActors;
		}

		public static GameObject Instantiate(GameObject newGO, bool netSpawn = false) {
			return Instantiate(newGO, Vector3.zero, Quaternion.identity, netSpawn);
		}

		public static GameObject Instantiate(GameObject newGO, Vector3 position, Quaternion rotation,
			bool netSpawn = false) {

			GameObject spawnedGO;

			if (netSpawn) {
				spawnedGO = Network.Instantiate(newGO, position, rotation, 1) as GameObject;
			} else {
				spawnedGO = GameObject.Instantiate(newGO, position, rotation) as GameObject;
			}

			SetActorParent(spawnedGO);

			return spawnedGO;
		}

		private static void SetActorParent(GameObject actorGO) {
			string actorName = actorGO.name;
			Transform parGO;
			if (actorParents.TryGetValue(actorName, out parGO) == false) {
					parGO = new GameObject(actorName + "s").transform;
					parGO.transform.parent = actorsParent;
					actorParents.Add(actorName, parGO);
			}
			actorGO.transform.parent = parGO;
		}

		public static void DespawnAllActors() {
			GameObject.Destroy(actorsParent.gameObject);
			actorsParent = new GameObject("Actors").transform;
			actorParents.Clear();
		}
}

public enum ControlledBy {
		PlayerLocal,
		AI,
		Empty,
		PlayerNetwork
}
