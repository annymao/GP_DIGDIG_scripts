using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCtrl : Photon.MonoBehaviour {
	private int playerId;
	public myCanvas cScript;
	public bool spawn;
	public GameObject water;
	public GameObject fire;
	public GameObject cure;
	public GameObject stone;

	// Use this for initialization
	void Start () {
		cScript = GameObject.Find ("Canvas(Clone)").GetComponent<myCanvas> ();
		spawn = false;
	}
	public void myPlayerID(int id){
		playerId=id;
	}
	// Update is called once per frame
	void Update () {
		if(/*Input.GetKeyDown(KeyCode.Q)*/spawn){
			
				//Partical.transform.parent = gameObject.transform;
			gameObject.GetComponent<PhotonView>().RPC("SetParent",PhotonTargets.All,PhotonNetwork.player.CustomProperties["PlayerID"].GetHashCode());
			spawn = false;
			
		}
	}
	[PunRPC]
	void SetParent(int id){
		GameObject Partical;
		switch(id){
		case 0:
			Partical = Instantiate(water,transform.position+new Vector3(0f,0f,-0.1f),Quaternion.identity) as GameObject;
			break;
		case 1:
			Partical = Instantiate(fire,transform.position +new Vector3(0f,0f,-0.1f),Quaternion.identity) as GameObject;
			break;
		case 2:
			Partical = Instantiate(cure,transform.position +new Vector3(0f,0f,-0.1f),Quaternion.identity) as GameObject;
			break;
		case 3:
			Partical = Instantiate(stone,transform.position +new Vector3(0f,0f,-0.1f),Quaternion.identity) as GameObject;
			break;
		case 4:
			Partical = Instantiate(water,transform.position+new Vector3(0f,0f,-0.1f),Quaternion.identity) as GameObject;
			break;
		case 5:
			Partical = Instantiate(fire,transform.position +new Vector3(0f,0f,-0.1f),Quaternion.identity) as GameObject;
			break;
		case 6:
			Partical = Instantiate(cure,transform.position +new Vector3(0f,0f,-0.1f),Quaternion.identity) as GameObject;
			break;
		case 7:
			Partical = Instantiate(stone,transform.position +new Vector3(0f,0f,-0.1f),Quaternion.identity) as GameObject;
			break;
		default:
			Partical = Instantiate(fire,transform.position +new Vector3(0f,0f,-0.1f),Quaternion.identity) as GameObject;
			break;

		}
		/*GameObject.Find ("water(Clone)").transform.parent = gameObject.transform;
		GameObject.Find ("water(Clone)").transform.position = gameObject.transform.position;*/
		Partical.transform.parent = gameObject.transform;
	}
}
