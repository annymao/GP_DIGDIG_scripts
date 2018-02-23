using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEx : Photon.MonoBehaviour {
	public GameObject terrain;
	private PolyGen tScript;
	public int size=4;
	public bool circular=false;
	// Use this for initialization
	void Start () {
		if(transform.position.x<154f && transform.position.y > 0.0f)
		 	terrain = GameObject.Find ("terrain(Clone)");
		else if(transform.position.x >= 154f && transform.position.y >0.0f)
			terrain = GameObject.Find ("terrain3(Clone)");
		else if(transform.position.x < 154f && transform.position.y <= 0.0f)
			terrain = GameObject.Find ("terrain2(Clone)");
		else
			terrain = GameObject.Find ("terrain4(Clone)");
		 tScript=terrain.GetComponent("PolyGen") as PolyGen;  
		Invoke ("DisableSelf", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		bool collision=false;
		for(int x=0;x<size;x++){
			for(int y=0;y<size;y++){
				if(circular){
					if(Vector2.Distance(new Vector2(x-(size/2),y-(size/2)),Vector2.zero)<=(size/3)){
						if(RemoveBlock(x-(size/2),y-(size/2))){
						collision=true;
						}
					}
				} 
				else {
					if(RemoveBlock(x-(size/2),y-(size/2))){
						collision=true;
					}
				}
				
			}
		}
		if( collision){
			tScript.update=true;
		
		}
    
	}
	bool RemoveBlock(float offsetX, float offsetY){
		int x =Mathf.RoundToInt(transform.position.x+offsetX);
		int y=Mathf.RoundToInt(transform.position.y+1f+offsetY);
	
		if(x<tScript.blocks.GetLength(0)+Mathf.RoundToInt(tScript.offset_x) && y<tScript.blocks.GetLength(1)+Mathf.RoundToInt(tScript.offset_y) && x>=Mathf.RoundToInt(tScript.offset_x) && y>=Mathf.RoundToInt(tScript.offset_y)){
		
			if(tScript.blocks[x-Mathf.RoundToInt(tScript.offset_x),y-Mathf.RoundToInt(tScript.offset_y)]!=0){
				tScript.blocks[x-Mathf.RoundToInt(tScript.offset_x),y-Mathf.RoundToInt(tScript.offset_y)]=0;
				return true;
			}
		}
		return false;
	}
	void DisableSelf(){
		gameObject.GetComponent<ColliderEx> ().enabled = false;
	}

}
