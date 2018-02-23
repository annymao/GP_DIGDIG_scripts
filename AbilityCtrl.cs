using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCtrl : Photon.MonoBehaviour {
	public int power;
	public float destroyTime;
	public float getTime = 0.0f;
	// Use this for initialization
	void Start () {
		/*Invoke ("DestroySkill", destroyTime);*/
		getTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - getTime > destroyTime) {
			DestroySkill ();
		}
	}
	void OnTriggerEnter(Collider other){
		/*if (other.gameObject.tag == "Enemy") {
			other.gameObject.GetComponent<EnemyInfo> ().health -= power;
		}*/
		/*if (other.gameObject.tag == "Player") {
			PlayerCtrl player = other.gameObject.GetComponent<PlayerCtrl> ();
			if (gameObject.name == "water(Clone)") {
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Water",1);
				PhotonNetwork.player.SetCustomProperties (p);
			}
			if (gameObject.name == "fire(Clone)") {
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Lava",1);
				PhotonNetwork.player.SetCustomProperties (p);
			}
		}*/
	}

	void DestroySkill(){
		//GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
			if (gameObject.name == "water(Clone)") {
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Water",0);
				pl.SetCustomProperties (p);
			} else if (gameObject.name == "fire(Clone)") {
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Lava",0);
				pl.SetCustomProperties (p);
			}
			else if (gameObject.name == "stone(Clone)") {
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Stone",0);
				pl.SetCustomProperties (p);
			}
				
		}
		Destroy (gameObject);
	}

}
