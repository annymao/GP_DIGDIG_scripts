using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCtrl : Photon.MonoBehaviour {
	public float start_x;
	public float start_y;
	public GameObject terrain;
	//private PolyGen tScript;
	public bool findTreasure = false;
	public float health = 200f;
	public float maxHealth = 200f;
	public bool inWater = false;
	//public int count=0;
	public float damage;
	public bool dead;
	myCanvas cScript;
	private Animator anim;
	public LayerMask groundLayer;
	public GameObject groundCheck;
	bool OnGround=false;
	private bool facingRight = true;
	public float jumpForce = 15f;
	public GameObject Hint_Canvas;
	public GameObject lava;	
	public GameObject water;
	public float count;
	public GameObject deadPanel;
	public GameObject TransportPanel;
	public bool isTrans;
	public bool skill;
	public bool water_skill;
	public bool lava_skill;
	float time = 0.5f;
	float last_spawn;
	// Use this for initialization
	void Start () {
		start_x = transform.position.x;
		start_y = transform.position.y;
		dead = false;
		cScript = GameObject.Find ("Canvas(Clone)").GetComponent<myCanvas> ();
		Hint_Canvas = GameObject.Find ("Hint_Canvas");
		anim = gameObject.GetComponent<Animator>();
		deadPanel = GameObject.Find ("DeadPanel");
		TransportPanel = GameObject.Find ("TransportPanel");
		isTrans = false;
		skill = false;
		//tScript = GameObject.Find("terrain").GetComponent("PolyGen") as PolyGen;
	}
	// Update is called once per frame
	void Update () {
		if (dead) {
			/*if (deadPanel.GetComponent<RectTransform> ().localScale.x < 1f)
				deadPanel.GetComponent<RectTransform> ().localScale = deadPanel.GetComponent<RectTransform> ().localScale + new Vector3 (Time.deltaTime, Time.deltaTime, 0f);*/
			float temp = deadPanel.GetComponent<Image> ().color.a;
			if(temp<1)
				deadPanel.GetComponent<Image> ().color= new Color (0f, 0f, 0f, temp + Time.deltaTime);
		} else {
			float temp = deadPanel.GetComponent<Image> ().color.a;
			/*if (deadPanel.GetComponent<RectTransform> ().localScale.x >0f)
				deadPanel.GetComponent<RectTransform> ().localScale = deadPanel.GetComponent<RectTransform> ().localScale - new Vector3 (Time.deltaTime, Time.deltaTime, 0f);*/
			if(temp>0)
				deadPanel.GetComponent<Image> ().color = new Color (0f, 0f, 0f, temp - Time.deltaTime);
		}
		if (isTrans) {
			Hint_Canvas.transform.GetChild (0).gameObject.SetActive (false);
			float temp2 = TransportPanel.GetComponent<Image> ().color.a;
			if(temp2<1)
				TransportPanel.GetComponent<Image> ().color= new Color (255f, 255f, 255f, temp2 + Time.deltaTime);
		}else {
			float temp2 = TransportPanel.GetComponent<Image> ().color.a;
			if(temp2>0)
				TransportPanel.GetComponent<Image> ().color = new Color (255f, 255f, 255f, temp2- Time.deltaTime);
		}
		ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
		p.Add ("Position", transform.position);
		PhotonNetwork.player.SetCustomProperties (p);

		if (Input.GetKeyDown (KeyCode.K)) {
			dead = true;
			Invoke ("Respawn", 2.0f);
		}
		if(Input.GetMouseButtonDown(1)&& last_spawn+time < Time.time){
			Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			GameObject ball = PhotonNetwork.Instantiate("Ball",transform.position,Quaternion.identity,0);
			Vector3 dir = new Vector3(pz.x,pz.y,transform.position.z) - transform.position;
			ball.GetComponent<Rigidbody>().AddForce(dir.normalized * 300f);
			last_spawn = Time.time;
		}
		lava = GameObject.Find ("GenLava(Clone)");
		if (lava != null&&PhotonNetwork.player.CustomProperties["Lava"].GetHashCode()==0/*((PhotonNetwork.player.CustomProperties["PlayerID"].GetHashCode()==1&&!skill)||(PhotonNetwork.player.CustomProperties["PlayerID"].GetHashCode()!=1&&!lava_skill))*/) {
			if (lava.GetComponent<Lava> ().blocks != null) {
				//Debug.Log ((int)(transform.position.y+0.5f-lava.GetComponent<Lava> ().offset_y) >0);
				if((int)(transform.position.x-lava.GetComponent<Lava> ().offset_x) < lava.GetComponent<Lava> ().blocks.GetLength (0) && (int)(transform.position.y+0.5f-lava.GetComponent<Lava> ().offset_y) < lava.GetComponent<Lava> ().blocks.GetLength (1)&& (int)(transform.position.y+0.5f-lava.GetComponent<Lava> ().offset_y) >0) {
					//Debug.Log ("123");
					if (lava.GetComponent<Lava> ().blocks [(int)(transform.position.x-lava.GetComponent<Lava> ().offset_x), (int)(transform.position.y+0.5f-lava.GetComponent<Lava> ().offset_y)] != 0 && dead == false ) {
							dead = true;
							Invoke ("Respawn", 2.0f);
					}else if (lava.GetComponent<Lava> ().blocks [(int)(transform.position.x-lava.GetComponent<Lava> ().offset_x), (int)(transform.position.y+0.5f-lava.GetComponent<Lava> ().offset_y)] == 0) {
						count = 0;
					}
				}
			}
		}
		water = GameObject.Find ("GenWater(Clone)");
		if (water != null&& PhotonNetwork.player.CustomProperties["Water"].GetHashCode()==0/*((PhotonNetwork.player.CustomProperties["PlayerID"].GetHashCode()==0&&!skill)||(PhotonNetwork.player.CustomProperties["PlayerID"].GetHashCode()!=0&&!water_skill))*/) {
			if (water.GetComponent<Lava> ().blocks != null) {
				if((int)(transform.position.x-water.GetComponent<Lava> ().offset_x) >0&&transform.position.x-water.GetComponent<Lava> ().offset_x < water.GetComponent<Lava> ().blocks.GetLength (0) && (int)(transform.position.y+0.5f-water.GetComponent<Lava> ().offset_y) < lava.GetComponent<Lava> ().blocks.GetLength (1)&& (int)(transform.position.y+0.5f-water.GetComponent<Lava> ().offset_y) >0) {
					if (water.GetComponent<Lava> ().blocks [(int)(transform.position.x-water.GetComponent<Lava> ().offset_x), (int)(transform.position.y+0.5f-water.GetComponent<Lava> ().offset_y)] != 0 && dead == false ) {
						StartCoroutine("hitAndChangeColor");
						if (health <=0) {
							dead = true;
							Invoke ("Respawn", 2.0f);
						} else {
							health -= 5 * Time.deltaTime;
						}
					}else if (water.GetComponent<Lava> ().blocks [(int)(transform.position.x-water.GetComponent<Lava> ().offset_x), (int)(transform.position.y+0.5f-water.GetComponent<Lava> ().offset_y)] == 0) {
						count = 0;
					}
				}
			}
		}
		 
		if (PhotonNetwork.player.CustomProperties ["Cure"].GetHashCode() == 1) {
			health += 10;
			if(health>maxHealth)
				health = maxHealth;
			ExitGames.Client.Photon.Hashtable c = new ExitGames.Client.Photon.Hashtable ();
			c.Add ("Cure",0);
			PhotonNetwork.player.SetCustomProperties (c);
				
		}


	}
	void OnGUI(){
		GUILayout.BeginArea (new Rect (750, 0, 200, 500));
		Vector3 pos = (Vector3)(PhotonNetwork.player.CustomProperties ["Position"]);
//		Debug.Log (PhotonNetwork.player.CustomProperties ["Position"]);
		GUILayout.Box(PhotonNetwork.player.NickName +  ": x: "+ (int)pos.x+" y: "+ (int)pos.y );
		if(Input.GetKey(KeyCode.LeftShift)){
			foreach (PhotonPlayer player in PhotonNetwork.otherPlayers) {
				pos = (Vector3)(player.CustomProperties ["Position"]);
				GUILayout.Box( player.NickName + ": x: "+ (int)pos.x+" y: "+ (int)pos.y );
			}
		}
		GUILayout.EndArea ();
	}
	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if (!dead&&!isTrans) {
			move ();
		}
		/*if(Input.GetKey(KeyCode.W)){
			Vector3 direct = new Vector3(0f,5f,0f);
			transform.Translate(direct * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.A)){
			Vector3 direct = new Vector3(-2f,0f,0f);
			//transform.Translate(direct * Time.deltaTime);
			transform.position += new Vector3(-2f*Time.deltaTime,0.0f,0.0f);
			gameObject.GetComponent<SpriteRenderer>().flipX = false;
			//Debug.Log("hit");
		}
		if(Input.GetKey(KeyCode.D)){
			Vector3 direct = new Vector3(2f,0f,0f);
			transform.Translate(direct * Time.deltaTime);
			gameObject.GetComponent<SpriteRenderer>().flipX = true;
			//Debug.Log("hit");
			
		}*/
		/*float move = Input.GetAxis("Horizontal");

		GetComponent<Rigidbody2D>().velocity = new Vector2(move*1f,GetComponent<Rigidbody2D>().velocity.y);*/
	}
	void move(){
		Collider[] touched = Physics.OverlapSphere(groundCheck.transform.position,0.18f,groundLayer);

		if(touched.Length!=0){
			anim.SetBool("onGround",true);
		}
		else{
			anim.SetBool("onGround",false);
		}
		if(Input.GetKey(KeyCode.W)){
				/*Vector3 direct = new Vector3(0f,5f,0f);
				transform.Translate(direct * Time.deltaTime);*/
			if(touched.Length!=0)
				gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f,1f,0f)*jumpForce);
		}
		if(Input.GetKey(KeyCode.A)){
			Vector3 direct = new Vector3(-5f,0f,0f);
			if(facingRight){
				transform.GetComponent<PhotonView> ().RPC("Flip",PhotonTargets.All,null);
			}

			transform.Translate(direct * Time.deltaTime);

			//transform.position += new Vector3(-2f*Time.deltaTime,0.0f,0.0f);

			anim.SetBool("walk",true);

			//gameObject.GetComponent<SpriteRenderer>().flipX = false;

			//Debug.Log("hit");
		}

		if(Input.GetKey(KeyCode.D)){
			Vector3 direct = new Vector3(5f,0f,0f);
			if(!facingRight){
				//Flip();
				transform.GetComponent<PhotonView> ().RPC("Flip",PhotonTargets.All,null);
			}
			transform.Translate(direct * Time.deltaTime);

			anim.SetBool("walk",true);

			//gameObject.GetComponent<SpriteRenderer>().flipX = true;
			//Debug.Log("hit");

		}
		/*if(Input.GetKeyDown(KeyCode.Q)){
			GameObject Partical = PhotonNetwork.Instantiate("fire",transform.position,Quaternion.identity,0) as GameObject;
			Partical.transform.parent = this.transform;
		}*/
		if(Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D)){
			anim.SetBool("walk",false);
		}
	}
	[PunRPC]
	void Flip(){
		facingRight = !facingRight;

		Vector3 characterScale = transform.localScale;

		characterScale.x*=-1;
		transform.localScale = characterScale;

	}
	void Respawn(){
		transform.position = new Vector3 (start_x, start_y, transform.position.z);
		health = maxHealth;
		dead = false;
		int currentDead = PhotonNetwork.player.CustomProperties ["DeadTimes"].GetHashCode();
		ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
		p.Add ("DeadTimes", currentDead+1);
		PhotonNetwork.player.SetCustomProperties (p);
		if(cScript!=null)
			cScript.SetDeadText ();
		//Debug.Log(currentDead+1);
		count = 0;
	}
	IEnumerator hitAndChangeColor(){
		for(int i=0;i<5;i++){
			GetComponent<SpriteRenderer>().color = new Color32(255, 20, 20, 255);
			print("attack red");
			yield return new WaitForSeconds(0.1f);
			GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
			print("attack black");
			yield return new WaitForSeconds(0.1f);
		}
	}
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	/// <summary>
	/// OnTriggerStay is called once per frame for every Collider other
	/// that is touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Fly") {
			PhotonNetwork.Instantiate ("Character_Water_Proof",transform.position,Quaternion.identity,0);
		}
		if(other.gameObject.tag == "Treasure" )
		{
			findTreasure = true;
			Debug.Log("FIND!");
		}
		if (other.gameObject.tag == "AngelCircle") {
			StartCoroutine("hitAndChangeColor");
			if (health > 0)
				health -=10f;
			else if(dead == false){
				dead = true;
				Invoke ("Respawn", 2.0f);
			}
		}
		/*if (other.gameObject.name = "water(Clone)") {
			water_skill = true;
		}
		if (other.gameObject.name = "fire(Clone)") {
			lava_skill = true;
		}*/
		/*if (other.gameObject.tag == "Hint") {
			if (Hint_Canvas != null) {
				GameObject hint_panel = Hint_Canvas.transform.GetChild (0).gameObject;
				hint_panel.SetActive (true);
				Hint_Canvas.GetComponent<HintCanvas> ().setText ((int)(other.gameObject.GetComponent<Hint> ().pos.x), (int)(other.gameObject.GetComponent<Hint> ().pos.y));
			}
		}*/
		if (other.gameObject.tag == "Transport"&& isTrans == false) {
			if (Hint_Canvas != null) {
				Transport trans_script = other.gameObject.GetComponent<Transport> ();
				if (other.gameObject.GetComponent<Transport> ().tag == 0) {
					HintCanvas h_script = Hint_Canvas.GetComponent<HintCanvas> ();
					h_script.spawn1.interactable = true;
					h_script.spawn2.interactable = true;
					h_script.spwanPoint1 = new Vector3 (start_x, start_y, transform.position.z);
					h_script.spwanPoint2 = new Vector3 (trans_script.to_pos.x, trans_script.to_pos.y, transform.position.z);
				} else if (other.gameObject.GetComponent<Transport> ().tag == 1) {
					Hint_Canvas.GetComponent<HintCanvas> ().spawn3.interactable = true;
					Hint_Canvas.GetComponent<HintCanvas> ().spwanPoint3 = new Vector3 (trans_script.to_pos.x, trans_script.to_pos.y, transform.position.z);
				} else if (other.gameObject.GetComponent<Transport> ().tag == 2) {
					Hint_Canvas.GetComponent<HintCanvas> ().spawn4.interactable = true;
					Hint_Canvas.GetComponent<HintCanvas> ().spwanPoint4 = new Vector3 (trans_script.to_pos.x, trans_script.to_pos.y, transform.position.z);
				}
				Hint_Canvas.transform.GetChild (0).gameObject.SetActive (true);
			}
			/*isTrans = true;
			Invoke ("Trans", 3f);*/

		}
		if (other.gameObject.tag == "Cure") {
			ExitGames.Client.Photon.Hashtable c = new ExitGames.Client.Photon.Hashtable ();
			c.Add ("Cure",1);
			PhotonNetwork.player.SetCustomProperties (c);
		}
		
		
	}
	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Ability") {
			PlayerCtrl player = other.gameObject.GetComponent<PlayerCtrl> ();
			if (other.gameObject.name == "water(Clone)") {
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Water",1);
				PhotonNetwork.player.SetCustomProperties (p);
			}
			if (other.gameObject.name == "fire(Clone)") {
				ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
				p.Add ("Lava",1);
				PhotonNetwork.player.SetCustomProperties (p);
			}
		}
	}
	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Roll(Clone)") {
			StartCoroutine("hitAndChangeColor");
			//PhotonNetwork.Destroy (other.gameObject);
			if (health > 0)
				health -=10f;
			else if(dead == false){
				dead = true;
				Invoke ("Respawn", 2.0f);
			}
		}
		if (other.gameObject.tag == "FireBall") {
			StartCoroutine("hitAndChangeColor");
			if (health > 0)
				health -=30f;
			else if(dead == false){
				dead = true;
				Invoke ("Respawn", 2.0f);
			}
		}
		
	}
	/// <summary>
	/// OnCollisionStay is called once per frame for every collider/rigidbody
	/// that is touching rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionStay(Collision other)
	{
		if(other.gameObject.tag =="Enemy"){
			if(transform.position.y<=other.transform.position.y+1f){
				if (health > 0)
					health -=0.1f;
				else if(dead == false){
					dead = true;
					Invoke ("Respawn", 2.0f);
				}


			}

		}

	}
	/// <summary>
	/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "DynamicParticle" )
		{
			//inWater = false;
			//findTreasure = true;
			count --;
			Debug.Log("LeaveWater");
		}
	}


}
