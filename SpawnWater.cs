using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWater : Photon.MonoBehaviour {
	public GameObject water;
	public GameObject water2;
	public GameObject spawnManger;
	float time = 0.15f;
	float lastspawn;
	int count=0;
	bool spawn = false;
	int random;
	DynamicParticle.STATES currentState;
	// Use this for initialization
	void Start () {
		random = Random.Range (0, 2);
		if (random == 0) {
			currentState = DynamicParticle.STATES.WATER;
		} else {
			currentState = DynamicParticle.STATES.LAVA;
		}
			
	}
	// Update is called once per frame\
	void FixedUpdate (){
		/*if (spawn) {
			transform.GetComponent<PhotonView> ().RPC ("Spawn", PhotonTargets.All, null);
		}*/
	}
	[PunRPC]
	void Spawn(){
		/*if(PhotonNetwork.isMasterClient){
			if (count <= 150 && lastspawn + time <= Time.time) {	
				water = (GameObject)PhotonNetwork.Instantiate ("DynamicParticle", transform.position, Quaternion.identity, 0);
				water.GetComponent<DynamicParticle> ().SetState (currentState);
				water.GetComponent<Rigidbody> ().AddForce (Vector3.left*2f);
				Debug.Log (currentState);
				water2 = (GameObject)PhotonNetwork.Instantiate ("DynamicParticle", transform.position + new Vector3 (1f, 0f, 0f), Quaternion.identity, 0);
				water2.GetComponent<DynamicParticle> ().SetState (currentState);
				water2.GetComponent<Rigidbody> ().AddForce (Vector3.right*5f);
				Debug.Log (currentState);
				//water.transform.parent = GameObject.Find ("SpawnManager").transform;
				count++;
				lastspawn = Time.time;
			} else if(count > 150){
				PhotonNetwork.Destroy (gameObject);
			}
		}*/

	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player")
			spawn = true;
	}
	void Update () {
		
	}
}
