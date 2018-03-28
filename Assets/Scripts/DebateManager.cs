using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebateManager : MonoBehaviour {
	public List<Image> imgCandidate;
	public List<Text> txtCredibility;
	public List<Text> txtCorruption;
	public List<Text> txtVisibility;
	public List<Image> imgEconomic;
	public List<Image> imgCivil;
	public List<Image> imgSocietal;

	public Sprite equalityPrefab;
	public Sprite marketPrefab;
	public Sprite authorityPrefab;
	public Sprite libertyPrefab;
	public Sprite traditionPrefab;
	public Sprite progressPrefab;

	public GameObject selectScreen;
	public GameObject versusScreen;
	public GameObject popUp;

	public GameManager gameManager;
	public Player player;

	private int candidateSelected;

	// Use this for initialization
	void Start () {
		selectScreen.SetActive (true);
		versusScreen.SetActive (false);
		popUp.SetActive (false);
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
		for (i = 0; i < gameManager.otherCandidates.Count; i++) {
			if (!gameManager.otherCandidates [i].name.Equals (player.name)) {		// Se o candidato não é o player
				imgCandidate[i].sprite = gameManager.otherCandidates[i].image;
				txtCredibility [i].text = gameManager.otherCandidates [i].credibility.ToString();
				txtCorruption [i].text = gameManager.otherCandidates [i].corruption.ToString();
				txtVisibility [i].text = gameManager.otherCandidates [i].exposition.ToString();

				if (gameManager.otherCandidates [i].economicEqualityMarkets > 0)
					imgEconomic [i].sprite = marketPrefab;
				else
					imgEconomic [i].sprite = equalityPrefab;
				
				if (gameManager.otherCandidates [i].civilAuthorityLiberty > 0)
					imgCivil [i].sprite = libertyPrefab;
				else
					imgCivil [i].sprite = authorityPrefab;
					
				if (gameManager.otherCandidates [i].societalTraditionProgress > 0)
					imgSocietal [i].sprite = progressPrefab;
				else
					imgSocietal [i].sprite = traditionPrefab;
			}
		}
	}

	public void OnClickCandidate1(){
		if (!popUp.activeInHierarchy) {
			candidateSelected = 0;
			popUp.SetActive (true);
		}
	}

	public void OnClickCandidate2(){
		if (!popUp.activeInHierarchy) {
			candidateSelected = 1;
			popUp.SetActive (true);
		}
	}

	public void OnClickCandidate3(){
		if (!popUp.activeInHierarchy) {
			candidateSelected = 2;
			popUp.SetActive (true);
		}
	}

	public void OnClickCandidate4(){
		if (!popUp.activeInHierarchy) {
			candidateSelected = 2;
			popUp.SetActive (true);
		}
	}

	public void OnClickCandidate5(){
		if (!popUp.activeInHierarchy) {
			candidateSelected = 4;
			popUp.SetActive (true);
		}
	}

	public void OnClickCandidate6(){
		if (!popUp.activeInHierarchy) {
			candidateSelected = 5;
			popUp.SetActive (true);
		}
	}

	public void OnClickDebate(){
		versusScreen.SetActive (true);
		popUp.SetActive (false);
		selectScreen.SetActive (false);
	}

	public void OnClickClosePopUp(){
		candidateSelected = -1;
		popUp.SetActive (false);
	}
}
