using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShakeCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.DOShakePosition (1, new Vector3 (1f, 1f, 0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
