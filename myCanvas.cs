using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myCanvas : Photon.MonoBehaviour {
	private Image imgHP;	
	private Image skillFilter;
	private float CDTime=10.0f;
	private float CDcounter;
	public GameObject kill;
	public GameObject dead;
	public GameObject player;
	public GameObject missionBoard;
	public bool skillAvailable;
	public float disableTime;
	public GameObject countDown;
	public bool getCount = false;
	public Text countValue;
	// Use this for initialization
	void Start () {
		GameObject HP =	GameObject.Find("HP");
		GameObject filter =	GameObject.Find("filter");
		player = GameObject.Find("Player");
		imgHP = HP.GetComponent<Image>();
		skillFilter = filter.GetComponent<Image>();
		CDcounter = 0;
		SetKillText ();
		skillAvailable = true;
		if ((PhotonNetwork.player.CustomProperties ["PlayerID"].GetHashCode ()) % 4== 0)
			disableTime = 8f;
		else if ((PhotonNetwork.player.CustomProperties ["PlayerID"].GetHashCode ()) % 4== 1)
			disableTime = 10f;
		else if ((PhotonNetwork.player.CustomProperties ["PlayerID"].GetHashCode ()) % 4== 2)
			disableTime = 5f;
		else if ((PhotonNetwork.player.CustomProperties ["PlayerID"].GetHashCode ()) % 4== 3)
			disableTime = 10f;
		StartCoroutine ("DelayGetCountDown");
	}

	IEnumerator DelayGetCountDown(){
		
		yield return new WaitUntil (() => (countDown = GameObject.Find("CountDownManager(Clone)"))!=null);
		getCount = true;

	}
	// Update is called once per frame
	void Update () {
		if (getCount) {
			countValue.text = countDown.GetComponent<CountDown> ().countDown.ToString("N");
		}
		imgHP.fillAmount = player.GetComponent<PlayerCtrl>().health / player.GetComponent<PlayerCtrl>().maxHealth;
		skillFilter.fillAmount = CDcounter/CDTime;
		if(Input.GetKey(KeyCode.Q)&&skillAvailable){
			
			CDcounter=CDTime;
			skillAvailable = false;
			player.GetComponent<PlayerCtrl> ().skill = true;
			player.GetComponent<SkillCtrl> ().spawn = true;
			if ((PhotonNetwork.player.CustomProperties ["PlayerID"].GetHashCode ()) % 4 == 0) {
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Water",1);
				int current_use = PhotonNetwork.player.CustomProperties ["SkillUse"].GetHashCode ();
				p.Add ("SkillUse",current_use+1);
				PhotonNetwork.player.SetCustomProperties (p);
			}
			else if((PhotonNetwork.player.CustomProperties ["PlayerID"].GetHashCode ()) % 4== 1){
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Lava",1);
				int current_use = PhotonNetwork.player.CustomProperties ["SkillUse"].GetHashCode ();
				p.Add ("SkillUse",current_use+1);
				PhotonNetwork.player.SetCustomProperties (p);
			}
			else if((PhotonNetwork.player.CustomProperties ["PlayerID"].GetHashCode ()) % 4== 3){
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Stone",1);
				int current_use = PhotonNetwork.player.CustomProperties ["SkillUse"].GetHashCode ();
				p.Add ("SkillUse",current_use+1);
				PhotonNetwork.player.SetCustomProperties (p);
			}
			else if((PhotonNetwork.player.CustomProperties ["PlayerID"].GetHashCode ()) % 4== 2){
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Cure",1);
				int current_use = PhotonNetwork.player.CustomProperties ["SkillUse"].GetHashCode ();
				p.Add ("SkillUse",current_use+1);
				PhotonNetwork.player.SetCustomProperties (p);
			}
			Invoke ("DisableSkill",disableTime);
		}
		if(CDcounter<=CDTime&&CDcounter>0){
			CDcounter-=Time.deltaTime;
		}
		else{
			CDcounter=0;
			skillAvailable = true;
		}
		
	}
	public void DisableSkill(){
		player.GetComponent<PlayerCtrl> ().skill = false;
	}
	public void SetKillText(){
		kill.GetComponent<Text> ().text = PhotonNetwork.player.CustomProperties ["Kill"].ToString();
	}
	public void SetDeadText(){
		dead.GetComponent<Text> ().text = PhotonNetwork.player.CustomProperties ["DeadTimes"].ToString();
	}
	public void OnMissionBoard(){
		
		missionBoard.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
	public void OnMissionBoardExit(){
		missionBoard.transform.localScale = new Vector3 (0, 0, 0);
	}
	public void OnCallFriends(){
		
	}
}
