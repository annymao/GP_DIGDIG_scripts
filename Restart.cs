using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		//PhotonNetwork.automaticallySyncScene = true;
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void RestartGame(){
		GameObject.Find ("BGM").GetComponent<AudioSource> ().Stop ();
		if (PhotonNetwork.isMasterClient) {
			PhotonNetwork.DestroyAll ();
		}
		PhotonNetwork.automaticallySyncScene = false;
		SceneManager.LoadScene (1);
		//PhotonNetwork.LoadLevel(1);
	}
}
