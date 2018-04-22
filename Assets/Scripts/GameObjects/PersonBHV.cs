using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBHV : CardBHV {
	//Database -----------------
	//Name
	public string personName;

	//Trait...

	//Recursos
	public Resources resources;

	//Eventos associados ao personagem
	public List<Event_Data> events;


	//Execution ----------------

	public void LoadData(Person_Data card){
		base.LoadData (card);
		personName = card.personName;
		resources = card.resources;
		events = card.events;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
