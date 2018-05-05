using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabEvent : MonoBehaviour {

	public GameManager gm;


	// Use this for initialization
	void Start () {
		//this.gameObject.SetActive (false);
		LoadText (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadText(int eventID){
		this.gameObject.transform.GetChild (1).GetComponent<UnityEngine.UI.Text>().text = gm.eventsData[eventID].description;
	}
		
}
