using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandidateBHV : PersonBHV {

	//Database -----------------
	//Eixos
//	public Alignment alignment;
//	public List<Staff_Data> avaiableStaff;
//
//	//Execution ----------------
//	public List<CandidateBHV> candidates; //Base de conhecimento sobre os outros candidatos
//	public float voteIntentions;
//	public List<Alignment> debateBoosts;

	public int economical;
	public int civil;
	public int societal;



//	public void LoadData(Candidate_Data card){
////		base.LoadData (card);
////		alignment = card.alignment;
////		avaiableStaff = card.avaiableStaff;
//	}


	public void Load(Candidate_Data card){
		base.Load (card);
		economical = card.alignment.economic.value;
		civil = card.alignment.civil.value;
		societal = card.alignment.societal.value;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
