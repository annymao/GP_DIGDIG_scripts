using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fair : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	/*public void fairTalk(bool talk){
		print("get message and talk");
		anim.SetBool("talk",talk);
	}*/
	// Update is called once per frame
	void Lateupdate () {
		if(this.gameObject.name=="fire_fairy"){
				print("change Sprite");
				string name="characters/fairy_f03";
				Sprite mySprite = Resources.Load(name, typeof(Sprite)) as Sprite;
				gameObject.GetComponent<SpriteRenderer>().sprite =mySprite;
				
			}
			else if(this.gameObject.name=="water_fairy"){
				string name="characters/fairy_f01";
				Sprite mySprite = Resources.Load(name, typeof(Sprite)) as Sprite;
				gameObject.GetComponent<SpriteRenderer>().sprite =mySprite;
			}
			else if(this.gameObject.name=="stone_fairy"){
				string name="characters/fairy_m03";
				Sprite mySprite = Resources.Load(name, typeof(Sprite)) as Sprite;
				gameObject.GetComponent<SpriteRenderer>().sprite =mySprite;
			}
			else if(this.gameObject.name=="cure_fairy"){
				string name="characters/fairy_m02";
				Sprite mySprite = Resources.Load(name, typeof(Sprite)) as Sprite;
				gameObject.GetComponent<SpriteRenderer>().sprite =mySprite;
			}
	}
}
