using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnSkip(){
		//GameObject.Find ("BGanimation").GetComponent<storyBGM> ().enabled = false;
		SceneManager.LoadScene (1);
	}
}
