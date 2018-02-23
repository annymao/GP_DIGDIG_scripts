using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HintCanvas : Photon.MonoBehaviour {
	GameObject[] monsterNest;
	int r;
	public GameObject text;
	public Button spawn1;
	public Button spawn2;
	public Button spawn3;
	public Button spawn4;
	public Vector3 spwanPoint1;
	public Vector3 spwanPoint2;
	public Vector3 spwanPoint3;
	public Vector3 spwanPoint4;
	Vector3 playerSpwan;
	// Use this for initialization
	void Start () {
		spawn1.interactable = false;
		spawn2.interactable = false;
		spawn3.interactable = false;
		spawn4.interactable = false;

	}

	// Update is called once per frame
	void Update () {

	}
	public void setText(int x, int y){
		text.GetComponent<Text> ().text = "(x,y) = " + "(" + x.ToString () + "," + y.ToString () + ")";
	}
	public void OnExitButton(){
		transform.GetChild(0).gameObject.SetActive (false);
	}
	public void OnClickFirst(){
		GameObject.Find ("Player").GetComponent<PlayerCtrl> ().isTrans = true;
		playerSpwan = spwanPoint1;
		Invoke ("Trans", 3f);

	}
	public void OnClickSecond(){
		GameObject.Find ("Player").GetComponent<PlayerCtrl> ().isTrans = true;
		playerSpwan = spwanPoint2;
		Invoke ("Trans", 3f);

	}
	public void OnClickThird(){
		GameObject.Find ("Player").GetComponent<PlayerCtrl> ().isTrans = true;
		playerSpwan = spwanPoint3;
		Invoke ("Trans", 3f);

	}
	public void OnClickFourth(){
		GameObject.Find ("Player").GetComponent<PlayerCtrl> ().isTrans = true;
		playerSpwan = spwanPoint4;
		Invoke ("Trans", 3f);

	}
	public void Trans(){
		GameObject player = GameObject.Find ("Player");
		player.transform.position = playerSpwan;
		player.GetComponent<PlayerCtrl>().isTrans = false;
		player.GetComponent<PlayerCtrl> ().start_x = playerSpwan.x;
		player.GetComponent<PlayerCtrl> ().start_y = playerSpwan.y;

		GameObject.Find ("BGM").GetComponent<BGM> ().playBGM (player.transform.position);
	}
}
