using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour {
    float x, y;
    public float moveSpeed = 0.3f;
    public float flySpeed = 0.05f;
    // Use this for initialization
    private Animator DragonAni;
    private SpriteRenderer sprd;
    bool fade = false;
    public int direction = 0; // Idle(0), Right(1), Left(2)
    public bool upDown = false; // Up(false), Down(true)
    float catchTime = 0.0f;
    public float timeInterval = 0.0f;
    public float upDownTimeInterval = 0.0f;
    public GameObject fire;
    private Rigidbody rigid;
    private float fireInterval = 0.0f;
    private float fireGetTime = 0.0f;
    [SerializeField] private int ClickCount = 0;
    public LayerMask enemyMask;
    enum DragonState { Normal, Chase };
    private DragonState state;
	public int DragonHealth = 100;
	myCanvas cScript;
	//public int count;
	private AudioSource audio;
	public AudioClip rour;
	float count=25f;
	bool shout=false;
	public Vector3 playerPos;
	public GameObject[] players;
	public bool getCount;
	public GameObject nowPlayer;
	//public GameObject player;
	void Start () {
		DragonAni = this.GetComponent<Animator> ();
		sprd = this.GetComponent<SpriteRenderer> ();
		sprd.color = new Color (sprd.color.r, sprd.color.g, sprd.color.b, 0.9f);
		catchTime = Time.time;
		timeInterval = (float) Random.Range (3, 5);
		upDownTimeInterval = (float)Random.Range (5, 10);
		fireGetTime = Time.time;
		fireInterval = 5.0f;
		cScript = GameObject.Find ("Canvas(Clone)").GetComponent<myCanvas> ();
		audio=gameObject.GetComponent<AudioSource>();
		StartCoroutine ("DelayArrow");
		//pos = new Stack<Vector3> ();
	}
	IEnumerator DelayArrow(){

		yield return new WaitUntil (() => (players = GameObject.FindGameObjectsWithTag("Player"))!=null);

		getCount = true;

	}
	// Update is called once per frame
	void Update () {

		if (direction == 0 && Time.time - fireGetTime > fireInterval) {
			GameObject temp = PhotonNetwork.Instantiate (fire.name, this.transform.position + Vector3.down, this.transform.rotation,0);
			temp.GetComponent<Rigidbody> ().AddForce (Vector3.down * 1000.0f);
			fireGetTime = Time.time;
		}
		if(shout){
			count=count+Time.deltaTime;
		}
		if(count>10f){
			count=0;
			audio.PlayOneShot(rour,1f);
		}
	}

	private void FixedUpdate() {
        if (state == DragonState.Normal)
            Fly();
        else
            Chase();
	} 

	private void LateUpdate() {
		Blink ();
	}

	private void Fly() {
		// Control Left and Right
		if (Time.time - catchTime > timeInterval) {
			direction = Random.Range(0, 3);
			switch (direction) {
			case 0:
				DragonAni.SetInteger ("State", 0);
				break;
			case 1:
				DragonAni.SetInteger ("State", 1);
				break;
			case 2:
				DragonAni.SetInteger ("State", 2);
				break;
			}
			timeInterval = (float) Random.Range (3, 5);
			catchTime = Time.time;
		}

		// Control Up and Down
		if (Time.time - catchTime > upDownTimeInterval) {
			upDownTimeInterval = (float)Random.Range (5, 10);
			flySpeed = (float) Random.Range (5, 10) * 0.1f;
			upDown = !upDown;
		}

        Move(direction, upDown);
	}

	private void Blink() {
		if (!fade) {
			sprd.color = new Color (sprd.color.r, sprd.color.g, sprd.color.b, sprd.color.a - 0.01f);
			if (sprd.color.a < 0.5f)
				fade = true;
		} else {
			sprd.color = new Color (sprd.color.r, sprd.color.g, sprd.color.b, sprd.color.a + 0.01f);
			if (sprd.color.a >= 0.9f)
				fade = false;
		}
	}

    private void Move(int direction, bool upDown) {
        switch (direction)
        {
            case 0: // Idle
                break;
            case 1: // Right
                this.transform.position += new Vector3(moveSpeed * 0.5f, 0, 0);
                break;
            case 2: // Left
                this.transform.position += new Vector3(-moveSpeed * 0.5f, 0, 0);
                break;
        }

        if (!upDown)
        {
            this.transform.position += new Vector3(0, flySpeed * 0.5f, 0);
        }
        else
        {
            this.transform.position += new Vector3(0, -flySpeed * 0.5f, 0);
        }
    }
	public void changeChase(){
		if (getCount) {
			if(nowPlayer == null)nowPlayer = GameObject.Find ("Player");
			if (Mathf.Abs (this.transform.position.y - nowPlayer.transform.position.y) > 8 && (Mathf.Abs (this.transform.position.x - nowPlayer.transform.position.x) > 5)) {
				foreach (GameObject pl in players) {
					if (Mathf.Abs (this.transform.position.y - pl.transform.position.y) < 8 && (Mathf.Abs (this.transform.position.x - pl.transform.position.x) < 5)) {
						playerPos = pl.transform.position;
						nowPlayer = pl;
						break;
					}
				}
			} else {
				playerPos = nowPlayer.transform.position;
			}
		} else {
			nowPlayer = GameObject.Find ("Player");
			playerPos = nowPlayer.transform.position;
		}
	}
    private void Chase() {
       // playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		changeChase();
        if (this.transform.position.y - playerPos.y > 4.5f)
        {
            upDown = true;
        }
        else {
            upDown = false;
        }

        if (this.transform.position.x - playerPos.x > 1)
        {
            direction = 2;
        }
        else if (this.transform.position.x - playerPos.x < -1)
        {
            direction = 1;
        }
        else {
            direction = 0;
        }

        switch (direction)
        {
            case 0:
                DragonAni.SetInteger("State", 0);
                break;
            case 1:
                DragonAni.SetInteger("State", 1);
                break;
            case 2:
                DragonAni.SetInteger("State", 2);
                break;
        }

        Move(direction, upDown);

    }

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Player") {
			//playerPos = other.transform.position;
            state = DragonState.Chase;
			shout=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.gameObject.tag == "Player") {
			state = DragonState.Normal;
			shout=false;
				
        }
    }

}
