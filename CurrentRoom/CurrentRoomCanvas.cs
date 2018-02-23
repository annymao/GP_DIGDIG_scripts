using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour {
	void Start(){
		PhotonNetwork.automaticallySyncScene = true;
	}
	public void OnClickStartSync() {
		PhotonNetwork.LoadLevel(2);
	}

	public void OnClickStartDelayed() {
		if (!PhotonNetwork.isMasterClient)
			return;
		PhotonNetwork.room.IsOpen = false;
		PhotonNetwork.room.IsVisible = false;
		PhotonNetwork.LoadLevel(2);
		GameObject.Find ("ChatPanel").GetComponent<ChatRoomCanvas> ().OnStartGame();
	}
}

