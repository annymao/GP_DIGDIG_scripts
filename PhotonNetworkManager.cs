using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkManager : MonoBehaviour {

	[SerializeField] private Text connectText;
	[SerializeField] private GameObject player;
	[SerializeField] private Transform spawnPoint; 
	[SerializeField] private GameObject lobbyCamera; 

	// Use this for initialization
	void Start () {
		// Connect to the photon server
		PhotonNetwork.ConnectUsingSettings("lluma");
	}
	/* 
	public virtual void OnConnectedToMaster() {
		Debug.Log("We are now connected to Master ");
	}
	*/
	public virtual void OnJoinedLobby() {
		Debug.Log("We have now joined the lobby");
		// Join a room if it exists,  or create one
		PhotonNetwork.JoinOrCreateRoom("New ", null, null); 
	} 

	public virtual void OnJoinedRoom() {
		Debug.Log("We have now joined a Room");
		// Spawn in the player
		PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation, 0);
		// Deactive the lobby camera
		lobbyCamera.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		// NOTE: FOR   TESTING ONLY
		connectText.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
}
