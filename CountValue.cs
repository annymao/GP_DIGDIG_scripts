using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountValue : MonoBehaviour {


	// 主要數值
	public int Score = 0;
	public int KillCount = 0;
	public int DeadCount = 0;
	public int AssistCount = 0;
	public int Boss = 0;
	public float KDA = 0;

	// 次要用值
	public int DragonKillCount = 0;
	public int TurtleKillCount = 0;
	public int MudKillCount = 0;
	public int DigCount = 0;
	public int SkillUsedCount = 0;

	void Start () {
		// 此區為DEBUG設的值(可改)
		//////////////////////////////////////////////////////////////////////////
		DragonKillCount = PhotonNetwork.player.CustomProperties ["DragonKill"].GetHashCode ();
		TurtleKillCount = PhotonNetwork.player.CustomProperties ["TurtleKill"].GetHashCode ();
		MudKillCount = PhotonNetwork.player.CustomProperties ["MudKill"].GetHashCode ();
		DigCount = PhotonNetwork.player.CustomProperties ["Dig"].GetHashCode ();
		SkillUsedCount = PhotonNetwork.player.CustomProperties ["SkillUse"].GetHashCode ();
		Boss = PhotonNetwork.player.CustomProperties ["Boss"].GetHashCode ();

		Score = 100*DragonKillCount+20*TurtleKillCount+50*MudKillCount+DigCount+Boss*2000;
		KillCount = PhotonNetwork.player.CustomProperties ["Kill"].GetHashCode ();/*DragonKillCount + TurtleKillCount + MudKillCount*/;
		DeadCount = PhotonNetwork.player.CustomProperties ["DeadTimes"].GetHashCode ();
		AssistCount = 0;
		KDA = ((float)KillCount + (float)SkillUsedCount) / ((float)DeadCount+1f);

		//////////////////////////////////////////////////////////////////////////

		GiveTitles();
	}

	// 給予稱號
	void GiveTitles() {
		ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
		if (Score >= 5000) {
			p.Add ("Title1",1);
			Debug.Log ("Get1");
		}

		if (DragonKillCount >= 5) {
			p.Add ("Title2",1);
			Debug.Log ("Get2");
		}

		if (TurtleKillCount >= 10) {
		}

		if (SkillUsedCount <= 5) {
			p.Add ("Title3",1);
			Debug.Log ("Get3");
		}

		if (DigCount >= 1000) {

		}

		if (KillCount >= 50) {
			p.Add ("Title4",1);
			Debug.Log ("Get4");
		}
		PhotonNetwork.player.SetCustomProperties (p);
	}
		

}
