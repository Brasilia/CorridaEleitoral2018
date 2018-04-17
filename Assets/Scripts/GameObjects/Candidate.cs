using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candidate : Person {

	//Database -----------------
	//Eixos
	public Alignment alignment;
	public List<Staff_Data> avaiableStaff;

	//Execution ----------------
	public List<Candidate> candidates; //Base de conhecimento sobre os outros candidatos
	public float voteIntentions;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
