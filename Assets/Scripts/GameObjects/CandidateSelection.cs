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
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		player = GameObject.Find ("Player").GetComponent<Player> ();
		candidateSelected = -1;
		UpdateSelectScreen ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateSelectScreen(){
		int i;
		for (i = 0; i < gameManager.otherCandidates.Count; i++) 
			imgCandidate[i].sprite = gameManager.otherCandidates[i].image;
	}

	public void OnClickCandidate(int candidateSelected){
		this.candidateSelected = candidateSelected;
		print ("Você selecionou o candidato " + gameManager.otherCandidates [candidateSelected].name);
	}
		
}
