using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialog {
	[TextArea(3,100)]
	public string[] sentences;
	public string name;
	public int count;
	void Start () {
		count= sentences.Length;
		Debug.Log (count);
	}
}
