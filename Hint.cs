using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hint : Photon.MonoBehaviour {
	GameObject[] monsterNest;
	int r;
	public GameObject text;
	public Vector2 pos;
	// Use this for initialization
	void Start () {
		monsterNest = GameObject.FindGameObjectsWithTag ("MonsterNest");
		r = Random.Range (0, monsterNest.GetLength (0));
		float x = monsterNest [r].transform.position.x;
		float y = monsterNest [r].transform.position.y;
		transform.GetComponent<PhotonView> ().RPC("setPos",PhotonTargets.All,x,y);
		//text.GetComponent<Text> ().text = "(x,y) = " + "(" + ((int)(pos.x)).ToString()+","+((int)(pos.y)).ToString()+")";
		//p = GameObject.Find ("Player");
	}
	[PunRPC]
	public void setPos(float x,float y){
		
		pos = new Vector2 (x,y);
	}
	// Update is called once per frame
	void Update () {

	}

}
