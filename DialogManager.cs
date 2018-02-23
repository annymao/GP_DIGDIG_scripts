using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	public Text nameText;
	public Text dialogText;
	public Animator animator;
	
	//public float letterPause = 0.01f;
	public Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}
	
	public void StartDialog(Dialog dialog){
		animator.SetBool("isOpen", true);
		nameText.text = dialog.name;
		//print("Start dialog with " + dialog.name);
		sentences.Clear();
		
		foreach(string sentence in dialog.sentences){
				sentences.Enqueue(sentence);
				
		}
		Debug.Log ("Count:"+sentences.Count);
		DisplayNextSentence();
	}
	public void DisplayNextSentence(){
		if(sentences.Count==0){ 
			FindObjectOfType<bgAnimator>().changeAnimation();
			EndDialog();
			return;
		}
		string sentence = sentences.Dequeue();
		FindObjectOfType<bgAnimator>().dialogInteractive();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
		
	}
	IEnumerator TypeSentence (string sentence){
		dialogText.text = "";
		foreach(char letter in sentence.ToCharArray()){
			dialogText.text += letter;
			yield return null;//new WaitForSeconds(letterPause);
		}
	}
	public void EndDialog(){
		print("End of Conversation");
			animator.SetBool("isOpen",false);
		
	}
	// Update is called once per frame
	void Update () {
		
	}
	public void OnClickStart(){
		
	}
}
