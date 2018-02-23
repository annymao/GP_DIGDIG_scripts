using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class bgAnimator : MonoBehaviour {
	public enum State{menu,opening,gaming,ending};
	private int state;
	Animator anim;
	Dialog dialog;
	private int count;
	GameObject galaxy;
	GameObject fair;
	GameObject[] fairList;
	storyBGM bgm;
	public bool isStory = false;
	// Use this for initialization
	void Start () {
		anim=this.gameObject.GetComponent<Animator>();
		galaxy = this.transform.GetChild(2).gameObject;
		fair = this.transform.GetChild(3).gameObject;
		bgm=GetComponent<storyBGM>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void changeAnimation(){
		anim.SetTrigger("changeScene");
		bgm.playBGM();
	}
	public void storyDialog(){
		FindObjectOfType<DialogTrigger>().TriggerDialog();
		
	}
	public void dialogInteractive(){
		AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo (0);
			count=FindObjectOfType<DialogManager>().sentences.Count;
		Debug.Log (count);
			setTalk(false);
			switch(count){
				case 17:
					isStory = true;
					/*if(!galaxy.activeSelf){
						galaxy.SetActive(true);
						Instantiate(galaxy,this.transform,true);
						print("active");
					}*/
					galaxy.GetComponent<Animator>().SetTrigger("showGalaxy");
					print("active");
					break;
				case 16:
					fair.transform.Find("water_fair").gameObject.GetComponent<Animator>().SetBool("showFair",true);
					fair.transform.Find("fire_fair").gameObject.GetComponent<Animator>().SetBool("showFair",true);
					fair.transform.Find("stone_fair").gameObject.GetComponent<Animator>().SetBool("showFair",true);
					fair.transform.Find("cure_fair").gameObject.GetComponent<Animator>().SetBool("showFair",true);
					print("active");
					break;
				case 13:
					galaxy.GetComponent<Animator>().SetTrigger("broke");
					bgm.galaxybroke();
					break;
				case 9:
					fair.transform.Find ("fire_fair").gameObject.GetComponent<Animator> ().SetBool ("fairTalk", true);
					break;
				case 8:
					fair.transform.Find("water_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",true);
					break;
				case 7:
					fair.transform.Find("fire_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",true);
					break;
				case 6:
					fair.transform.Find("cure_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",true);
					break;
				case 5:
					fair.transform.Find("fire_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",true);
					break;
				case 4:
					fair.transform.Find("stone_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",true);
					break;
				case 3:
					fair.transform.Find("water_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",true);
					break;
				case 2:
					fair.transform.Find("cure_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",true);
					break;
				case 1:
					fair.transform.Find("stone_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",true);
					break;
				case 0:
					if (isStory)
						Invoke ("LoadLobby", 3f);
					setTalk(true);
					break;

			}
			
	}
	private void setTalk(bool talk){
		fair.transform.Find("water_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",talk);
		fair.transform.Find("fire_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",talk);
		fair.transform.Find("stone_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",talk);
		fair.transform.Find("cure_fair").gameObject.GetComponent<Animator>().SetBool("fairTalk",talk);
	}
	public void LoadLobby(){
		SceneManager.LoadScene (1);
	}
	
}
