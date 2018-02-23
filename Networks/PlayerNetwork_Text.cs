using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerNetwork_Text : MonoBehaviour {
	
	public static PlayerNetwork_Text Instance;
	public string PlayerName { get;  set; }
	private PhotonView PhotonView;
	private int PlayersInGame = 0;
	public InputField nameInputField;
	private void Awake() {
		PhotonView = this.GetComponent<PhotonView>();
		if (Instance == null) {
			Instance = this;
			//PhotonView.DontDestroyOnLoad (this);
		} else if(this!=Instance) {
			//SceneManager.sceneLoaded -= OnSceneFinishingLoading;
			if(PhotonView.isMine)
				PhotonNetwork.RemoveRPCs (PhotonView);
			PhotonView.DestroyImmediate (gameObject);
			return;
		}
		Instance = this;

	
		// Ex: lluma#1234
		PlayerName =  nameInputField.text;
		SceneManager.sceneLoaded += OnSceneFinishingLoading;

	}
	void Start(){
		//PlayerName =  nameInputField.text;
	}
	private void OnSceneFinishingLoading(Scene scene, LoadSceneMode mode) {
		Debug.Log("OnSceneFinishingLoading");
		if (scene.name == "game") {
			if (PhotonNetwork.isMasterClient) {
				print ("This connection is master client");
				MasterLoadedGame ();
			} else {
				print ("Load");
				NonMasterLoadedGame ();
			}
		} 
	}

	private void MasterLoadedGame() {
		PlayersInGame = 1;
		PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
	}

	private void NonMasterLoadedGame() {
		PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
	}

	[PunRPC]
	private void RPC_LoadGameOthers() {
		GameObject.Find ("ChatPanel").GetComponent<ChatRoomCanvas> ().OnStartGame();
		PhotonNetwork.LoadLevel(2);
	}

	[PunRPC]
	private void RPC_LoadedGameScene() {
		PlayersInGame++;
		print ("# Player in game: " + PlayersInGame);
		if (PlayersInGame == PhotonNetwork.playerList.Length) {
			print("All players are in the game scene");
		}
	}
 }
