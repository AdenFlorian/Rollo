﻿using UnityEngine;
using System.Collections;

public class ServerNetController : MonoBehaviour {

	int playerCount = 0;
	int disconnectTimeout = 200; // Default 200

	public void Init() {
		Network.InitializeSecurity();
		bool useNat = !Network.HavePublicAddress();
		Network.InitializeServer(256, NetConfig.serverPort, useNat);
	}

	public void Disconnect() {
		Network.Disconnect(disconnectTimeout);
	}

	#region callbacks
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		if (Network.isServer)
			Debug.Log("Local server connection disconnected");
		else
			if (info == NetworkDisconnection.LostConnection)
				Debug.Log("Lost connection to the server");
			else
				Debug.Log("Successfully diconnected from the server");
	}

	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player " + player + " connected from " + player.ipAddress + ":" + player.port);
		playerCount++;
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
		playerCount--;
	}

	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
	#endregion
}
