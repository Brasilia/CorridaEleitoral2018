using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolAction : MonoBehaviour {

	[SerializeField]
	public GameObject card;
	public RectTransform panelCard;
	private GameManager gameManager;

	public bool choice;

	// Use this for initialization
	void Start () {
		//card = panelCard.GetComponent<CardBHV> ();
		gameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Ativa o widget.
	public void SetActiveBoolAction(GameObject card){
		Invoke ("Activate", 0.5f);
		this.card = card;

	}

	private void Activate(){
		gameObject.SetActive (true);
		this.card.transform.SetParent (panelCard.transform);
		panelCard.anchoredPosition = Vector2.zero;
		this.card.transform.localPosition = Vector2.zero;
	}
		
	public void OnDrag(){
		float x, y;
		x = Input.mousePosition.x;
		if (x < 0) {
			//x = 0.0f;
			gameObject.SetActive (false);
			Destroy (this.card);
			//panelCard.transform.localPosition = Vector2.zero;
			choice = false;
			Debug.Log ("NO");
			GameManager.instance.ReturnControl ();
			//gameManager.BoolChosen(false, card);
		}
		if (x > 900f) {
			//x = 900f;
			//card.ActionYes();
			gameObject.SetActive (false);
			Destroy (this.card);
			//panelCard.transform.localPosition = Vector2.zero;
			choice = true;
			Debug.Log ("YES");
			GameManager.instance.ReturnControl ();
			//gameManager.BoolChosen(true, card);
		}
		y = panelCard.transform.position.y;
		panelCard.transform.position = new Vector2(x, y);
		card.transform.position = new Vector3(x, y, -1f);
		//transform.SetAsLastSibling ();
	}
}
