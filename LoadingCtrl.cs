using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCtrl : Photon.MonoBehaviour {
	Animation anim;
	public GameObject treasure;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animation> ();
		//anim.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (gameObject.transform.position.x < treasure.transform.position.x) {
			gameObject.transform.Translate (new Vector3 (5f, 0f, 0f) * Time.deltaTime);
		} else {
			PhotonNetwork.LoadLevel (4);
		}
	}
}
