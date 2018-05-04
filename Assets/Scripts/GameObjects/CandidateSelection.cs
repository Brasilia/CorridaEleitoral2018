using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandidateSelection : MonoBehaviour {

	public List<Image> imgCandidate;
	private GameManager gameManager;
	private Player player;
	private int candidateSelected;

	// Use this for initialization
	void Start () {
		gameManager = GameManager.instance;
		player = GameObject.Find ("Player").GetComponent<Player> ();
		candidateSelected = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateSelectScreen(List<Candidate_Data> availableCandidates){
		gameObject.SetActive (true);
		int i;
		for (i = 0; i < availableCandidates.Count; i++) 
			imgCandidate[i].sprite = availableCandidates[i].image;
	}

	public void OnClickCandidate(int candidateSelected){
		this.candidateSelected = candidateSelected;
		//print ("Você selecionou o candidato " + gameManager.otherCandidates [candidateSelected].name);
		if (gameManager.State == GameManager.STATE.ChooseCandidate)
			gameManager.CandidateChoosen (candidateSelected);
	}
		
}
