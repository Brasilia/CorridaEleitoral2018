using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person_Data : Card_Data {
	//Name
	public string personName;

	//Trait...

	//Recursos
	public Resources resources;

	// Alignment
	public Alignment alignment;

	//Eventos associados ao personagem
	public List<Event_Data> events;
}
