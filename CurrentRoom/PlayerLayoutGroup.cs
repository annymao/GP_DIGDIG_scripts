using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLayoutGroup : Photon.PunBehaviour {
	[SerializeField] private GameObject _playerListingPrefab;
	private GameObject PlayerListingPrefab { get { return _playerListingPrefab; } }
	public InputField nameInputField;
	private List<PlayerListing> _playerListings = new List<PlayerListing>();
	private List<PlayerListing> PlayerListings { get { return _playerListings; } }
	public Sprite[] playerSprite;
	public Button chooseButton;
	//public string PlayerName { get; private set; }
	// Photon network called when master client is switched
	void Start(){
		if (PhotonNetwork.inRoom) {
			PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;

			foreach (PhotonPlayer photonPlayer in photonPlayers) {
				PlayerJoinedRoom(photonPlayer) ;
			}
			chooseButton.GetComponent<Image> ().sprite = playerSprite [PhotonNetwork.player.CustomProperties["PlayerID"].GetHashCode()];
		}
	}
	/*public override void OnMasterClientSwitched(PhotonPlayer newMasterClient) {
		PhotonNetwork.LeaveRoom();
	}*/
	// Photon network called whenever you join a room
	public override void OnJoinedRoom() {
		foreach(Transform child in this.transform) {
			Destroy(child.gameObject);
		} 
		print("Joined ROOM");
		ChatRoomCanvas roomcanvas = GameObject.Find ("ChatPanel").GetComponent<ChatRoomCanvas> ();

		FindMyID ();
		PhotonNetwork.player.NickName = nameInputField.text;
		PlayerNetwork_Text.Instance.PlayerName =  nameInputField.text;
		roomcanvas.getConnected ();
		ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
		p.Add ("DeadTimes", 0);
		p.Add ("Kill", 0);
		p.Add ("DragonKill", 0);
		p.Add ("TurtleKill", 0);
		p.Add ("MudKill", 0);
		p.Add ("SkillUse", 0);
		p.Add ("Boss", 0);
		p.Add ("Points", 0);
		//p.Add ("Name",nameInputField.text);
		p.Add ("Position",new Vector3(0f,0f,0f));
		p.Add ("Dig",0);
		p.Add ("Jewel",0);
		p.Add ("Water",0);
		p.Add ("Lava",0);
		p.Add ("Stone",0);
		p.Add ("Cure",0);
		p.Add ("Title1", 0);
		p.Add ("Title2", 0);
		p.Add ("Title3", 0);
		p.Add ("Title4", 0);
		PhotonNetwork.player.SetCustomProperties (p);
		MainCanvasManager.Instance.CurrentRoomCanvas.transform.SetAsLastSibling();

		 PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;

		 foreach (PhotonPlayer photonPlayer in photonPlayers) {
			 PlayerJoinedRoom(photonPlayer) ;
		 }

	}

	// Photon network called when a player join a room
	public override void OnPhotonPlayerConnected(PhotonPlayer photonPlayer) {
		PlayerJoinedRoom(photonPlayer);
	}

	// Photon network called when a player is leaving a room
	public override void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer) {
		PlayerLeftRoom(photonPlayer);
		Debug.Log ("LEAVE");
		//PhotonNetwork.DestroyPlayerObjects (photonPlayer.ID);

	}


	private void PlayerJoinedRoom(PhotonPlayer photonPlayer) {
		if (photonPlayer == null) return;
		
		PlayerLeftRoom(photonPlayer);

		GameObject playerListingObj = Instantiate(PlayerListingPrefab);
		playerListingObj.transform.SetParent(this.transform, false);

		PlayerListing playerList = playerListingObj.GetComponent<PlayerListing>();
		playerList.ApplyPhotonPlayer(photonPlayer);

		PlayerListings.Add(playerList);
	} 

	private void PlayerLeftRoom(PhotonPlayer photonPlayer) {
		int index = PlayerListings.FindIndex(x => x.PhotonPlayer == photonPlayer);
		Debug.Log ("PlayerLeftRoom");
		if (index != -1){
			Destroy(PlayerListings[index].gameObject);
			PlayerListings.RemoveAt(index);
		}
	}

	public void OnClickRoomState() {
		if (!PhotonNetwork.isMasterClient) return;

		PhotonNetwork.room.IsOpen = !PhotonNetwork.room.IsOpen;
		PhotonNetwork.room.IsVisible = PhotonNetwork.room.IsOpen;
	}
	public void OnClickLeaveRoom() {
		int[] playerIndex = new int[10];
		playerIndex =(int[]) PhotonNetwork.room.CustomProperties ["PlayerIndex"];
		playerIndex [(int)PhotonNetwork.player.CustomProperties ["PlayerID"]] = 0;
		ExitGames.Client.Photon.Hashtable index = new ExitGames.Client.Photon.Hashtable ();
		index.Add ("PlayerIndex", playerIndex);
		PhotonNetwork.room.SetCustomProperties (index);
		PhotonNetwork.DestroyPlayerObjects (PhotonNetwork.player);
		PhotonNetwork.LeaveRoom();
		OnLeftRoom ();
		ChatRoomCanvas ChatRoomCanvas = GameObject.Find("ChatPanel").GetComponent<ChatRoomCanvas>();
		if (ChatRoomCanvas != null)
		{
			ChatRoomCanvas.getDisconnected();
		}

	}
	void FindMyID(){
		int[] playerIndex = new int[10];
		playerIndex =(int[]) PhotonNetwork.room.CustomProperties ["PlayerIndex"];
		for (int i = 0; i < 8; i++) {
			if (playerIndex [i] == 0) {
				playerIndex [i] = 1;
				ExitGames.Client.Photon.Hashtable id = new ExitGames.Client.Photon.Hashtable ();
				id.Add ("PlayerID", i);
				PhotonNetwork.player.SetCustomProperties (id);
				chooseButton.GetComponent<Image> ().sprite = playerSprite [i];
				break;
			}
		}
		ExitGames.Client.Photon.Hashtable index = new ExitGames.Client.Photon.Hashtable ();
		index.Add ("PlayerIndex", playerIndex);
		PhotonNetwork.room.SetCustomProperties (index);
	}
	public void OnClickChoosePlayer(){
		int[] playerIndex = new int[10];
		playerIndex =(int[]) PhotonNetwork.room.CustomProperties ["PlayerIndex"];
		int current_id = PhotonNetwork.player.CustomProperties ["PlayerID"].GetHashCode ();

		int i = (current_id + 1)%8;
		while (i != current_id) {
			if (playerIndex [i] == 0) {
				playerIndex [current_id] = 0;
				playerIndex [i] = 1;
				ExitGames.Client.Photon.Hashtable id = new ExitGames.Client.Photon.Hashtable ();
				id.Add ("PlayerID", i);
				PhotonNetwork.player.SetCustomProperties (id);
				chooseButton.GetComponent<Image> ().sprite = playerSprite [i];
				break;
			} 
			i = (i + 1) % 8;
		}
		ExitGames.Client.Photon.Hashtable index = new ExitGames.Client.Photon.Hashtable ();
		index.Add ("PlayerIndex", playerIndex);
		PhotonNetwork.room.SetCustomProperties (index);
	}
}
