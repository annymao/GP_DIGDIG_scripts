using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KillMud : MonoBehaviour {
	public int initialKill = 0;
	public int currentKill = 0;

	// Use this for initialization
	void Start () {
		Debug.Log ("Mud Kill");
		initialKill = PhotonNetwork.player.CustomProperties ["Kill"].GetHashCode();
	}

	// Update is called once per frame
	void Update () {

	}
	public void SetCurrentKill(){
		gameObject.GetComponent<Text> ().text = "Kill Mud " + currentKill.ToString () + "/10";
		if (currentKill >= 10) {
			transform.parent.gameObject.GetComponent<MissionCtrl> ().complete = true;
			gameObject.GetComponent<KillMud> ().enabled = false;
		}
	}
}
