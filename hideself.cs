using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideself : MonoBehaviour {
	public GameObject skip;
	public void hideSelf(){
		skip.SetActive (true);
		this.gameObject.SetActive(false);
	}
}
