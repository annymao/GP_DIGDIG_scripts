using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngleAI : MonoBehaviour {

	private float moveSpeed = 0.1f;
	private float flySpeed = 0.05f;
	private float changeDirectionTimeInterval = 0.0f;
	private float changeUpDownInterval = 0.0f;
	private float wanderTimeInterval = 0.0f;
	private float attackTimeInterval = 0.0f;
	float getTime = 0.0f;
	float catchTime = 0.0f;
	float grabTime = 0.0f;
	float pickTime = 0.0f;
	private bool direction = false; // Right(false), Left(true)
	private bool upDown = false; // Up(false), Down(true)
	private Animator AngleAni;
	private SpriteRenderer sprd;
	private Rigidbody rigid;

	enum AngleState {
		Idle,
		Wander,
		Chase,
		Attack,
		Hit,
		Dead
	};

	AngleState state = AngleState.Idle;

	int pressCount = 0;

	public GameObject circle;
	public GameObject ball;

	// Use this for initialization
	void Start () {
		AngleAni = this.GetComponent<Animator>();
		sprd = this.GetComponent<SpriteRenderer>();
		rigid = this.GetComponent<Rigidbody> ();
		state = AngleState.Idle;
		changeDirectionTimeInterval = Random.Range(5.0f, 10.0f);
		changeUpDownInterval = Random.Range(4.0f, 8.0f);
		wanderTimeInterval = Random.Range(3.0f, 5.0f);
		attackTimeInterval = Random.Range (4.0f, 6.0f);
		catchTime = Time.time;
		getTime = catchTime;
		grabTime = getTime;
		pickTime = grabTime;
	}

	// Update is called once per frame
	void Update () {
		// (Debug)
		/*
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject temp = Instantiate(circle, this.transform.position, circle.transform.rotation);
            state = AngleState.Attack;
            AngleAni.SetInteger("State", 3);
            StartCoroutine(DelayAttack(0.01f));
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            state = AngleState.Hit;
            AngleAni.SetInteger("State", 4);
            StartCoroutine(DelayAttack(0.01f));
            GameObject b =  Instantiate(ball, this.transform.position + new Vector3(1f, 1.2f, 0.0f), ball.transform.rotation);
            b.GetComponent<Rigidbody2D>().AddForce(new Vector2(800, 0));    
        }

       

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            sprd.flipX = true;
            moveSpeed = 0.05f;
            pressCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            sprd.flipX = false;
            moveSpeed = 0.05f;
            pressCount = 0;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            pressCount++;
            if (pressCount > 30) {
                moveSpeed += 0.05f;
                pressCount = 0;
            }            
        }

        if (state == AngleState.Idle || state == AngleState.Wander) {
            if (x != 0)
            {
                state = AngleState.Wander;
                AngleAni.SetInteger("State", 1);
            }
            else
            {
                state = AngleState.Idle;
                AngleAni.SetInteger("State", 0);
            }
        }


        this.transform.position += new Vector3(x * moveSpeed, y * moveSpeed, 0.0f);
        */
		if (Input.GetKeyDown(KeyCode.Return)) {
			Hit ();   
		}



	}

	private void FixedUpdate()
	{
		if (state != AngleState.Attack && state != AngleState.Dead) {
			if (state == AngleState.Chase) {
				Chase();
			}
			else
			{
				if (Time.time - catchTime > changeDirectionTimeInterval) {
					direction = !direction;
					sprd.flipX = !sprd.flipX;
					changeDirectionTimeInterval = Random.Range(5.0f, 10.0f);
					catchTime = Time.time;
				}

				if (Time.time - grabTime > changeUpDownInterval) {
					upDown = !upDown;
					changeUpDownInterval = changeUpDownInterval = Random.Range(4.0f, 8.0f);
					grabTime = Time.time;
				}
				Move(true);
				Fly(true);
			}
		}
	}

	private void Move(bool wander)
	{
		if (wander) {
			state = AngleState.Wander;
			AngleAni.SetInteger("State", 1);

			if (direction)
				this.transform.position += new Vector3(0.5f * -moveSpeed, 0, 0.0f);
			else
				this.transform.position += new Vector3(0.5f * moveSpeed, 0, 0.0f);      
		}
		else {
			state = AngleState.Chase;
			AngleAni.SetInteger("State", 1);
		}



	}

	void Fly(bool wander) {
		if (wander) {
			state = AngleState.Wander;
		}
		else {
			state = AngleState.Chase;
		}

		if (upDown) {
			this.transform.position += new Vector3(0.0f, 0.5f * -flySpeed, 0.0f);
		}
		else {
			this.transform.position += new Vector3(0.0f, 0.5f * flySpeed, 0.0f);
		}
	}

	void Chase() {
		GameObject player = GameObject.Find("Player");

		if (Vector2.Distance (new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y)) > 5)
		{
			if (this.transform.position.x - player.transform.position.x > 0)
			{
				direction = true;
				sprd.flipX = true;
			}
			else
			{
				direction = false;
				sprd.flipX = false;
			}

			if (this.transform.position.y - player.transform.position.y > 0)
			{
				upDown = true;
			}
			else {
				upDown = false;
			}

			if (state != AngleState.Attack && state != AngleState.Hit) {
				Move(false);
				this.transform.position = Vector3.MoveTowards (this.transform.position, player.transform.position, 1.0f * Time.deltaTime);
			}

			/*
			if (state != AngleState.Attack) {
				Move(false);
				Fly(false);
			}
			*/
		}
		else {

			if (Time.time - pickTime > attackTimeInterval) {
				//rigid.velocity = (player.transform.position - this.transform.position) * 1.0f;
				int val = Random.Range(1,3);
				if (val % 2 == 0)
					Attack ();
				else
					Hit ();
				attackTimeInterval = Random.Range (4.0f, 6.0f);
				pickTime = Time.time;
			}


			if (this.transform.position.x - player.transform.position.x > 0)
			{
				sprd.flipX = true;
			}
			else
			{
				sprd.flipX = false;
			}


			if (state != AngleState.Attack && state != AngleState.Hit)
				AngleAni.SetInteger("State", 0);
		}
	}

	void Attack() {
		GameObject temp = PhotonNetwork.Instantiate(circle.name, this.transform.position, circle.transform.rotation,0);
		state = AngleState.Attack;
		AngleAni.SetInteger("State", 3);
		StartCoroutine(DelayAttack(0.01f));
	}

	void Hit() {
		state = AngleState.Hit;
		AngleAni.SetInteger ("State", 4);
		StartCoroutine(DelayAttack(0.5f));

	}

	IEnumerator DelayAttack(float t) {
		yield return new WaitForSeconds(t);
		if (t == 0.5f) {
			GameObject player = GameObject.Find ("Player");
			GameObject b =  PhotonNetwork.Instantiate(ball.name, this.transform.position + new Vector3(1f, 1.2f, 0.0f), ball.transform.rotation,0);
			Vector2 emissionPoint = new Vector2 (this.transform.position.x + 1.0f, this.transform.position.y + 1.2f);
			b.GetComponent<Rigidbody>().AddForce(70.0f * new Vector3(player.transform.position.x - emissionPoint.x, player.transform.position.y - emissionPoint.y, 0.0f));  
		}
		state = AngleState.Chase;
	}

	private void OnTriggerEnter(Collider other)
	{
		// print(other.gameObject.name);
		if (other.name == "Player") {
			state = AngleState.Chase;
		}
	}
}
