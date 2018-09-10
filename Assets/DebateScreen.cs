using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebateScreen : MonoBehaviour {

	public Image img1;
	public Image img2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){
		int opponent = GameManager.instance.opponentIndex;
		Debug.Log ("Opponent: " + opponent);
		if (GameManager.instance.firstPlayer == 0) {
			img1.sprite = GameManager.instance.candidates [0].image;
			img2.sprite = GameManager.instance.candidates [opponent].image;
		} else {
			img1.sprite = GameManager.instance.candidates [opponent].image;
			img2.sprite = GameManager.instance.candidates [0].image;
		}
	}
}
