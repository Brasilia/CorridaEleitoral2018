using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventAction_Data {
	public string description;
	//public Person_Data target;
	public Resources resources;
	public Alignment alignment;
	public Event_Data nextEvent;
	//SpecialConsequence...
	public enum SpecialConsequenceNumber {
		ADD_CORRUPTION, //talvez não seja necessário isto para a corrupção: deve bastar adicionar uma carta genérica de "corruption" a todos os adversários, na quantidade da corrupção total menos a corrupção conhecida
        COUNT
	}
	public SpecialConsequenceNumber[] specialConsequences;

}
