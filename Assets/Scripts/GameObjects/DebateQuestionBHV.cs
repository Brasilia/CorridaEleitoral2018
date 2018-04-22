using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebateQuestionBHV : EventBHV {
	//Database -----------------
	public Event_Data nextEvent2nd;
	public Event_Data nextEvent3rd;
	//Execution ----------------

	public void LoadData(DebateQuestion_Data card){
		base.LoadData (card);
		nextEvent2nd = card.nextEvent2nd;
		nextEvent3rd = card.nextEvent3rd;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
