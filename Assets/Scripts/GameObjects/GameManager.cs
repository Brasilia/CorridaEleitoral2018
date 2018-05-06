using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public enum STATE {
		ChooseCandidate, 
		ChooseStaff, 
		Event,
		Proposal,
		ChooseOpponent,
		DebateQuestion,
		DebateReply,
		DebateRejoinder,
		DebateResults
	} // possiveis estados do jogo

	private STATE state;

	public static GameManager instance = null;

	public Carousel uiCarousel;
	public CardTable uiChoiceTable;
	public BoolAction uiBoolSlider;

	public List<Candidate_Data> availableCandidates;	// Lista de scriptable objects de candidatos
	public List<Candidate> candidates = new List<Candidate>();				//
	public List<Event_Data> eventsData;
	public List<CampaignProposal_Data> campProposals;
	public List<DebateQuestion_Data> debateQuestions;
	public int countEvents;
	public int eventsPerCicle;

	public GameObject candidateCardPrefab;
	public GameObject staffCardPrefab;
	public GameObject eventPrefab;
	public GameObject campaignProposalPrefab;
	public GameObject debateQuestionPrefab;

	public STATE State {
		get {
			return state;
		}
		set { 
			state = value; 
		}
	}

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}
		
	// Use this for initialization
	void Start () {
		state = STATE.ChooseCandidate;
		ChooseCandidate ();
	}

	public void ReturnControl(){
		switch (state) {
		case STATE.ChooseCandidate:
			CandidateChosen (uiChoiceTable.candidateSelected);
			state = STATE.ChooseStaff;
			Debug.Log ("ChooseStaff()");
			ChooseStaff ();
			break;
		case STATE.ChooseStaff:
			StaffChosen ();
			Debug.Log ("ah, mlk");
			break;
		}

	}



	public void ChooseCandidate(){
		List<GameObject> candidates = new List<GameObject> ();
		foreach(Candidate_Data cand in availableCandidates){
			GameObject candCard = (GameObject)Instantiate (candidateCardPrefab);
			//Carregar atributos da carta
			candidates.Add (candCard.gameObject);
		}
		uiChoiceTable.SetActiveSelectScreen (candidates);
	}

	// Chamado após o candidato ser escolhido.
	public void CandidateChosen(int index){
		uiChoiceTable.gameObject.SetActive (false);
		if (state == STATE.ChooseCandidate) {
			int i;
			Candidate cand = new Candidate (availableCandidates[index]);
			candidates.Add (cand);
			for (i = 0; i < availableCandidates.Count; i++) {
				if (i != index) {
					cand = new Candidate(availableCandidates [i]);
					candidates.Add (cand);
				}
			}
		}
	}

	public void ChooseStaff(){
		List<GameObject> staff = new List<GameObject> ();
		foreach(Staff_Data s in candidates[0].avaiableStaff){
			Debug.Log ("Staff!");
			GameObject staffCard = (GameObject)Instantiate (staffCardPrefab);
			//Carregar atributos da carta
			staff.Add (staffCard.gameObject);
		}
		uiCarousel.SetCarouselActive (staff, 2);
	}

	public void StaffChosen (){
		for (int i = 0; i < uiCarousel.chosenList.Count; i++){
			candidates[0].hiredStaff.Add( candidates[0].avaiableStaff[uiCarousel.chosenList[i]] );
		}
	}




	// Chamada após a opção booleana ser escolhida (decline or accept, yes or no)
	public void BoolChoosen(bool option, GameObject card){
		// Chama a função dependendo do estado	0 => esquerda (não)		1 => direita (sim)
		if(!option)
			print("Escolheu NO!");
		else
			print("Escolheu YES!");//
		Destroy(card);
		// Próximo passo dependendo da máquina de estados
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
