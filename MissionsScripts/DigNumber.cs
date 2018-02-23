using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DigNumber : MonoBehaviour {
	public int dig;
	public int current_dig;
	// Use this for initialization
	void Start () {
		dig = Random.Range (1000, 3000);
		SetCurrentDig ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SetCurrentDig(){
		gameObject.GetComponent<Text> ().text = "Dig " + current_dig.ToString () + "/"+dig;
		if (current_dig >= dig) {
			transform.parent.gameObject.GetComponent<MissionCtrl> ().complete = true;
			gameObject.GetComponent<DigNumber> ().enabled = false;
		}
	}
}
