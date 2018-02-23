using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtrl :Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("DestroyBall", 8f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision other){
		gameObject.GetComponent<PhotonView> ().RPC ("Disable_ball", PhotonTargets.All, null);
		//gameObject.GetComponent<SphereCollider> ().isTrigger = true;
	}
	[PunRPC]
	void Disable_ball(){
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	}
	void DestroyBall(){
		if(gameObject.GetComponent<PhotonView>().isMine)
			PhotonNetwork.Destroy(gameObject);
	}
}
