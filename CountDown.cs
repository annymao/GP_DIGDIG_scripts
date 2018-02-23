using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CountDown : Photon.MonoBehaviour {
	public float countDown = 120f;
	public GameObject timeValue;
	float time = 0.5f;
	float lastspawn;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
			StartCountDown();

	}
	public void StartCountDown(){
		//timeValue = GameObject.Find("Canvas(Clone)").transform.Find("timeValue").gameObject;
		if (PhotonNetwork.isMasterClient)
			gameObject.GetComponent<PhotonView> ().RPC ("SetCountDown", PhotonTargets.All, countDown - Time.deltaTime);
		/*if(timeValue!=null)
			timeValue.GetComponent<Text>().text = ((int)countDown).ToString();*/
		if(countDown<0){
			countDown = 0;

				//PhotonNetwork.RemoveRPCs(gameObject.GetComponent<PhotonView>());
			PhotonNetwork.LoadLevel (3);
		}
	}
	[PunRPC]
	void SetCountDown(float c){
		countDown = c;
	}
	[PunRPC]
	void Clean(){
		PhotonNetwork.RemoveRPCs(gameObject.GetComponent<PhotonView>());
	}
}
