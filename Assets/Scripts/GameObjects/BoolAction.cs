using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolAction : MonoBehaviour {

	[SerializeField]
	private GameObject card;
	public GameObject panelCard;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		//card = panelCard.GetComponent<CardBHV> ();
		gameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetActiveBoolAction(GameObject card){
		this.card = card;
		this.card.transform.SetParent (panelCard.transform);
		this.card.transform.position = new Vector3 (16.2f, 325.6f, 0.0f);
	}

	public void OnDrag(){
		float x, y;
		x = Input.mousePosition.x;
		if (x < 0) {
			//x = 0.0f;

			gameObject.SetActive (false);
			panelCard.transform.position = new Vector2(16.2f, 325.6f);
			gameManager.BoolChoosen(false, card);
		}
		if (x > 900f) {
			//x = 900f;
			//card.ActionYes();
			gameObject.SetActive (false);
			panelCard.transform.position = new Vector2(16.2f, 325.6f);
			gameManager.BoolChoosen(true, card);
		}
		y = panelCard.transform.position.y;
		panelCard.transform.position = new Vector2(x, y);
		card.transform.position = new Vector3(x, y, -1f);
		//transform.SetAsLastSibling ();
	}
}
