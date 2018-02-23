using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAI : Photon.MonoBehaviour {

    public static bool flip = false; // Left(false), Right(true)
    enum TurtleState { Idle, Wander, Attack, Dead};
    private TurtleState state;
    private Animator TurtleAni;
    public GameObject roll;
	private AudioSource audio;
	public AudioClip rour;
    float TimeInterval = 0.0f;
    float catchTime = 0.0f;

    float AttackInterval = 0.0f;
    float getTime = 0.0f;
    int fury = 0;

    SpriteRenderer sprd;
    public int HP = 20;

	// Use this for initialization
	void Start () {
        state = TurtleState.Wander;
        TurtleAni = this.GetComponent<Animator>();
        sprd = this.GetComponent<SpriteRenderer>();
        TimeInterval = Random.Range(3.0f, 5.0f);
        AttackInterval = 5.0f;
        catchTime = Time.time;
        getTime = Time.time;
        fury = (int) Random.Range(3, 5);
        TurtleAni.SetInteger("HP", HP);
		audio=gameObject.GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        /*
        // (Debug)
        if (Input.GetKeyDown(KeyCode.Z)) {
            Attack();
        }

        if (Input.GetKeyUp(KeyCode.Z)) {
            TurtleAni.SetBool("Attack", false);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            FuryAttack();
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            TurtleAni.SetBool("Fury", false);
        }
        */
        if (Input.GetKeyDown(KeyCode.Space)) {
            HP -= 5;
            TurtleAni.SetInteger("HP", HP);
            if (HP == 0) {
                state = TurtleState.Dead;
                StartCoroutine("DelayDying");
            }
            StartCoroutine("DelayStunning");
        }

    }

    private void FixedUpdate()
    {
        if (state == TurtleState.Wander)
            Wander();
    }

    private void Wander()
    {
        if (Time.time - catchTime > TimeInterval) {
            flip = !flip;
            sprd.flipX = flip; 
            catchTime = Time.time;
        }


        if (Time.time - getTime > AttackInterval) {
            fury--;
            if (fury == 0) {
                fury = (int)Random.Range(3, 5);
                FuryAttack();
            }                
            else 
                Attack();
            getTime = Time.time;
        }

    }

    private void Attack() {
        state = TurtleState.Attack;
        TurtleAni.SetBool("Attack", true);
        print(TurtleAni.GetCurrentAnimatorStateInfo(0).normalizedTime);

        StartCoroutine(DelayAttack(1.5f));
    }

    private void FuryAttack()
    {
        state = TurtleState.Attack;
        TurtleAni.SetBool("Fury", true);

        StartCoroutine(DelayAttack(3.0f));

    }

    IEnumerator  DelayAttack(float force) {
        yield return new WaitForSeconds(0.5f);
		if (PhotonNetwork.isMasterClient) {
			PhotonNetwork.InstantiateSceneObject("Roll", this.transform.position + Vector3.left * ((flip == false)? 1:-1) * force, this.transform.rotation,0,null);
		}

        state = TurtleState.Wander;
        if (force == 3.0f)
            TurtleAni.SetBool("Fury", false);
        else
            TurtleAni.SetBool("Attack", false);
    }

    IEnumerator DelayStunning() {
        state = TurtleState.Idle;
        yield return new WaitForSeconds(1.5f);
        state = TurtleState.Wander;
    }

    IEnumerator DelayDying() {
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
	private void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {
			audio.PlayOneShot(rour,1f);
		}
	}

}
