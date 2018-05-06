using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Candidate{

	public string description;
	public Sprite image;
	//Name
	public string personName;

	//Trait...

	//Recursos
	public Resources resources;

	//Eventos associados ao personagem
	public List<Event_Data> events;

	//Eixos
	public Alignment alignment;

	public List<Staff_Data> avaiableStaff;
	public List<Staff_Data> hiredStaff = new List<Staff_Data>();

	//Execution ----------------
	public List<CandidateBHV> candidates; //Base de conhecimento sobre os outros candidatos
	public float voteIntentions;
	public List<Alignment> debateBoosts;

	public Candidate(Candidate_Data card){
		description = card.description;
		image = card.image;
		personName = card.personName;
		resources = card.resources;
		events = card.events;
		alignment = card.alignment;
		avaiableStaff = card.avaiableStaff;
	}
}
