using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.MonoBehaviour {
	public GameObject Canvas;
	public SpawnSpot[] spawnSpots;
	public bool isConnected=false;
	public Sprite[] playerSprite;
	GameObject myPlayerGO;
	GameObject[] monsterNest;
	// Use this for initialization
	void Start () {
		//PhotonNetwork.ConnectUsingSettings ("DIG DIG CHUA 1.0");
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		InitialMap();
		SpawnMyplayer ();
		isConnected = true;
	}
	/*public override void OnConnectedToMaster(){
		Debug.Log("Conneted to master");
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}
	public override void OnJoinedLobby(){
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnPhotonRandomJoinFailed(){
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
	}
	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		InitialMap();
		SpawnMyplayer ();
		isConnected = true;
	}*/
	public void InitialMap(){
		if(PhotonNetwork.isMasterClient){
			Debug.Log ("initialMap");
			int y = Random.Range(-90,90);
			int x = Random.Range (10, 300);
			Vector3 pos = new Vector3(0f,0f,10f);
			Vector3 pos2 = new Vector3(0f,-117f,10f);
			Vector3 pos3 = new Vector3(154f,0f,10f);
			Vector3 pos4 = new Vector3(154f,-117f,10f);

			PhotonNetwork.InstantiateSceneObject("terrain", pos, Quaternion.identity,0,null);
			PhotonNetwork.InstantiateSceneObject("terrain3", pos3, Quaternion.identity,0,null);
			PhotonNetwork.InstantiateSceneObject("terrain2", pos2, Quaternion.identity,0,null);
			PhotonNetwork.InstantiateSceneObject("terrain4", pos4, Quaternion.identity,0,null);
			PhotonNetwork.InstantiateSceneObject("GenWater", new Vector3 (154f, 0f, 9.9f), Quaternion.identity, 0,null);
			PhotonNetwork.InstantiateSceneObject("GenLava", new Vector3 (0f, -117f, 9.9f), Quaternion.identity, 0,null);
			SpawnHoles ();
			SpawnEmpty ();
			SpawnMonsterNest ();
			CreateSpawnSpotHoles ();
			//SpawnHint ();
			spawnTransport ();
			//PhotonNetwork.InstantiateSceneObject("SpawnHoles", new Vector3 (287f, 103f, 10f), Quaternion.identity, 0,null);
			//monsterNest = GameObject.FindGameObjectsWithTag ("MonsterNest");

			/*int r = Random.Range (0, monsterNest.GetLength (0));
			GameObject hint;*/
			//PhotonNetwork.InstantiateSceneObject("Hint", new Vector3 (287f, 103f, 10f), Quaternion.identity, 0,null);
			/*Vector2 m_pos = new Vector2 (monsterNest [r].transform.position.x, monsterNest [r].transform.position.y);
			hint.GetComponent<Hint>().setPos(m_pos);*/

			// PhotonNetwork.InstantiateSceneObject("Hint", new Vector3 (25f, 68f, 10.1f), Quaternion.identity, 0,null);
			/*r = Random.Range (0, monsterNest.GetLength (0));
			m_pos = new Vector2 (monsterNest [r].transform.position.x, monsterNest [r].transform.position.y);
			hint.GetComponent<Hint>().setPos(m_pos);*/

			PhotonNetwork.InstantiateSceneObject("CountDownManager", new Vector3 (0f, 0f, 0f), Quaternion.identity, 0,null);

			x = Random.Range (170, 300);
			y = Random.Range (-100, -80);
			while (IsSpawnPoint ((float)x)) {
				x = Random.Range (170, 300);
			}
			PhotonNetwork.InstantiateSceneObject("emptyHoles", new Vector3 ((float)(x -2), (float)y, 10f), Quaternion.identity, 0,null);
			PhotonNetwork.InstantiateSceneObject("treasure", new Vector3 ((float)x, (float)y+1f, 10f), Quaternion.identity, 0,null);
			//PhotonNetwork.InstantiateSceneObject("Angle", new Vector3 ((float)x, (float)y, 10f), Quaternion.identity, 0,null);
		}
				
		
		Instantiate(Canvas,new Vector3 (0f, 0f, 10f) , Quaternion.identity);
	}
	void spawnTransport(){
		int x1,y1;
		int x2,y2;
		GameObject trans;
		x1 = Random.Range(5,153);
		y1 = Random.Range (5, 30);
		x2 = Random.Range(155,170);
		y2 = Random.Range(70,90);
		trans = PhotonNetwork.InstantiateSceneObject("transport",new Vector3 ((float)x1, (float)y1, 10.1f), Quaternion.identity, 0,null);
		trans.GetComponent<PhotonView> ().RPC ("SetPos", PhotonTargets.All, new Vector3 ((float)x2, (float)y2, 10f), 0);
		/*PhotonNetwork.InstantiateSceneObject("transport",new Vector3 ((float)x2, (float)y2, 10.1f), Quaternion.identity, 0,null);
		trans.GetComponent<Transport> ().to_pos = new Vector3 ((float)x1, (float)y1, 10f);*/
		PhotonNetwork.InstantiateSceneObject("emptyHoles", new Vector3 ((float)x1, (float)y1, 10f), Quaternion.identity, 0,null);
		PhotonNetwork.InstantiateSceneObject("emptyHoles", new Vector3 ((float)x2, (float)y2, 10f), Quaternion.identity, 0,null);
		x1 = Random.Range(200,300);
		y1 = Random.Range (5, 30);
		x2 = Random.Range(5,100);
		y2 = Random.Range(-20,-5);
		trans = PhotonNetwork.InstantiateSceneObject("transport",new Vector3 ((float)x1, (float)y1, 10.1f), Quaternion.identity, 0,null);
		trans.GetComponent<PhotonView> ().RPC ("SetPos", PhotonTargets.All, new Vector3 ((float)x2, (float)y2, 10f), 1);
		/*PhotonNetwork.InstantiateSceneObject("transport",new Vector3 ((float)x2, (float)y2, 10.1f), Quaternion.identity, 0,null);
		trans.GetComponent<Transport> ().to_pos = new Vector3 ((float)x1, (float)y1, 10f);*/
		PhotonNetwork.InstantiateSceneObject("emptyHoles", new Vector3 ((float)x1, (float)y1, 10f), Quaternion.identity, 0,null);
		PhotonNetwork.InstantiateSceneObject("emptyHoles", new Vector3 ((float)x2, (float)y2, 10f), Quaternion.identity, 0,null);
		x1 = Random.Range(100,150);
		y1 = Random.Range (-110, -100);
		x2 = Random.Range(155,175);
		y2 = Random.Range(-30,-20);
		trans = PhotonNetwork.InstantiateSceneObject("transport",new Vector3 ((float)x1, (float)y1, 10.1f), Quaternion.identity, 0,null);
		trans.GetComponent<PhotonView> ().RPC ("SetPos", PhotonTargets.All, new Vector3 ((float)x2, (float)y2, 10f), 2);
		/*PhotonNetwork.InstantiateSceneObject("transport",new Vector3 ((float)x2, (float)y2, 10.1f), Quaternion.identity, 0,null);
		trans.GetComponent<Transport> ().to_pos = new Vector3 ((float)x1, (float)y1, 10f);*/
		PhotonNetwork.InstantiateSceneObject("emptyHoles", new Vector3 ((float)x1, (float)y1, 10f), Quaternion.identity, 0,null);
		PhotonNetwork.InstantiateSceneObject("emptyHoles", new Vector3 ((float)x2, (float)y2, 10f), Quaternion.identity, 0,null);

	}
	void SpawnHint(){
		int y;
		int x;
		for (int i = 0; i < 10; i++) {
			y = Random.Range(-100,-20);
			x = Random.Range (10, 300);
			PhotonNetwork.InstantiateSceneObject("Hint",new Vector3 ((float)x, (float)y, 10.1f), Quaternion.identity, 0,null);
		}
		for (int i = 0; i < 10; i++) {
			y = Random.Range(0,100);
			x = Random.Range (10, 300);
			PhotonNetwork.InstantiateSceneObject("Hint", new Vector3 ((float)x, (float)y, 10.1f), Quaternion.identity, 0,null);
		}
	}
	public bool IsSpawnPoint(float x){
		for (int i = 0; i < spawnSpots.GetLength (0); i++) {
			if (spawnSpots [i].transform.position.x-5f <= x && x<=spawnSpots [i].transform.position.x+5f)
				return true;
		}
		return false;
	}
	public void CreateSpawnSpotHoles(){
		for (int i = 0; i < spawnSpots.GetLength (0); i++) {
			PhotonNetwork.InstantiateSceneObject("SpawnHoles", spawnSpots[i].transform.position, Quaternion.identity, 0,null);
		}
	}
	public void SpawnHoles(){
		int y;
		int x;
		for (int i = 0; i <10; i++) {
			y = Random.Range(-90,-30);
			x = Random.Range (10, 152);
			PhotonNetwork.InstantiateSceneObject("Holes", new Vector3 ((float)x, (float)y, 10f), Quaternion.identity, 0,null);
			if (x < 153) {
				GameObject lava = GameObject.Find ("GenLava(Clone)");
				lava.GetComponent<PhotonView>().RPC("SetInitialBlock",PhotonTargets.All,x, y - (int)lava.GetComponent<Lava>().offset_y);
			}
				

		}
		for (int i = 0; i < 10; i++) {
			y = Random.Range(5,90);
			x = Random.Range (154, 300);
			PhotonNetwork.InstantiateSceneObject("Holes", new Vector3 ((float)x, (float)y, 10f), Quaternion.identity, 0,null);
			if (x >153) {
				GameObject water = GameObject.Find ("GenWater(Clone)");
				//Debug.Log (x + "/" + (y - (int)lavaScript.offset_y));
				water.GetComponent<PhotonView>().RPC("SetInitialBlock",PhotonTargets.All,x- (int)water.GetComponent<Lava>().offset_x, y );
			}
		}
	}
	public void  SpawnEmpty(){
		int y;
		int x;
		for (int i = 0; i < 10; i++) {
			y = Random.Range(-100,90);
			x = Random.Range (10, 300);
			PhotonNetwork.InstantiateSceneObject("emptyHoles", new Vector3 ((float)x, (float)y, 10f), Quaternion.identity, 0,null);
		}
	}
	public void SpawnMonsterNest(){
		int y;
		int x;
		GameObject Monster;
		for (int i = 0; i < 30; i++) {
			y = Random.Range(-100,-20);
			x = Random.Range (10, 150);
			Monster = PhotonNetwork.InstantiateSceneObject("MonsterNest", new Vector3 ((float)x, (float)y, 10f), Quaternion.identity, 0,null);
			x = Random.Range (0, Monster.GetComponent<SpawnMonster> ().MaxMonster);
			Monster.GetComponent<PhotonView> ().RPC ("SetRandom", PhotonTargets.All, x);
		}
		for (int i = 0; i < 30; i++) {
			y = Random.Range(-100,-20);
			x = Random.Range (155, 300);
			Monster = PhotonNetwork.InstantiateSceneObject("MonsterNest", new Vector3 ((float)x, (float)y, 10f), Quaternion.identity, 0,null);
			x = Random.Range (0, Monster.GetComponent<SpawnMonster> ().MaxMonster);
			Monster.GetComponent<PhotonView> ().RPC ("SetRandom", PhotonTargets.All, x);
		}
		for (int i = 0; i < 30; i++) {
			y = Random.Range(0,100);
			x = Random.Range (10, 150);
			Monster = PhotonNetwork.InstantiateSceneObject("MonsterNest", new Vector3 ((float)x, (float)y, 10f), Quaternion.identity, 0,null);
			x = Random.Range (0, Monster.GetComponent<SpawnMonster> ().MaxMonster);
			Monster.GetComponent<PhotonView> ().RPC ("SetRandom", PhotonTargets.All, x);
		}
		for (int i = 0; i < 30; i++) {
			y = Random.Range(0,100);
			x = Random.Range (155, 300);
			Monster = PhotonNetwork.InstantiateSceneObject("MonsterNest", new Vector3 ((float)x, (float)y, 10f), Quaternion.identity, 0,null);
			x = Random.Range (0, Monster.GetComponent<SpawnMonster> ().MaxMonster);
			Monster.GetComponent<PhotonView> ().RPC ("SetRandom", PhotonTargets.All, x);
		}

	}
	void SpawnMyplayer()
	{
		Debug.Log (PhotonNetwork.player.CustomProperties ["PlayerID"]);
		Vector3 pos = spawnSpots[(int)PhotonNetwork.player.CustomProperties["PlayerID"]].transform.position;
		ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
		//p.Add ("Ability", 0);
		myPlayerGO = (GameObject)PhotonNetwork.Instantiate("Character", pos, Quaternion.identity,0);
		myPlayerGO.name = "Player";
		Debug.Log(myPlayerGO.name);
		GameObject.Find ("BGM").GetComponent<BGM> ().playBGM (myPlayerGO.transform.position);
		if(myPlayerGO!=null)
		{
			
			myPlayerGO.GetComponent<PlayerCtrl> ().enabled = true;
			myPlayerGO.GetComponent<RayCast> ().enabled = true;
			myPlayerGO.GetComponent<SetSprite> ().MySprite ((int)PhotonNetwork.player.CustomProperties["PlayerID"]);
			myPlayerGO.GetComponentInChildren<DigCtrl> ().enabled = true;
			myPlayerGO.GetComponentInChildren<AxeCtrl> ().enabled = true;
			//myPlayerGO.GetComponentInChildren<HintCanvas> ().enabled = true;
			myPlayerGO.GetComponentInChildren<SkillCtrl> ().enabled = true;
			myPlayerGO.GetComponentInChildren<SkillCtrl> ().myPlayerID((int)PhotonNetwork.player.CustomProperties["PlayerID"]);
			myPlayerGO.transform.Find("Main Camera").gameObject.SetActive(true);
		}

	}
	// Update is called once per frame
	void Update () {

	}
}
