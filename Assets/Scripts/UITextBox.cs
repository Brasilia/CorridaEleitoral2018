using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextBox : MonoBehaviour {

	[Range(0, 1)]
	public float minAlpha;
	[Range(0, 1)]
	public float maxAlpha;

	[Range(0, 1)]
	public float textMinAlpha;
	[Range(0, 1)]
	public float textMaxAlpha;

	private Image image;
	private Text text = null;


	void Awake () {
		image = GetComponent<Image> ();
		text = GetComponentInChildren<Text> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Highlight (bool b) {
		float alpha;
		float textAlpha;
		if (b) {
			alpha = maxAlpha;
			textAlpha = textMaxAlpha;
		} else {
			alpha = minAlpha;
			textAlpha = textMinAlpha;
		}
		var tempColor = image.color;
		tempColor.a = alpha;
		image.color = tempColor;
		if (text != null){
			tempColor = text.color;
			tempColor.a = textAlpha;
			text.color = tempColor;
		}
			
	}
}
