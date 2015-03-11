using UnityEngine;
using System.Collections;

/// <summary>
/// Client and Server
/// </summary>
[RequireComponent(typeof(NetworkView))]
public class NetInfo : MonoBehaviour {

	public static NetInfo Inst;

	public static int connectedPlayerCount { get; private set; }
	public static bool connected { get; private set; }
	public static bool isServer { get; private set; }
	public static bool isClient { get; private set; }

	void Awake() {
		Inst = this;
	}

	void Update() {
		if (isClient && connected && connectedPlayerCount > 0) {
			DebugUI.AddWatchLine("PLAYERS: " + connectedPlayerCount);
		}
	}

	// Server Functions
	public void IncrementPlayerCount() {
		GetComponent<NetworkView>().RPC("SetPlayerCountRPC", RPCMode.Others, ++connectedPlayerCount);
		Debug.Log("inc " + connectedPlayerCount);
	}

	public void DecrementPlayerCount() {
		GetComponent<NetworkView>().RPC("SetPlayerCountRPC", RPCMode.Others, --connectedPlayerCount);
		Debug.Log("dec " + connectedPlayerCount);
	}

	// Client Functions
	[RPC]
	void SetPlayerCountRPC(int playerCount) {
		connectedPlayerCount = playerCount;
	}

	// Server Message Handlers
	void OnServerInitialized() {
		isServer = true;
	}
	void OnPlayerConnected(NetworkPlayer player) {
		IncrementPlayerCount();
	}
	void OnPlayerDisconnected(NetworkPlayer player) {
		DecrementPlayerCount();
	}

	// Client Message Handlers
	void OnConnectedToServer() {
		isClient = true;
		connected = true;
	}
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		connected = false;
	}
}
