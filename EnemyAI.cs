using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum enemyState {
    wander,
    chase,
    jump
} 

public class EnemyAI : MonoBehaviour {
    // player var
    public string playerName = "Player"; // Change player name if needed
    GameObject player;

    // enemy var
    SpriteRenderer sr;
    enemyState state = enemyState.wander;
    Rigidbody rigid;
    RaycastHit lefthit;
    RaycastHit righthit;
    public bool isGround;

    // wander var
    public float wanderTime;
    float catchTime;
    public float speed;

    // jump var
    public int lefthitCount = 0;
    public int righthitCount = 0;
    public bool jumped;

    // Use this for initialization
    void Start () {
        player = GameObject.Find(playerName); 
        sr = this.GetComponent<SpriteRenderer>();
        if (sr == null) print("You need SpriteRenderer!");
        rigid = this.GetComponent<Rigidbody>();
        if (rigid == null) print("You need RigidBody!");
        wanderTime = 3.0f;
        catchTime = Time.time;
        jumped = false;

        // public sets initialize
        playerName = "Player";
        isGround = false;
        speed = 0.01f;

    }
	
	// Update is called once per frame
	void Update () {

        // Debug.Log(rigid.velocity.y);
        if (rigid.velocity.y == 0.0f)
            isGround = true;
        else 
            isGround = false;

        if (state == enemyState.chase && isGround) {
            float direction  = player.transform.position.x - this.transform.position.x;
        }
        else if (state == enemyState.wander && isGround) {
            if (Time.time - catchTime > wanderTime) {
                if (sr.flipX) {
                    sr.flipX = false;
                }
                else {
                    sr.flipX = true;
                }
                lefthitCount = 0;
                righthitCount = 0; 
                catchTime = Time.time;
                wanderTime = Random.Range(5.0f, 15.0f);
            }
        }

        Debug.DrawLine(this.transform.position + Vector3.left * sr.bounds.size.x * 0.5f, this.transform.position + Vector3.left * sr.bounds.size.x * 0.6f, Color.red);
        if (Physics.Linecast(this.transform.position + Vector3.left * sr.bounds.size.x * 0.5f, this.transform.position + Vector3.left * sr.bounds.size.x * 0.6f, out lefthit)) {
            // Debug.Log("HIT " + lefthit.collider.name + " " + hitCount);
            if (lefthit.collider.name == "terrain" && jumped == false && sr.flipX) {
                lefthitCount++;
                if (lefthitCount > 30){
                    state = enemyState.jump;
                } 
            }
        }

        Debug.DrawLine(this.transform.position + Vector3.right * sr.bounds.size.x * 0.5f, this.transform.position + Vector3.right * sr.bounds.size.x * 0.6f, Color.red);
        if (Physics.Linecast(this.transform.position + Vector3.right * sr.bounds.size.x * 0.5f, this.transform.position + Vector3.right * sr.bounds.size.x * 0.6f, out righthit)) {
            if (righthit.collider.name == "terrain" && jumped == false && sr.flipX == false) {
                righthitCount++;
                if (righthitCount > 30) {
                    state = enemyState.jump;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("enemy speed: " + rigid.velocity);
        }
	}

    void FixedUpdate() {
        if ((state == enemyState.chase || state == enemyState.wander) && isGround) { // enemy chase or wander
            if (sr.flipX) rigid.velocity = Vector3.left*2.0f; 
            else rigid.velocity = Vector3.right*2.0f; 
        } 
        else if (state == enemyState.jump && isGround && jumped == false) { // enemy jump
            jumped = true;
            StartCoroutine("jumpDelay");
            state = enemyState.wander;
        } 
    }

    IEnumerator jumpDelay() {
        rigid.velocity += Vector3.up * 8.0f;
        yield return new WaitForSeconds(0.8f);
        
        if (sr.flipX)
                rigid.velocity += Vector3.left * 2.0f;
            else 
                rigid.velocity += Vector3.right * 2.0f;
        
        jumped = false;
        lefthitCount = 0;
        righthitCount = 0;
    }

    void OnCollisionEnter(Collision col) {
        if (col.collider.name == "terrain")
            isGround = true;
    }
    
    void OnCollisionExit(Collision other) {
        if (other.collider.name == "terrain")
            isGround = false;
    }

    void OnTriggerStay(Collider other) {
        if (other.name == playerName) {
            state = enemyState.chase;
            float direction  = player.transform.position.x - this.transform.position.x;
            if (direction < 0) {
                sr.flipX = true;
            }
            else {
                sr.flipX = false;
            }
        }
    }
    
    void OnTriggerExit(Collider other) {
        if (other.name == playerName) {
            state = enemyState.wander;
            float direction  = player.transform.position.x - this.transform.position.x;
            if (direction < 0) {
                sr.flipX = true;
            }
            else {
                sr.flipX = false;
            }
        }

    }
}
