using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonBHV : CardBHV {
	//Database -----------------
	//Name
//	public string personName;
//
//	//Trait...
//
//	//Recursos
//	public Resources resources;

	//Eventos associados ao personagem
	public List<Event_Data> events;


	public Text personName;
	public Text[] resources = new Text[4];


	//Execution ----------------

	public void LoadData(Person_Data card){
//		base.LoadData (card);
//		personName.text = card.personName;
//		resources = card.resources;
//		events = card.events;
	}

	public void Load (Person_Data card){
		base.Load (card);
		personName.text = card.personName;
		resources [0].text = card.resources.cash.ToString();
		resources [1].text = card.resources.corruption.ToString();
		resources [2].text = card.resources.credibility.ToString();
		resources [3].text = card.resources.visibility.ToString();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
