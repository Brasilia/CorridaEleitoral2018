using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Card/Event", order = 3)]
public class Event_Data : Card_Data {
	public EventAction_Data actionDecline;
	public EventAction_Data actionAccept;
}
