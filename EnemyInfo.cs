using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : Photon.MonoBehaviour {
	public int health;
	myCanvas cScript;
	// Use this for initialization
	void Start () {
		cScript = GameObject.Find ("Canvas(Clone)").GetComponent<myCanvas> ();
	}

	// Update is called once per frame
	void Update () {


	}
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Ball") {
			StartCoroutine("hitAndChangeColor");
				health -= 5;
			if (health <= 0) {
				int currentKill = PhotonNetwork.player.CustomProperties ["Kill"].GetHashCode();
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Kill", currentKill+1);
				GameObject killmonster = GameObject.Find ("Mission-KillMonster(Clone)");
				if (killmonster != null) {
					killmonster.GetComponent<KillMonster> ().currentKill++;
					if (killmonster.GetComponent<KillMonster> ().enabled) {
						killmonster.GetComponent<KillMonster> ().SetCurrentKill ();
					}
				}
				if(gameObject.name == "Mud(Clone)"){
					int current_mud = PhotonNetwork.player.CustomProperties ["MudKill"].GetHashCode();
					p.Add ("MudKill", current_mud + 1);
					GameObject killMud = GameObject.Find("Mission-KillMud(Clone)");
					if (killMud != null ) {
						killMud.GetComponent<KillMud> ().currentKill++;
						if (killMud.GetComponent<KillMud> ().enabled) {
							killMud.GetComponent<KillMud> ().SetCurrentKill ();
						}
					}
				}
				if (gameObject.name == "Dragon(Clone)") {
					GameObject findDragon = GameObject.Find("Mission-FindDragon(Clone)");
					int current_dragon = PhotonNetwork.player.CustomProperties ["DragonKill"].GetHashCode();
					p.Add ("DragonKill", current_dragon + 1);
					if (findDragon != null) {
						findDragon.GetComponent<FindDragon> ().currentKill++;
						if (findDragon.GetComponent<FindDragon> ().enabled) {
							findDragon.GetComponent<FindDragon> ().SetCurrentKill ();
						}
					}

				}
				if (gameObject.name == "Turtle(Clone)") {
					int current_turtle = PhotonNetwork.player.CustomProperties ["TurtleKill"].GetHashCode();
					p.Add ("TurtleKill", current_turtle + 1);
				}
				if (gameObject.name == "Angle(Clone)") {
					p.Add ("Boss", 1);
					gameObject.GetComponent<PhotonView> ().RPC ("Load", PhotonTargets.All, null);
				}
				PhotonNetwork.player.SetCustomProperties (p);
				cScript.SetKillText ();
				gameObject.GetComponent<PhotonView> ().RPC ("DestroyEnemy", PhotonTargets.All, null);

			}
			//other.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			other.gameObject.GetComponent<SphereCollider> ().isTrigger = true;
		}

	}
	[PunRPC]
	void Load(){
		PhotonNetwork.LoadLevel (3);
	}
	[PunRPC]
	void DestroyEnemy(){
		if(gameObject.GetComponent<PhotonView>().isMine)
			PhotonNetwork.Destroy(gameObject);
	}
	IEnumerator hitAndChangeColor(){
		for(int i=0;i<5;i++){
			GetComponent<SpriteRenderer>().color = new Color32(255, 20, 20, 255);
			print("attack red");
			yield return new WaitForSeconds(0.1f);
			GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
			print("attack black");
			yield return new WaitForSeconds(0.1f);
		}
	}
	/*void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Ball" && gameObject.name == "Dragon(Clone)") {
			PhotonNetwork.Destroy (other.gameObject);
			health -= 5;
			if (health <= 0) {
				int currentKill = PhotonNetwork.player.CustomProperties ["Kill"].GetHashCode();
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Kill", currentKill+1);
				PhotonNetwork.player.SetCustomProperties (p);
				cScript.SetKillText ();
				GameObject killmonster = GameObject.Find ("Mission-KillMonster(Clone)");
				if (killmonster != null) {
					killmonster.GetComponent<KillMonster> ().currentKill++;
					if (killmonster.GetComponent<KillMonster> ().enabled) {
						killmonster.GetComponent<KillMonster> ().SetCurrentKill ();
					}
				}
				GameObject findDragon = GameObject.Find("Mission-FindDragon(Clone)");
				if (findDragon != null && gameObject.name == "Dragon(Clone)") {
					findDragon.GetComponent<FindDragon> ().currentKill++;
					if (findDragon.GetComponent<FindDragon> ().enabled) {
						findDragon.GetComponent<FindDragon> ().SetCurrentKill ();
					}
				}
				PhotonNetwork.Destroy (gameObject);

			}
		}
	}*/
}
