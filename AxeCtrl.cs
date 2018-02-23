using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCtrl : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0)) {
			anim.SetBool ("Dig", true);
			/*if (!anim.IsPlaying ("AxeAni"))
				
				Debug.Log (anim.Play ());*/
			//gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		}
		else if(Input.GetMouseButtonUp (0)) {
			anim.SetBool ("Dig", false);
			/*if (!anim.IsPlaying ("AxeAni"))
				
				Debug.Log (anim.Play ());*/
			//gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
}
