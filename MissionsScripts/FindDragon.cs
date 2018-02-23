using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FindDragon : MonoBehaviour {
	public int currentKill = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SetCurrentKill(){
		gameObject.GetComponent<Text> ().text = "Find Dragon " + currentKill.ToString () + "/10";
		if (currentKill >= 10) {
			transform.parent.gameObject.GetComponent<MissionCtrl> ().complete = true;
			gameObject.GetComponent<FindDragon> ().enabled = false;
		}
	}
}
