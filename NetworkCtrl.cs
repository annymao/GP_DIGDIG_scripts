using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCtrl : Photon.MonoBehaviour {

	Vector3 realPosition ;
	Quaternion realRotation ;
	void Awake(){
		PhotonNetwork.sendRate = 40;
		PhotonNetwork.sendRateOnSerialize = 15;
	}

	// Use this for initialization
	void Start () {
		realPosition = transform.position;
		realRotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
		if(photonView.isMine){
			//Do nothing
			print("isMine");
		}else{
			print("notMine");				
			transform.position = Vector3.Lerp(transform.position,realPosition,3f) ;
			transform.rotation = Quaternion.Lerp(transform.rotation,realRotation,3f);

		}
	}

	public void OnPhotonSerializeView(PhotonStream stream , PhotonMessageInfo info){

		if (stream.isWriting){//自己傳給別人
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}else{//別人傳到自己
			realPosition= (Vector3)stream.ReceiveNext();
			realRotation= (Quaternion)stream.ReceiveNext();
		}
	}
}
