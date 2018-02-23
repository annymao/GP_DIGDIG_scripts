using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour {
	public int tag;
	public Vector3 to_pos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		
	}
	[PunRPC]
	public void SetPos(Vector3 pos,int t){
		to_pos = pos;
		tag = t;
	}

}
