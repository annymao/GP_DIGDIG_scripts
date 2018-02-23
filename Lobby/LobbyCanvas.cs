using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCanvas : MonoBehaviour {

	[SerializeField] private RoomLayoutGroup _roomLayoutGroup;
	public InputField nameInputField;
	public InputField roomInputField;
	private RoomLayoutGroup RoomLayoutGroup { get { return _roomLayoutGroup; } }
	public Text chatArea;
	void Awake(){
		nameInputField.onEndEdit.RemoveAllListeners ();
		nameInputField.text = "Player " + Random.Range (0, 999); 
		roomInputField.onEndEdit.RemoveAllListeners ();
		roomInputField.text = "Room " + Random.Range (0, 999); 
	}
	void Start(){
		
	}
	public void OnClickJoinRoom(string roomName) {
		PhotonNetwork.player.NickName = nameInputField.text;
		if (PhotonNetwork.JoinRoom(roomName)) {
			// Chat settings
			/*chatArea.text = "";
			ChatRoomCanvas.currentRoomChat = roomName;
			ChatRoomCanvas.joined = true;*/
		}
		else {
			print("Join room failed!");
		}
	}
}
