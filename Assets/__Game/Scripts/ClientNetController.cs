using UnityEngine;
using System.Collections;

public class ClientNetController : MonoBehaviour {

	public int disconnectTimeout = 200; // Default 200
	public ConnectTo connectTo = ConnectTo.localhost;

	public void Connect() {
		ConnectToServer();
	}

	public void Disconnect() {
		Network.Disconnect(disconnectTimeout);
	}

	void ConnectToServer() {
		switch (connectTo) {
			case ConnectTo.localhost:
				Debug.Log("Connecting to localhost server...");
				Network.Connect("127.0.0.1", NetConfig.serverPort);
				break;
			case ConnectTo.localvagrant:
				Debug.Log("Connecting to vagrant server...");
				Network.Connect(NetConfig.server_vagrant, NetConfig.serverPort);
				break;
			case ConnectTo.lempub14:
				Debug.Log("Connecting to lempub14 server...");
				Network.Connect(NetConfig.server_lempub14, NetConfig.serverPort);
				break;
			default:
				break;
		}
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

public enum ConnectTo {
	localhost,
	localvagrant,
	lempub14
}