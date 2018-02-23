using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon.Chat;
using UnityEngine.UI;

public class ChatRoomCanvas : Photon.MonoBehaviour, IChatClientListener {


	ChatClient ChatClient;
	public Text connectionState;
	string worldChat;
	public static string currentRoomChat;
	public Text msgInput;
	public Text msgArea;
	public static bool joined;

	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
		if (string.IsNullOrEmpty (PhotonNetwork.PhotonServerSettings.ChatAppID)) {
			Debug.LogError ("No Chat ID is provided!!") ;
			return;
		}

		connectionState.text = "Connecting...";
		worldChat = "world";
		joined = false;
		if (PhotonNetwork.inRoom) {
			getConnected ();
		}
	}
	void OnApplicationQuit(){
		if (ChatClient != null)
			ChatClient.Disconnect ();
	}

	// Update is called once per frame
	void Update () {
		if (ChatClient != null)
			ChatClient.Service ();
		/*if (joined) {
			getConnected ();
			joined = false;
		}*/

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
			if (msgInput.text != "") {
				sendMsg();
			}
		}
	}

	public void getConnected () {
		print("Chat app try to connect");
		ChatClient = new ChatClient (this);
		ChatClient.Connect (PhotonNetwork.PhotonServerSettings.ChatAppID, "1,0", new ExitGames.Client.Photon.Chat.AuthenticationValues(PhotonNetwork.player.NickName));
		msgArea.text = "";
		connectionState.text = "Connecting to chat app";
	}

	public void getDisconnected() {
		print("Now is disconnect to current Chat app");
		ChatClient.Disconnect();
	}

	/// <summary>
	/// Below method set to the photon chat methods's interface 
	/// </summary>

	public void sendMsg () {
		if (msgInput.text != "")
			ChatClient.PublishMessage (currentRoomChat, msgInput.text);
		msgInput.text.Remove(0);
	}
	public void OnStartGame(){
		ChatClient.Disconnect ();
	}
	public void sendLeaveRoomMsg () {
		ChatClient.PublishMessage (currentRoomChat, "Left");
		ChatClient.Unsubscribe (new string[] { currentRoomChat });
	}

	public void OnConnected () {
		print ("****************** chat app is connected");
		connectionState.text = "Connected";
		Debug.Log ("RoomName" + PhotonNetwork.room.Name);
		currentRoomChat = PhotonNetwork.room.Name;
		ChatClient.Subscribe (new string[] { currentRoomChat });
		ChatClient.SetOnlineStatus (ChatUserStatus.Online);
	}

	public void OnDisconnected () {
		print ("****************** Disconnect");
	}

	public void OnGetMessages (string channelName, string[] senders, object[] messages) {
		for (int i = 0; i < senders.Length; i++) {
			msgArea.text += senders[i] + " : " + messages[i] + "\n";
		}
	}

	public void OnPrivateMessage (string sender, object message, string channelName) {

	}

	public void OnSubscribed (string[] channels, bool[] results) {
		connectionState.text = "In " + currentRoomChat + " chat";
		ChatClient.PublishMessage (currentRoomChat, "Joined");
	}

	public void OnUnsubscribed (string[] channels) {

	}

	public void OnStatusUpdate (string user, int status, bool gotMessage, object message) {

	}

	public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message) {

	}

	public void OnChatStateChange (ChatState state) {

	}

}
