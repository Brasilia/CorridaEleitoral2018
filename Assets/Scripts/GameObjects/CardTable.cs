using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTable : MonoBehaviour {

	public List<Button> btnCandidate;
	private GameManager gameManager;
	public int candidateSelected;
	private List<GameObject> candidates;

	// Use this for initialization
	void Start () {
		gameManager = GameManager.instance;
		//player = GameObject.Find ("Player").GetComponent<Player> ();
		candidateSelected = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Ativa o widget.
	public void SetActiveSelectScreen(List<GameObject> candidates){
		this.candidates = candidates;
		Debug.Log (candidates.Count);
		gameObject.SetActive (true);
		int i;
		for (i = 0; i < candidates.Count; i++) {
			Debug.Log (i + " " + candidates [i]);
			candidates [i].GetComponent<UI_StretchableElement> ().FitToParent (btnCandidate [i].GetComponent<RectTransform> ());
		}
	}

	// Identifica o candidato escolhido.
	public void OnClickCandidate(int candidateSelected){
		this.candidateSelected = candidateSelected;
		foreach (GameObject g in this.candidates)
			Destroy (g);
		//print ("Você selecionou o candidato " + gameManager.otherCandidates [candidateSelected].name);
		//if (gameManager.State == GameManager.STATE.ChooseCandidate)
		//gameManager.CandidateChosen (candidateSelected);
		gameManager.ReturnControl();
	}
		
}
