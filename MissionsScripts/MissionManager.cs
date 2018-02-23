using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {
	public GameObject mission1;
	public GameObject mission2;
	public GameObject mission3;
	public GameObject[] missions;
	public int[] MissionUsed;
	public int numOfMissions = 4;
	float time = 2f;
	float lastTime;
	int random;
	// Use this for initialization
	void Start () {
		MissionUsed = new int[numOfMissions];
		random = Random.Range (0, numOfMissions);
		Instantiate (missions [2], mission1.transform.position, Quaternion.identity, mission1.transform);
		mission1.GetComponent<MissionCtrl> ().mission_ID = random;
		MissionUsed[2] = 1;
		Instantiate (missions [(2+1)%numOfMissions], mission2.transform.position, Quaternion.identity, mission2.transform);
		mission2.GetComponent<MissionCtrl> ().mission_ID = random;
		MissionUsed[(2+1)%4] = 1;
		Instantiate (missions [(2+2)%numOfMissions], mission3.transform.position, Quaternion.identity, mission3.transform);
		mission3.GetComponent<MissionCtrl> ().mission_ID = random;
		MissionUsed[(2+2)%4] = 1;
		transform.localScale = new Vector3 (0, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		if (mission1.GetComponent<MissionCtrl> ().complete) {
			mission1.GetComponent<MissionCtrl> ().complete = false;
			Debug.Log ("Done1");
			transform.localScale = new Vector3 (1f, 1f, 1f);
			StartCoroutine ("ChangeMission",mission1);
		}
		if (mission2.GetComponent<MissionCtrl> ().complete) {
			mission2.GetComponent<MissionCtrl> ().complete = false;
			Debug.Log ("Done2");
			StartCoroutine ("ChangeMission",mission2);
		}
		if (mission3.GetComponent<MissionCtrl> ().complete) {
			mission3.GetComponent<MissionCtrl> ().complete = false;
			Debug.Log ("Done3");
			StartCoroutine ("ChangeMission",mission3);
		}
	}

	IEnumerator ChangeMission(GameObject mission){
		yield return new WaitForSeconds (1f);
		Destroy (mission.transform.GetChild (0).gameObject);
		random = Random.Range (0, numOfMissions);
		while (MissionUsed [random]==1) {
			random = (random + 1) % numOfMissions;
		}
		MissionUsed[mission.GetComponent<MissionCtrl>().mission_ID] = 0;
		MissionUsed[random] = 1;
		Instantiate (missions [random], mission.transform.position, Quaternion.identity, mission.transform);
		mission.GetComponent<MissionCtrl>().mission_ID = random;

	}
}
