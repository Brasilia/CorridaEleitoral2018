using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBHV : MonoBehaviour {

	//Database -----------------
	public string description;
	public SpriteRenderer image;

	//Execution ----------------

	public void LoadData(Card_Data card){
		description = card.description;
		image = card.image;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ActionNo(){
		print ("Escolheu opção No");
	}

	public void ActionYes(){
		print ("Escolheu opção Yes");
	}
}
