using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingControl : MonoBehaviour {

	public static LoadingControl Instance;

	Image loadingimage;
	Text loadingText;
	private float percent;
	[SerializeField] private bool connected = false;
	[SerializeField] private float loadingBoundary = 0.0f;

	public bool LoadingComplete = false;

	void Awake() {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		loadingimage = this.GetComponent<Image> ();
		loadingText = this.GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!connected) {
			Debug.Log (PhotonNetwork.connectionState);
			switch (PhotonNetwork.connectionState) {
				case ConnectionState.Connecting:
					loadingBoundary = 0.3f;
					break;
				case ConnectionState.InitializingApplication:
					loadingBoundary = 0.7f;
					break;
				case ConnectionState.Connected:
					Debug.LogWarning (loadingimage.fillAmount);
					loadingBoundary = 1.0f;
					break;
			}

			if (PhotonNetwork.connectionState == ConnectionState.Connected)
				connected = true;
		}

		percent = loadingimage.fillAmount;
		loadingText.text = (percent * 100).ToString("0.0") + " %";
		if (percent < 1) {
			this.transform.SetAsLastSibling ();
			if (percent < loadingBoundary)
				loadingimage.fillAmount += 0.1f;

		} else {
			if (PhotonNetwork.inRoom) {
				GameObject.Find ("CurrentRoom").transform.SetAsLastSibling ();
			}
			LoadingComplete = true;
			Destroy (this.gameObject);
		}
	}
}
