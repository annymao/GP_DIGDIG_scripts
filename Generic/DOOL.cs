using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOOL : MonoBehaviour {
	static DOOL instance;
	// Use this for initialization
	private void Awake () {
		if (instance == null) {
			instance = this;
			//PhotonView.DontDestroyOnLoad (this);
		} else if(this!=instance) {
			PhotonView.DestroyImmediate (gameObject);
		}
	}
	
}
