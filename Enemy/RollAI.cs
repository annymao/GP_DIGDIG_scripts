using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAI :Photon.MonoBehaviour {
    float existTime = 5.0f;
    float getTime = 0.0f;

    private void Start()
    {
        getTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - getTime > existTime) {
			if(gameObject.GetComponent<PhotonView>().isMine)
           	 	PhotonNetwork.Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!TurtleAI.flip)
            this.transform.position += Vector3.left * 0.5f;
        else
            this.transform.position += Vector3.right * 0.5f;
    }
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") {
			PhotonNetwork.Destroy(this.gameObject);
		}

	}
}
