using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandidateBHV : PersonBHV {

	//Database -----------------
	//Eixos
	public Alignment alignment;
	public List<Staff_Data> avaiableStaff;

	//Execution ----------------
	public List<CandidateBHV> candidates; //Base de conhecimento sobre os outros candidatos
	public float voteIntentions;


	public void LoadData(Candidate_Data card){
		base.LoadData (card);
		alignment = card.alignment;
		avaiableStaff = card.avaiableStaff;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
