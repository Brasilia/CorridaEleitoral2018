using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoolAction : MonoBehaviour {

	[SerializeField]
	public GameObject card;
	public RectTransform panelCard;
	public Text textDecline;
	public Text textAccept;
	public GameObject panelDecline;
	public GameObject panelAccept;

	private GameManager gameManager;

	private Vector2 clickPoint; //coordenadas do mouse


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
		panelDecline.gameObject.SetActive (false);
		panelAccept.gameObject.SetActive (false);
		textDecline.text = card.GetComponent<EventBHV> ().actionDecline;
		textAccept.text = card.GetComponent<EventBHV> ().actionAccept;



		this.card.transform.SetParent (panelCard.transform);

		panelCard.anchoredPosition = Vector2.zero;

		card.transform.localPosition = Vector2.zero;
		card.transform.localScale = Vector2.one;
		card.GetComponent<RectTransform> ().sizeDelta = Vector2.zero;
	}

	public void OnBeginDrag(){
		clickPoint = Input.mousePosition;
	}
		
	public void OnDrag(){
		Vector2 offset = (Vector2)Input.mousePosition - clickPoint;
		SetCardTransform (offset);
		if (offset.x < -450) {
			//x = 0.0f;
			gameObject.SetActive (false);
			Destroy (this.card);
			//panelCard.transform.localPosition = Vector2.zero;
			choice = false;
			Debug.Log ("NO");
			panelCard.transform.localRotation = Quaternion.identity;
			GameManager.instance.ReturnControl ();
			//gameManager.BoolChosen(false, card);
		}
		if (offset.x > 450) {
			//x = 900f;
			//card.ActionYes();
			gameObject.SetActive (false);
			Destroy (this.card);
			//panelCard.transform.localPosition = Vector2.zero;
			choice = true;
			Debug.Log ("YES");
			panelCard.transform.localRotation = Quaternion.identity;
			GameManager.instance.ReturnControl ();
			//gameManager.BoolChosen(true, card);
		}

		panelDecline.gameObject.SetActive (false);
		panelAccept.gameObject.SetActive (false);
		if (offset.x < 10){
			panelDecline.gameObject.SetActive (true);
		}
		else if (offset.x > 10){
			panelAccept.gameObject.SetActive (true);
		}
	}

	public void OnEndDrag(){
		panelDecline.gameObject.SetActive (false);
		panelAccept.gameObject.SetActive (false);
		panelCard.transform.localPosition = Vector3.zero;
		panelCard.transform.localRotation = Quaternion.identity;
	}

	//Função de deslocamento da carta
	private void SetCardTransform(Vector2 offset){
		float y = -Mathf.Pow (offset.x/20, 2)/5;
		panelCard.transform.localPosition = new Vector2(offset.x, y);
		panelCard.transform.localRotation = Quaternion.AngleAxis (-offset.x / 20, Vector3.forward);
	}
}
