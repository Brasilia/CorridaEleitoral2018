using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBHV : MonoBehaviour{

	//Database -----------------
//	public string description;
//	public Sprite image;

	public Card_Data cardData;
	public Text description;
	public Sprite image;

	//Execution ----------------

//	public void LoadData(Card_Data card){
//		//description = card.description;
//		//image = card.image;
//	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Load(Card_Data c){
		description.text = c.description;
		image = c.image;
		cardData = c;
	}

}
