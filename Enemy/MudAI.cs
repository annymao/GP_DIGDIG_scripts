using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MudAI : Photon.MonoBehaviour {
	public LayerMask enemyMask;
	float moveSpeed = 5.0f;
	public float timeInterval = 0.0f;
	float catchTime;
	float startTime;
	Rigidbody rigid;
	private int direction = 0; // right(0), left(1)
	public bool isGrounded = false;
	public int hitCount = 0;
	string groundName = "Cube";
	private Animator mudAni;
	public float existTime; 

	private SpriteRenderer sprd;
	public bool initDone = false; 
	public bool destoryed = false;
	private AudioSource audio;
	public AudioClip rour;
	float count=25f;
	bool shout=false;

	void initAni() {
		if (sprd.color.a < 1.0f)
			sprd.color = new Color (sprd.color.r, sprd.color.g, sprd.color.b, sprd.color.a + 0.01f);
		else {
			initDone = true;
		}
	}

	void disappearAni() {
		if (sprd.color.a > 0.0f) {
			sprd.color = new Color (sprd.color.r, sprd.color.g, sprd.color.b, sprd.color.a - 0.01f);
		} else {
			print ("Destroy");
			destoryed = true;
			/*if(gameObject.GetComponent<PhotonView>().isMine)
				PhotonNetwork.Destroy(gameObject);*/
			Destroy (gameObject);
		}
	}
		
	// Use this for initialization
	void Start () {
		timeInterval = (float)Random.Range (3, 5);
		catchTime = Time.time;
		startTime = catchTime;
		print (startTime);
		rigid = this.GetComponent<Rigidbody> ();
		mudAni = this.GetComponent<Animator> ();
		mudAni.SetInteger ("State", 0);
		sprd = this.GetComponent<SpriteRenderer> ();
		sprd.color = new Color (sprd.color.r, sprd.color.g, sprd.color.b, 0.0f);
		existTime = (float) Random.Range(10, 15);
		audio=gameObject.GetComponent<AudioSource>();
	}
	// Update is called once per frame
	void Update () {
		// Left line ray
		Debug.DrawLine (new Vector3(this.transform.position.x, this.transform.position.y, 0.0f), new Vector3 (this.transform.position.x - sprd.bounds.extents.x - 0.1f, this.transform.position.y, 0.0f), Color.red);
		// Right line ray
		Debug.DrawLine (new Vector3(this.transform.position.x, this.transform.position.y, 0.0f), new Vector3 (this.transform.position.x + sprd.bounds.extents.x + 0.1f, this.transform.position.y, 0.0f), Color.red);
		RaycastHit hit;
		if (Physics.Linecast(new Vector3(this.transform.position.x, this.transform.position.y, 0.0f), new Vector3 (this.transform.position.x - sprd.bounds.extents.x - 0.1f, this.transform.position.y, 0.0f), out hit, enemyMask) || Physics.Linecast(new Vector3(this.transform.position.x, this.transform.position.y, 0.0f), new Vector3 (this.transform.position.x + sprd.bounds.extents.x + 0.1f, this.transform.position.y, 0.0f), out hit, enemyMask)) {
			//print (hit.collider.name);
			if (hit.collider.tag == "Terrain") {
				hitCount++;
				if (hitCount > 30) {
					print ("hit");
					direction = ~direction;
					hitCount = 0;
				}
			}
		}

		if (isGrounded && initDone)
			Move ();
		if(count>3f){
			count=0;
			audio.PlayOneShot(rour,1f);
		}
	}

	private void FixedUpdate() {
		if (!initDone)
			initAni ();

		if (Time.time - startTime > existTime && !destoryed) {
			//print ("Disappearing");
			disappearAni ();
		}
	} 

	private void Move() {
		if (direction == 0) {
			rigid.velocity = Vector2.right * moveSpeed;
			mudAni.SetInteger ("State", 2);
		} else {
			rigid.velocity = Vector2.left * moveSpeed;
			mudAni.SetInteger ("State", 1);
		}

		if (Time.time - catchTime > timeInterval && !destoryed) {
			direction = ~direction;
			catchTime = Time.time;
			timeInterval = (float)Random.Range (3, 5);
		}
	}

	private void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Terrain") {
				isGrounded = true;
		}
	}


}
