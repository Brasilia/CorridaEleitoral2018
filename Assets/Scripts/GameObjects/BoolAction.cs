using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoolAction : MonoBehaviour {

	[SerializeField]
	public GameObject card;
	[Header("Dependencies")]
	public RectTransform panelCard;
	public Text textDecline;
	public Text textAccept;
	public UITextBox panelDecline;
	public UITextBox panelAccept;
	public GameObject arrows;

	//Audio
	public AudioSource audioSource;
	public AudioClip audioAccept;
	public AudioClip audioDecline;

	[Header("Settings")]
	[Range(0f, 900f)]
	public float threshold1 = 40;
	[Range(0f, 900f)]
	public float threshold2 = 400;
	[Range(0f, 0.05f)]
	public float verticalSensitivity = 0.025f;
	[Range(-0.1f, 0.1f)]
	public float rotationSensitivity = 0.07f;
	public bool actOnRelease = true;
	public bool actOnThreshold2 = true;


	private GameManager gameManager;

	private Vector2 clickPoint; //coordenadas do mouse

	private bool isDragging; // previne que múltiplas decisões sejam tomadas em um único drag
	private Vector2 offset = Vector2.zero; // offset atual do mouse
	[SerializeField]
	private Vector2 releaseOffset; // offset do mouse quando botão é liberado






	[Header("Misc")]
	public bool choice;





	// Use this for initialization
	void Start () {
		//card = panelCard.GetComponent<CardBHV> ();
		gameManager = GameManager.instance;
	}

	void OnEnable () {
		//releaseOffset = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDragging) {
			releaseOffset.x = Mathf.Lerp (releaseOffset.x, 0.0f, 0.5f);
			if (Mathf.Abs (releaseOffset.x) < 5.0f) {
				releaseOffset.x = 0.0f;
			}
			SetCardTransform (releaseOffset);
		}
	}

	// Ativa o widget.
	public void SetActiveBoolAction(GameObject card){
		Invoke ("Activate", 0.5f);
		this.card = card;
	}

	private void Activate(){
		gameObject.SetActive (true);
		arrows.SetActive (true);
		isDragging = false;
		panelDecline.Highlight (false);
		panelAccept.Highlight (false);
		textDecline.text = card.GetComponent<EventBHV> ().actionDecline;
		textAccept.text = card.GetComponent<EventBHV> ().actionAccept;
		panelCard.anchoredPosition = Vector2.zero;
		card.GetComponent<UI_StretchableElement> ().FitToParent (panelCard);
	}

	public void OnBeginDrag(){
		clickPoint = Input.mousePosition;
		arrows.SetActive (false);
		isDragging = true;
	}
		
	public void OnDrag(){
		if (!isDragging){
			return;
		}
		offset = (Vector2)Input.mousePosition - clickPoint;
		SetCardTransform (offset);
		if (actOnThreshold2) {
			if (offset.x < -threshold2) { // Decline
				Decline ();
			}
			if (offset.x > threshold2) { // Accept
				Accept ();
			}
		}

		panelDecline.Highlight (false);
		panelAccept.Highlight (false);
		if (offset.x < -threshold1){
			panelDecline.Highlight (true);
		}
		else if (offset.x > threshold1){
			panelAccept.Highlight (true);
		}
	}

	public void OnEndDrag(){
		if (!isDragging){
			return;
		}
		Debug.Log ("OnEndDrag");
		releaseOffset = (Vector2)Input.mousePosition - clickPoint;
		arrows.SetActive (true);
		panelDecline.Highlight (false);
		panelAccept.Highlight (false);
		isDragging = false;
		//panelCard.transform.localPosition = Vector3.zero;
		//panelCard.transform.localRotation = Quaternion.identity;
		if (actOnRelease) {
			if (offset.x < -threshold1) {
				Decline ();
				offset = Vector2.zero;
				releaseOffset = Vector2.zero;
			} else if (offset.x > threshold1) {
				Accept ();
				offset = Vector2.zero;
				releaseOffset = Vector2.zero;
			}
		}
	}

	//Função de deslocamento da carta
	private void SetCardTransform(Vector2 offset){
		float y = -Mathf.Pow (offset.x*verticalSensitivity, 2);
		panelCard.transform.localPosition = new Vector2(offset.x*1.0f, y);
		panelCard.transform.localRotation = Quaternion.AngleAxis (-offset.x * rotationSensitivity, Vector3.forward);
	}

	private void Accept () {
		audioSource.clip = audioAccept;
		audioSource.Play ();
		//x = 900f;
		//card.ActionYes();
		gameObject.SetActive (false);
		Destroy (this.card);
		//panelCard.transform.localPosition = Vector2.zero;
		choice = true;
		Debug.Log ("YES");
		panelCard.transform.localRotation = Quaternion.identity;
		gameManager.ReturnControl ();
		//gameManager.BoolChosen(true, card);
		isDragging = false;
	}

	private void Decline () {
		audioSource.clip = audioDecline;
		audioSource.Play ();
		//x = 0.0f;
		gameObject.SetActive (false);
		Destroy (this.card);
		//panelCard.transform.localPosition = Vector2.zero;
		choice = false;
		Debug.Log ("NO");
		panelCard.transform.localRotation = Quaternion.identity;
		gameManager.ReturnControl ();
		//gameManager.BoolChosen(false, card);
		isDragging = false;
	}
}
