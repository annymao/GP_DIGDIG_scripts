using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

	// Use this for initialization
	public Dialog dialog;


	public void TriggerDialog(){
		FindObjectOfType<DialogManager>().StartDialog(dialog);
	}
	
}
