using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolAction : MonoBehaviour {

	public Card card;
	public GameObject panelCard;

	// Use this for initialization
	void Start () {
		card = panelCard.GetComponent<Card> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrag(){
		float x, y;
		x = Input.mousePosition.x;
		if (x < 0) {
			//x = 0.0f;
			card.ActionNo ();
		}
		if (x > 900f) {
			//x = 900f;
			card.ActionYes();
		}
		y = panelCard.transform.position.y;
		panelCard.transform.position = new Vector2(x, y);
		//transform.SetAsLastSibling ();
	}
}
