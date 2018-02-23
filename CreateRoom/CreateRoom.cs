using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour {
	[SerializeField] private Text _roomName; 
	private Text RoomName { get { return _roomName; } }
	public int[] playerIndex;
	public Text chatArea;
	public InputField roomInputField;
	void Start(){
		playerIndex = new int[10];
	}
	public void OnClick_CreateRoom() {
		RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
		Debug.Log (RoomName.text + "/" + roomInputField.text);
		if (PhotonNetwork.CreateRoom(/*RoomName.text*/roomInputField.text, roomOptions, TypedLobby.Default)) {
			chatArea.text = "";
			 print("Create room successfully sent"); 

		 } 
		 else {
			 print("Create room failed to send");
		 }
	}

	private void OnPhotonCreateRoomFailed(object[] codeAndMessage) {
		print("Create room failed: " + codeAndMessage[1]);
	}

	private void OnCreatedRoom() {
		print("Room created successfully.");
		ExitGames.Client.Photon.Hashtable index = new ExitGames.Client.Photon.Hashtable ();
		index.Add ("PlayerIndex", playerIndex);
		PhotonNetwork.room.SetCustomProperties (index);

		ExitGames.Client.Photon.Hashtable id = new ExitGames.Client.Photon.Hashtable ();
		id.Add ("PlayerID", 0);
		PhotonNetwork.player.SetCustomProperties (id);
	}
}
