using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Photon.MonoBehaviour {
	public int count;
	public bool spawned=false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (count == PhotonNetwork.room.PlayerCount&&!spawned) {
			if (PhotonNetwork.isMasterClient) {
				PhotonNetwork.Instantiate ("Angle", transform.position - new Vector3 (0f, -1f, 0f), Quaternion.identity, 0);
				spawned = true;
			}
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			gameObject.GetComponent<PhotonView> ().RPC ("PlayerIn", PhotonTargets.All, count + 1);
		}
	}
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			gameObject.GetComponent<PhotonView> ().RPC ("PlayerIn", PhotonTargets.All, count - 1);
		}
	}
	[PunRPC]
	void PlayerIn(int co){
		count = co;
	}

}
