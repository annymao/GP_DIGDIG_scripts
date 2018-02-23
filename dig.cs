using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dig : MonoBehaviour {
	float existTime = 3.0f;
	float getTime = 0.0f;

	private void Start()
	{
		existTime = 3.0f;
		getTime = Time.time;
	}

	private void Update()
	{
		if (Time.time - getTime > existTime) {
			if(gameObject.GetComponent<PhotonView>().isMine)
				PhotonNetwork.Destroy(this.gameObject);
		}
	}
}
