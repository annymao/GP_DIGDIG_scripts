using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster :Photon. MonoBehaviour {
	public bool spawn = false;
	public GameObject[] monsters;
	float time = 2f;
	float lastspawn;
	int count=0;
	public int random;
	public int MaxMonster = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate (){
		if (spawn) {
			if (PhotonNetwork.isMasterClient) {
				if (random == 0) {
					if (count <= 1 && lastspawn + time <= Time.time) {	
						PhotonNetwork.Instantiate (monsters [random].gameObject.name, transform.position, Quaternion.identity, 0);
						//water.transform.parent = GameObject.Find ("SpawnManager").transform;
						count++;
						lastspawn = Time.time;
					} else if (count > 1) {
						spawn = false;
						PhotonNetwork.Destroy (gameObject);
					}
				} else if (random == 1) {
					if (count <= 2 && lastspawn + time <= Time.time) {	
						PhotonNetwork.Instantiate (monsters [random].gameObject.name, transform.position, Quaternion.identity, 0);
						//water.transform.parent = GameObject.Find ("SpawnManager").transform;
						count++;
						lastspawn = Time.time;
					} else if (count > 2) {
						spawn = false;
						PhotonNetwork.Destroy (gameObject);
					}
				} else if (random == 2) {	
					PhotonNetwork.Instantiate (monsters [random].gameObject.name, transform.position, Quaternion.identity, 0);
					spawn = false;
					PhotonNetwork.Destroy (gameObject);
				}
			}
			//transform.GetComponent<PhotonView> ().RPC ("Spawn", PhotonTargets.All, null);
		}
	}
	[PunRPC]
	public void SetRandom(int x){
		random = x;
	}
	[PunRPC]
	void Spawn(){
			if (random == 0) {
				if (count <= 1 && lastspawn + time <= Time.time) {	
					PhotonNetwork.Instantiate (monsters [random].gameObject.name, transform.position, Quaternion.identity, 0);
					//water.transform.parent = GameObject.Find ("SpawnManager").transform;
					count++;
					lastspawn = Time.time;
				} else if (count > 1) {
					PhotonNetwork.Destroy (gameObject);
				}
			} 
			else if (random == 1) {
				if (count <= 2 && lastspawn + time <= Time.time) {	
					PhotonNetwork.Instantiate(monsters [random].gameObject.name, transform.position, Quaternion.identity, 0);
					//water.transform.parent = GameObject.Find ("SpawnManager").transform;
					count++;
					lastspawn = Time.time;
				} else if (count > 2) {
					PhotonNetwork.Destroy (gameObject);
				}
			}
			else if (random == 2) {	
					PhotonNetwork.Instantiate (monsters [random].gameObject.name, transform.position, Quaternion.identity, 0);
					PhotonNetwork.Destroy (gameObject);
			}

	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player")
			spawn = true;
	}
}
