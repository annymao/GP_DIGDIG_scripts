using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : Photon.MonoBehaviour {
	public GameObject terrain;
	public GameObject terrain2;
	private PolyGen tScript;
	private PolyGen tScript2;
	private PolyGen tScript3;
	private PolyGen tScript4;
	public GameObject target;
	public GameObject fire;
	private LayerMask layerMask = (1 << 0);
	public bool update;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
    
		RaycastHit hit;
		
		float distance=Vector3.Distance(transform.position,target.transform.position);
		if(Input.GetMouseButton(0)){
			ChangeUpdate();
		}
		
	}
	void ChangeUpdate(){
		tScript= GameObject.Find("terrain(Clone)").GetComponent("PolyGen") as PolyGen;
		tScript2= GameObject.Find("terrain2(Clone)").GetComponent("PolyGen") as PolyGen;
		tScript3 = GameObject.Find("terrain3(Clone)").GetComponent("PolyGen") as PolyGen;
		tScript4 = GameObject.Find("terrain4(Clone)").GetComponent("PolyGen") as PolyGen;
		if(target.transform.position.y - tScript.offset_y >0 &&target.transform.position.x - tScript.offset_x > 1 && target.transform.position.x < tScript.offset_x+tScript.blocks.GetLength(0)-1)
			{
				//tScript.update=true;
			Vector2 point= new Vector2(target.transform.position.x-0.5f, target.transform.position.y-tScript.offset_y+0.5f);   //Add this line 
			AddDig(point,0);
			Debug.Log ("test"+PhotonNetwork.player.CustomProperties ["Stone"].GetHashCode ().ToString());
			transform.GetComponent<PhotonView> ().RPC ("UpdateBlock", PhotonTargets.All, point, 0, PhotonNetwork.player.CustomProperties ["Stone"].GetHashCode());
		}else if(target.transform.position.y - tScript2.offset_y > 0 && target.transform.position.y < tScript2.offset_y+tScript2.blocks.GetLength(1)-1 &&　target.transform.position.x < tScript2.offset_x+tScript2.blocks.GetLength(0)-1&&target.transform.position.x - tScript2.offset_x > 1){
				//tScript2.update=true;
			Vector2 point= new Vector2(target.transform.position.x-0.5f, target.transform.position.y-tScript2.offset_y+0.5f);   //Add this line 
			AddDig(point,1);	
			transform.GetComponent<PhotonView> ().RPC("UpdateBlock",PhotonTargets.All,point,1,PhotonNetwork.player.CustomProperties ["Stone"].GetHashCode());
			}else if(target.transform.position.y - tScript3.offset_y > 0 && target.transform.position.x < tScript3.offset_x+tScript3.blocks.GetLength(0)&&target.transform.position.x - tScript3.offset_x > 0 ){
				//tScript3.update=true;
				Vector2 point= new Vector2(target.transform.position.x-(tScript3.offset_x+1.5f), target.transform.position.y-(tScript3.offset_y)+0.5f);   //Add this line 
			AddDig(point,2);	
			transform.GetComponent<PhotonView> ().RPC("UpdateBlock",PhotonTargets.All,point,2,PhotonNetwork.player.CustomProperties ["Stone"].GetHashCode());
			}else if(target.transform.position.y - tScript4.offset_y > 0 && target.transform.position.x < tScript4.offset_x+tScript4.blocks.GetLength(0) && target.transform.position.y < tScript4.offset_y+tScript4.blocks.GetLength(1)-2&&target.transform.position.x - tScript4.offset_x > 0 ){
				//tScript4.update=true;
				Vector2 point= new Vector2(target.transform.position.x-tScript4.offset_x-1.5f, target.transform.position.y-tScript4.offset_y+0.5f);   //Add this line 
			AddDig(point,3);	
			transform.GetComponent<PhotonView> ().RPC("UpdateBlock",PhotonTargets.All,point,3,PhotonNetwork.player.CustomProperties ["Stone"].GetHashCode());
			}
		
	}
	void AddDig(Vector2 point,int terrainNum){
		GameObject digMission = GameObject.Find ("Mission-Dig(Clone)");
		if (digMission != null) {
			if (terrainNum == 0) {
				tScript= GameObject.Find("terrain(Clone)").GetComponent("PolyGen") as PolyGen;
				if (tScript.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] ==1) {
					int currentDig = PhotonNetwork.player.CustomProperties ["Dig"].GetHashCode();
					ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
					p.Add ("Dig", currentDig+1);
					PhotonNetwork.player.SetCustomProperties (p);
					digMission.GetComponent<DigNumber> ().current_dig++;
					digMission.GetComponent<DigNumber> ().SetCurrentDig ();
					PhotonNetwork.Instantiate ("dig", new Vector3(point.x,point.y,transform.position.z), Quaternion.identity, 0);
				}

			} else if (terrainNum == 1) {
				tScript2= GameObject.Find("terrain2(Clone)").GetComponent("PolyGen") as PolyGen;
				if (tScript2.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] ==1) {
					int currentDig = PhotonNetwork.player.CustomProperties ["Dig"].GetHashCode();
					ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
					p.Add ("Dig", currentDig+1);
					PhotonNetwork.player.SetCustomProperties (p);
					digMission.GetComponent<DigNumber> ().current_dig++;
					digMission.GetComponent<DigNumber> ().SetCurrentDig ();	
					PhotonNetwork.Instantiate ("dig",  new Vector3(point.x+tScript2.offset_x+1f,point.y+tScript2.offset_y,transform.position.z), Quaternion.identity, 0);
				}

			} else if (terrainNum == 2) {
				tScript3 = GameObject.Find("terrain3(Clone)").GetComponent("PolyGen") as PolyGen;
				if (tScript3.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] ==1) {
					int currentDig = PhotonNetwork.player.CustomProperties ["Dig"].GetHashCode();
					ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
					p.Add ("Dig", currentDig+1);
					PhotonNetwork.player.SetCustomProperties (p);
					digMission.GetComponent<DigNumber> ().current_dig++;
					digMission.GetComponent<DigNumber> ().SetCurrentDig ();	
					PhotonNetwork.Instantiate ("dig",  new Vector3(point.x+tScript3.offset_x+1f,point.y+tScript3.offset_y,transform.position.z), Quaternion.identity, 0);
				}

			} else if (terrainNum == 3) {
				tScript4 = GameObject.Find("terrain4(Clone)").GetComponent("PolyGen") as PolyGen;
				if (tScript4.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] ==1) {
					int currentDig = PhotonNetwork.player.CustomProperties ["Dig"].GetHashCode();
					ExitGames.Client.Photon.Hashtable p = new ExitGames.Client.Photon.Hashtable ();
					p.Add ("Dig", currentDig+1);
					PhotonNetwork.player.SetCustomProperties (p);
					digMission.GetComponent<DigNumber> ().current_dig++;
					digMission.GetComponent<DigNumber> ().SetCurrentDig ();
					PhotonNetwork.Instantiate ("dig",  new Vector3(point.x+tScript4.offset_x+1f,point.y+tScript4.offset_y,transform.position.z), Quaternion.identity, 0);
				}

			}
		}

	}
	[PunRPC]
	void UpdateBlock(Vector2 point,int terrainNum,int addblock){
		if (terrainNum == 0) {
			tScript= GameObject.Find("terrain(Clone)").GetComponent("PolyGen") as PolyGen;
			if (tScript.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] == 0 && addblock == 1) {
				tScript.update = true;
				tScript.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] = 1;
			}
			else if (tScript.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] != 3) {
				tScript.update = true;
				tScript.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] = 0;
			}  

		} else if (terrainNum == 1) {
			tScript2= GameObject.Find("terrain2(Clone)").GetComponent("PolyGen") as PolyGen;
			if (tScript3.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] == 0 && addblock == 1) {
				tScript3.update = true;
				tScript3.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] = 1;
			}else if (tScript2.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] != 3) {
				tScript2.update=true;
				tScript2.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] = 0;			
			}

		} else if (terrainNum == 2) {
			tScript3 = GameObject.Find("terrain3(Clone)").GetComponent("PolyGen") as PolyGen;
			if (tScript3.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] == 0 && addblock == 1) {
				tScript3.update = true;
				tScript3.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] = 1;
			}else if (tScript3.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] != 3) {
				tScript3.update=true;
				tScript3.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] = 0;			
			}

		} else if (terrainNum == 3) {
			tScript4 = GameObject.Find("terrain4(Clone)").GetComponent("PolyGen") as PolyGen;
			if (tScript4.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] == 0 && addblock == 1) {
				tScript4.update = true;
				tScript4.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] = 1;
			}else if (tScript4.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] != 3) {
				tScript4.update=true;
				tScript4.blocks [Mathf.RoundToInt (point.x), Mathf.RoundToInt (point.y)] = 0;			
			}

		}
	}
	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		
	}
}
