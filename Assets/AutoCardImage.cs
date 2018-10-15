using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoCardImage : MonoBehaviour {

	private Image cardImage;
	private GameManager gm;

	void Awake () {
		cardImage = GetComponent<Image> ();
		gm = GameManager.instance;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable () {
		
	}

	public void SetImage(){
		if (cardImage.sprite == null) {
			if ((int)gm.state >= (int)GameManager.STATE.DebateQuestion) {
				cardImage.sprite = gm.candidates [gm.opponentIndex].image;
			}
		}
	}
}
