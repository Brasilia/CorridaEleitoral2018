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

	//Referência para os widgets
	public Carousel uiCarousel;
	public CardTable uiChoiceTable;
	public BoolAction uiBoolSlider;

	//Candidatos - sempre em memória principal; leitura e escrita
	public List<Candidate> candidates = new List<Candidate>();	
	//Data: apenas leitura
	public List<Candidate_Data> availableCandidates;	// Lista de scriptable objects de candidatos
	public List<Event_Data> eventsData;
	public List<CampaignProposal_Data> campProposals;
	public List<DebateQuestion_Data> debateQuestions;

	//Gerenciamento do ciclo de eventos
	public int countEvents;
	public int eventsPerCicle;

	//Referência para os prefabs de cartas
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
		countEvents = 0;
	}

	//Método para receber o controle de volta para o Game Manager
	public void ReturnControl(){
		switch (state) {
		case STATE.ChooseCandidate:
			CandidateChosen ();
			state = STATE.ChooseStaff;
			Debug.Log ("ChooseStaff()");
			ChooseStaff ();
			break;
		case STATE.ChooseStaff:
			StaffChosen ();
			state = STATE.Event;
			Debug.Log ("ah, mlk");
			ChooseEvent (eventsData);
			break;
		case STATE.Event:
			EventAnswerChosen ();
			countEvents++;
			if (countEvents >= eventsPerCicle){
				state = STATE.Proposal;
				Debug.Log ("ChooseProposal");
			} else {
				ChooseEvent (eventsData);
			}
			break;
		}
	}

	//--Start: Choose Candidate
	private void ChooseCandidate(){
		List<GameObject> candidates = new List<GameObject> ();
		foreach(Candidate_Data cand in availableCandidates){
			GameObject candCard = (GameObject)Instantiate (candidateCardPrefab);
			Debug.Log (cand);
			Debug.Log (candCard.GetComponent<CandidateBHV> ());
			candCard.GetComponent<CandidateBHV> ().Load (cand); //Carregar atributos da carta
			candidates.Add (candCard.gameObject);
		}
		uiChoiceTable.SetActiveSelectScreen (candidates);
	}

	// Chamado após o candidato ser escolhido.
	private void CandidateChosen(){
		int index = uiChoiceTable.candidateSelected;
		uiChoiceTable.gameObject.SetActive (false);
		if (state == STATE.ChooseCandidate) {
			Candidate cand = new Candidate (availableCandidates[index]);
			candidates.Add (cand);
			for (int i = 0; i < availableCandidates.Count; i++) {
				if (i != index) {
					cand = new Candidate(availableCandidates [i]);
					candidates.Add (cand);
				}
			}
		}
	}

	//--Start: Choose Staff
	private void ChooseStaff(){
		List<GameObject> staff = new List<GameObject> ();
		foreach(Staff_Data s in candidates[0].avaiableStaff){
			Debug.Log ("Staff!");
			GameObject staffCard = (GameObject)Instantiate (staffCardPrefab);
			staffCard.GetComponent<StaffBHV> ().Load (s); //Carregar atributos da carta
			staff.Add (staffCard.gameObject);
		}
		uiCarousel.SetCarouselActive (staff, 2);
	}

	private void StaffChosen (){
		for (int i = 0; i < uiCarousel.chosenList.Count; i++){
			candidates[0].hiredStaff.Add( candidates[0].avaiableStaff[uiCarousel.chosenList[i]] );
		}
		uiCarousel.chosenList.Clear ();
	}

	//--Events
	private void ChooseEvent(List<Event_Data> events){
		int rand = Random.Range (0, events.Count); //params are min(inclusive), max(exclusive)
		GameObject ev = Instantiate(eventPrefab);
		ev.GetComponent<EventBHV> ().Load (events[rand]); //Carregar atributos da carta - de índice rand
		uiBoolSlider.SetActiveBoolAction (ev);
	}

	private void EventAnswerChosen(){
		Event_Data evData = (Event_Data)(uiBoolSlider.card.GetComponent<EventBHV> ().cardData);
		EventAction_Data chosenAction;
		if(uiBoolSlider.choice == false){
			chosenAction = evData.actionDecline;
		} else {
			chosenAction = evData.actionAccept;
		}
		//apply action TODO
		if(chosenAction.nextEvent != null){
			GameObject ev = Instantiate(eventPrefab);
			ev.GetComponent<EventBHV> ().Load (chosenAction.nextEvent); //Carregar atributos da carta - apontada por chosenAction.nextEvent
			uiBoolSlider.SetActiveBoolAction (ev);
		}
	}




//	// Chamada após a opção booleana ser escolhida (decline or accept, yes or no)
//	public void BoolChosen(bool option, GameObject card){
//		// Chama a função dependendo do estado	0 => esquerda (não)		1 => direita (sim)
//		if(!option)
//			print("Escolheu NO!");
//		else
//			print("Escolheu YES!");//
//		Destroy(card);
//		// Próximo passo dependendo da máquina de estados
//	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
