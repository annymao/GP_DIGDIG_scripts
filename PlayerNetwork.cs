using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour {
	[SerializeField] private GameObject playerCamera;
	[SerializeField] private MonoBehaviour[] playerControlScripts;

	private PhotonView photonView;

	void Start()
	{
		photonView = this.GetComponent<PhotonView>(); 
		Initialize();
	}

	private void Initialize() {
		if (photonView.isMine) {
			// Do stuff here
		}
		else { // Handle functionality for non-local character

			// Disable its camera 
			playerCamera.SetActive(false);

			// Disable its control scripts 
			foreach (MonoBehaviour m in playerControlScripts) {
				m.enabled = false;
			}
		}
	}

}
