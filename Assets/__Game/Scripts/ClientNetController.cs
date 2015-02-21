using UnityEngine;
using System.Collections;

public class ClientNetController : MonoBehaviour {

	public int disconnectTimeout = 200; // Default 200

	public void Connect() {
		ConnectToServer();
	}

	public void Disconnect() {
		Network.Disconnect(disconnectTimeout);
	}

	void ConnectToServer() {
		Debug.Log("Connecting to server...");
		Network.Connect("127.0.0.1", NetConfig.serverPort);
	}

	#region callbacks
	void OnConnectedToServer() {
		Debug.Log("Connected to server");
	}

	void OnDisconnectedFromServer(NetworkDisconnection info) {
		if (Network.isServer)
			Debug.Log("Local server connection disconnected");
		else
			if (info == NetworkDisconnection.LostConnection)
				Debug.Log("Lost connection to the server");
			else
				Debug.Log("Successfully diconnected from the server");
	}

	void OnFailedToConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: " + error);
		Debug.Log("Trying again...");
		ConnectToServer();
	}
	#endregion
}
