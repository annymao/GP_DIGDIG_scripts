using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigCtrl : MonoBehaviour {
	public  Vector3 initial_pos;
	Animator anim;
	// Use this for initialization
	void Start () {
		initial_pos = transform.parent.position - transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(pz.x,pz.y,transform.position.z);

		/*if (Input.GetKey (KeyCode.S)) {
			initial_pos = transform.parent.position - transform.position;
			transform.position = transform.parent.position + new Vector3 (0.0f, -0.75f, 0.0f);
		} /*else if (Input.GetKeyUp (KeyCode.S)) {
			transform.position = transform.parent.position - initial_pos;
		}*/
		/*if (Input.GetKey(KeyCode.W)) {
			initial_pos = transform.parent.position - transform.position;
			transform.position = transform.parent.position + new Vector3 (0.0f, 0.75f, 0.0f);
		} 
		if (Input.GetKey (KeyCode.A)) {
			initial_pos = transform.parent.position - transform.position;
			transform.position = transform.parent.position + new Vector3 (-0.75f, 0.0f, 0.0f);
		} 
		if (Input.GetKey (KeyCode.D)) {
			initial_pos = transform.parent.position - transform.position;
			transform.position = transform.parent.position + new Vector3 (0.75f, 0.0f, 0.0f);
		} */


		/*if (Input.GetKeyUp (KeyCode.W)) {
			transform.position = transform.parent.position - initial_pos;
		}*/
		
	}
}
