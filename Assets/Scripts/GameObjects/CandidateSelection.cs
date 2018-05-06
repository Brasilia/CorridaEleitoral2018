﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandidateSelection : MonoBehaviour {

	public List<Button> btnCandidate;
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

	// Ativa o widget.
	public void SetActiveSelectScreen(List<GameObject> candidates){
		gameObject.SetActive (true);
		int i;
		for (i = 0; i < candidates.Count; i++) {
			candidates [i].transform.SetParent (btnCandidate [i].transform);
			candidates [i].transform.position = btnCandidate [i].transform.position;
		}
	}

	// Identifica o candidato escolhido.
	public void OnClickCandidate(int candidateSelected){
		this.candidateSelected = candidateSelected;
		//print ("Você selecionou o candidato " + gameManager.otherCandidates [candidateSelected].name);
		//if (gameManager.State == GameManager.STATE.ChooseCandidate)
		gameManager.CandidateChoosen (candidateSelected);
	}
		
}
