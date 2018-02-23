using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyNetwork : Photon.PunBehaviour {

	[SerializeField] private Text connectText;

	// Use this for initialization
	private void Start () {
		print("Connecting to server...");
		if (!PhotonNetwork.connected)
			PhotonNetwork.ConnectUsingSettings ("0.0.0");
		else {
			if (PhotonNetwork.inRoom) {
				GameObject.Find ("CurrentRoom").transform.SetAsLastSibling ();
				PhotonNetwork.room.IsVisible = true;
				PhotonNetwork.room.IsOpen = true;
			}
		}


	}

	private void OnConnectedToMaster() {
		print("Connected to master");
		PhotonNetwork.automaticallySyncScene = false; // Set whether player load synchronous scene 
													  // when joining room or not 
		//PhotonNetwork.playerName = PlayerNetwork_Text.Instance.PlayerName;
		PhotonNetwork.JoinLobby(TypedLobby.Default);
	}

	private void OnJoinedLobby() {
		 print("Joined Lobby");
		GameObject.Find ("BGM").GetComponent<BGM> ().lobbyBGM();
		if (!PhotonNetwork.inRoom) 
			MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
	}

	void OnApplicationQuit(){

		PhotonNetwork.Disconnect ();
	}

	void Update() {
		// NOTE: FOR TESTING ONLY
		connectText.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
	
}
