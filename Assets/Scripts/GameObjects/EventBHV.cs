using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventBHV : CardBHV {
	//Database -----------------
//	public EventAction_Data actionDecline;
//	public EventAction_Data actionAccept;
	public string actionDecline;
	public string actionAccept;
	//Execution ----------------


//	public void LoadData(Event_Data card){
//		base.LoadData (card);
////		actionDecline = card.actionDecline;
////		actionAccept = card.actionAccept;
//	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Load(Event_Data e){
		base.Load (e);
		actionDecline = e.actionDecline.description; //FIXME descomentar
		actionAccept = e.actionAccept.description; //FIXME descomentar
		AutoCardImage autoImage = GetComponentInChildren<AutoCardImage>();
		if (autoImage != null) {
			autoImage.SetImage ();
		}
	}
}
