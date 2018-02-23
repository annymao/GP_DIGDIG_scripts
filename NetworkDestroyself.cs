using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDestroyself : Photon.MonoBehaviour {
	public float time = 3f;
	// Use this for initialization
	void Start () {
		Invoke ("Destroymyself", time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Destroymyself(){
		if(photonView.isMine)
			PhotonNetwork.Destroy (gameObject);
	}
}
