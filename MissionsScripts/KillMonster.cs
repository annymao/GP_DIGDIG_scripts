using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KillMonster :Photon.MonoBehaviour {
	public int initialKill = 0;
	public int currentKill = 0;

	// Use this for initialization
	void Start () {
		Debug.Log ("Monster Kill");
		initialKill = PhotonNetwork.player.CustomProperties ["Kill"].GetHashCode();
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void SetCurrentKill(){
		gameObject.GetComponent<Text> ().text = "Kill Monsters " + currentKill.ToString () + "/100";
		if (currentKill >= 100) {
			transform.parent.gameObject.GetComponent<MissionCtrl> ().complete = true;
			gameObject.GetComponent<KillMonster> ().enabled = false;
		}
	}

}