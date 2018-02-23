using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArrowCtrl : MonoBehaviour {

	public float degreesPerSec = 360f;
	public GameObject Target;
	public GameObject player;
	bool getCount = false;
	public GameObject[] trans;
	public GameObject[] SetTrans;
	// Update is called once per frame
	void Start(){
		Target = GameObject.FindGameObjectWithTag ("Treasure");
		player = GameObject.Find ("Player");
		//StartCoroutine ("DelayArrow");
		SetTrans = new GameObject[5];
	}
	IEnumerator DelayArrow(){

		yield return new WaitUntil (() => (trans = GameObject.FindGameObjectsWithTag("Transport"))!=null);
		foreach(GameObject g in trans){
			int i = g.GetComponent<Transport> ().tag;
			SetTrans [i] = g; 
		}
		getCount = true;

	}
	void Update()
	{
		Vector3 dir = new Vector3();
		Vector3 mypos;
		trans = GameObject.FindGameObjectsWithTag ("Transport");
		foreach(GameObject g in trans){
			int i = g.GetComponent<Transport> ().tag;
			SetTrans [i] = g; 
		}
		getCount = true;
		Target = GameObject.FindGameObjectWithTag ("Treasure");
		/*Quaternion rotation = Quaternion.LookRotation (Target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
		transform.localRotation = new Quaternion(0, 0, rotation.z, rotation.w);*/
		if (Target != null && player.transform.position.x>153f&&player.transform.position.y <0f ) {
			dir = Target.transform.position - player.transform.position;
			dir.z = 0;
			mypos = player.transform.up;
			mypos.z = 0;
			transform.Rotate (Quaternion.FromToRotation (transform.up, dir-mypos).eulerAngles);
		}
		else if (getCount) {
			if (player.transform.position.x < 153f && player.transform.position.y > 0f) {
				dir = SetTrans [0].transform.position - player.transform.position;
				dir.z = 0;
				mypos = player.transform.up;
				mypos.z = 0;
				transform.Rotate (Quaternion.FromToRotation (transform.up, dir-mypos).eulerAngles);
			}
			else if (player.transform.position.x > 153f && player.transform.position.y > 0f) {
				dir = SetTrans [1].transform.position - player.transform.position;
				dir.z = 0;
				mypos = player.transform.up;
				mypos.z = 0;
				transform.Rotate (Quaternion.FromToRotation (transform.up, dir-mypos).eulerAngles);
			}
			else if (player.transform.position.x < 153f && player.transform.position.y < 0f) {
				dir = SetTrans [2].transform.position - player.transform.position;
				dir.z = 0;
				mypos = player.transform.up;
				mypos.z = 0;
				transform.Rotate (Quaternion.FromToRotation (transform.up, dir-mypos).eulerAngles);
			}

		}


	}
}
