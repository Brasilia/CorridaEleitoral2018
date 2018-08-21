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
	public ResourcesBHV uiResources;

	//Candidatos - sempre em memória principal; leitura e escrita
	public List<Candidate> candidates = new List<Candidate>();	
	//Data: apenas leitura
	public List<Candidate_Data> availableCandidates;	// Lista de scriptable objects de candidatos
	public List<ElectoralGroup_Data> electorGroups; // Lista de scriptable objects de grupos eleitorais (para cálculo de votos)
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

	//Atributos para controle do debate
	private int opponentIndex;
	private int firstPlayer;
	private DebateQuestion_Data currentQuestion;
	private List<int> questionsIndex = new List<int>();
	

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
		UpdateIntentions (); // FIXME - local temporário para teste
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
			uiResources.SetResourcesActive ();
			ChooseEvent (eventsData);
			break;
		case STATE.Event:
			EventAnswerChosen ();
			countEvents++;
			if (countEvents >= eventsPerCicle){
				state = STATE.Proposal;
				Debug.Log ("ChooseProposal");
				ChooseProposal ();
			} else {
				ChooseEvent (eventsData);
			}
			break;
		case STATE.Proposal:
			ProposalChosen ();
			state = STATE.ChooseOpponent;
			Debug.Log ("ChooseOpponent");
			uiResources.gameObject.SetActive (false);
			ChooseOpponent ();
			break;
		case STATE.ChooseOpponent:
			OpponentChosen ();
			state = STATE.DebateQuestion;
			Debug.Log ("AskQuestion");
			AskQuestion ();
			break;
		case STATE.DebateQuestion:
			state = STATE.DebateReply;
			QuestionAsked ();
			Debug.Log ("DebateReply");
			break;
		case STATE.DebateReply:
			state = STATE.DebateRejoinder;
			ReplyQuestion ();
			break;
		case STATE.DebateRejoinder:
			RejoinderQuestion ();
			ShowRejoinder ();
			// ?? Simulação dos outros candidatos debatendo
			state = STATE.DebateResults;
			ShowResults ();
			break;
		}

		/*Debate Question
		 * 	Player escolhe pergunta ou IA escolhe pergunta
		 *
		 *
		*/
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
			Staff_Data staffChosen = candidates [0].avaiableStaff [uiCarousel.chosenList [i]];
			SetResourcesFromStaff (staffChosen);
			candidates[0].hiredStaff.Add(staffChosen);
		}
		//uiCarousel.chosenList.Clear (); passado pra classe carousel
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

		SetEventConsequences(chosenAction, 0);

		if(chosenAction.nextEvent != null){
			GameObject ev = Instantiate(eventPrefab);
			ev.GetComponent<EventBHV> ().Load (chosenAction.nextEvent); //Carregar atributos da carta - apontada por chosenAction.nextEvent
			uiBoolSlider.SetActiveBoolAction (ev);
		}
	}

	private void ChooseProposal(){
		int nAlternatives = 3;
		List<int> rands = new List<int> ();
		int rand;
		List<GameObject> proposals = new List<GameObject> ();
		int i = 0;
		while (i < nAlternatives){
			rand = Random.Range (0, campProposals.Count);
			if (!rands.Contains(rand)){
				rands.Add (rand);
				GameObject prop = Instantiate(campaignProposalPrefab);
				prop.GetComponent<CampaignProposalBHV> ().Load (campProposals[rand]);
				proposals.Add (prop);
				i++;
			}
		}
		uiCarousel.SetCarouselActive (proposals, 1);
	}

	private void ProposalChosen(){
		CampaignProposal_Data prop = (CampaignProposal_Data)( uiCarousel.cards [uiCarousel.chosenList [0]].GetComponent<CampaignProposalBHV>().cardData );
		Debug.Log (uiCarousel.chosenList.Count);
		Debug.Log ("prop: " + prop);
		SetEventConsequences (prop.actionAccept, 0);
	}


	private void ChooseOpponent(){
		firstPlayer = Random.Range (0, 2);
		if (firstPlayer == 0) {	// Jogador inicia a jogada
			List<GameObject> candidates = new List<GameObject> ();
			for (int i = 1; i < this.candidates.Count; i++) {
				GameObject candCard = (GameObject)Instantiate (candidateCardPrefab);
				Debug.Log (this.candidates[i]);
				Debug.Log (candCard.GetComponent<CandidateBHV> ());
				candCard.GetComponent<CandidateBHV> ().Load (this.candidates[i]); //Carregar atributos da carta
				candidates.Add(candCard.gameObject);
			}/*
			foreach (Candidate cand in this.candidates) {
				GameObject candCard = (GameObject)Instantiate (candidateCardPrefab);
				Debug.Log (cand);
				Debug.Log (candCard.GetComponent<CandidateBHV> ());
				candCard.GetComponent<CandidateBHV> ().Load (cand); //Carregar atributos da carta
				candidates.Add (candCard.gameObject);
			}*/
			uiChoiceTable.SetActiveSelectScreen (candidates);
		} else { 	// IA inicia a jogada => sorteia o oponente
			opponentIndex = Random.Range (1, candidates.Count);
			ReturnControl ();
		}
	}

	private void OpponentChosen(){
		if (firstPlayer == 0) {
			opponentIndex = uiChoiceTable.candidateSelected;
			uiChoiceTable.gameObject.SetActive (false);
		} 
	}

	private void AskQuestion(){
		if (firstPlayer == 0) {	// Se o player faz a pergunta
			//Sorteia 3 debate questions
			List<GameObject> questions = new List<GameObject>();
			for (int i = 0; i < 3; i++) {
				// FIXME - tirar repetição
				int index = Random.Range (0, debateQuestions.Count);
				questionsIndex.Add (index);
				GameObject questionCard = (GameObject)Instantiate (debateQuestionPrefab);
				questionCard.GetComponent<EventBHV> ().Load (debateQuestions [index]);
				questions.Add (questionCard);
			}
			Debug.Log ("Player fez primeira pergunta");
			//poe no carrossel
			uiCarousel.SetCarouselActive (questions, 1);
		} else {	// Se a IA faz a pergunta
			// Gera o card com a pergunta
			int index = Random.Range(0, debateQuestions.Count);
			currentQuestion = debateQuestions [index];
			GameObject questionCard = (GameObject)Instantiate (debateQuestionPrefab);
			questionCard.GetComponent<EventBHV> ().Load (currentQuestion);

			Debug.Log ("IA fez primeira pergunta.");
			// Envia o card para o Bool Action.
			uiBoolSlider.SetActiveBoolAction(questionCard);
		}
	}

	private void QuestionAsked(){
		if (firstPlayer == 0) {		// Se o player perguntou
			currentQuestion = debateQuestions[uiCarousel.chosenList[0]];	// Pergunta escolhida no carousel.
			// IA escolhe resposta e exibe
			IAChooseAnswer ();
			ShowUIAnswer ();
		} else {	// Pega a resposta do player
			GetPlayerAnswer();
			ReturnControl ();
		}

	}

	private void ReplyQuestion(){
		// Exibe a tréplica da IA, se o player iniciou o debate, ou a réplica da IA, caso contrário.
		// Pega a resposta do player
		if (firstPlayer == 0) 
			GetPlayerAnswer ();

		IAChooseAnswer ();
		ShowUIAnswer ();
	}

	private void RejoinderQuestion(){
		// Consequências da tréplica do player
		if (firstPlayer == 1) 
			GetPlayerAnswer ();
		questionsIndex.Clear ();
	}

	private void ShowRejoinder(){
		// Cria evento pro player de acordo com a resposta - player só vê a resposta, joga pra qualquer um dos lados, sem consequencia
	}

	private void ShowResults(){
		// Mostra resultados gerais do ciclo de debate
	}

	private void GetPlayerAnswer(){
		if (uiBoolSlider.choice){
			SetEventConsequences(currentQuestion.actionAccept, 0);
			currentQuestion = (DebateQuestion_Data)currentQuestion.actionAccept.nextEvent;
			Debug.Log ("Player aceitou.");// Consequências de positivo
		}
		else{
			SetEventConsequences(currentQuestion.actionDecline, 0);
			currentQuestion = (DebateQuestion_Data)currentQuestion.actionDecline.nextEvent;
			Debug.Log ("Player recusou.");// Consequências de negativo
		}
	}

	private void IAChooseAnswer(){
		// TODO: IA escolhe a resposta
		int answer = Random.Range (0, 2);

		if (answer == 0){
			SetEventConsequences (currentQuestion.actionAccept, opponentIndex);
			currentQuestion = (DebateQuestion_Data) currentQuestion.actionAccept.nextEvent;
			Debug.Log ("IA aceitou");
		}
		else{
			SetEventConsequences (currentQuestion.actionDecline, opponentIndex);
			currentQuestion = (DebateQuestion_Data) currentQuestion.actionDecline.nextEvent;
			Debug.Log ("IA recusou.");
		}
	}

	private void ShowUIAnswer(){
		GameObject ev = Instantiate (eventPrefab);
		ev.GetComponent<EventBHV> ().Load (currentQuestion); // FIXME - IA vai escolher se aceita ou rejeita
		uiBoolSlider.SetActiveBoolAction (ev);
	}

	// Incrementa alinhamento e recursos do player com valores do staff
	private void SetEventConsequences(EventAction_Data eventChosen, int index){
		// Incrementa recursos
		candidates [index].resources.cash += eventChosen.resources.cash;
		candidates [index].resources.corruption += eventChosen.resources.corruption;
		candidates [index].resources.credibility += eventChosen.resources.credibility;
		candidates [index].resources.visibility += eventChosen.resources.visibility;
		
		// Incrementa alinhamento
		candidates [index].alignment += eventChosen.alignment;

		if(index == 0)
			uiResources.UpdateValues ();
	}

	// Incrementa atributos do player com os valores de recursos do staff
	private void SetResourcesFromStaff(Staff_Data staffChosen){
		candidates [0].resources.cash += staffChosen.resources.cash;
		candidates [0].resources.corruption += staffChosen.resources.corruption;
		candidates [0].resources.credibility += staffChosen.resources.credibility;
		candidates [0].resources.visibility += staffChosen.resources.visibility;

		uiResources.UpdateValues ();
	}

	
	// Update is called once per frame
	void Update () {
		
	}






	// Métodos de cálculo de inteções de votos...
	public void UpdateIntentions(){
		//Reseta intenções de voto
		foreach(Candidate cand in candidates){
			cand.voteIntentions = 0;
		}
		//Recalcula intenções
		foreach (ElectoralGroup_Data elec in electorGroups){
			//Calcula intenções, partindo deste grupo de eleitores
			List<float> attractionFactors = new List<float>();
			float af;
			float totalAttraction = 0.0f;
			foreach(Candidate cand in candidates){
				af = GetAttractionFactor (cand, elec);
				attractionFactors.Add (af);
				totalAttraction += af;
			}
			foreach (Candidate	cand in candidates){
				float partialIntentions = 
					(float)elec.weight * attractionFactors[candidates.IndexOf(cand)] / totalAttraction;
				Debug.Log ("total Attraction " + totalAttraction);
				cand.voteIntentions += partialIntentions; 
			}
		}
		//mainScreen.UpdateVoteIntentionsDisplay ();
	}

	private float GetDistance (Candidate p1, ElectoralGroup_Data p2){
		float distance1, distance2, d1, d2, d3, d4;
		d1 = Mathf.Abs(p1.alignment.economic.value - p2.alignment.economic.value);
		d2 = Mathf.Abs(p1.alignment.civil.value - p2.alignment.civil.value);
		d3 = Mathf.Abs(p1.alignment.societal.value - p2.alignment.societal.value);
		d4 = Mathf.Abs(p1.resources.visibility - p2.resources.visibility);
		distance1 = d1 + d2 + d3;
		distance2 = distance1 + d4*d4 / (p1.resources.visibility*p1.resources.visibility+200);// + Mathf.Sqrt(p2.exposition/(p1.exposition+100));
		return (distance2 + 0.001f);
	}

//	//Euclidiana... não parece interessante
//	private float GetEuclidianDistance(Player p1, Player p2){
//		//Distância nas 5 dimensões
//		float distance1, distance2, d1, d2, d3, d4, d5;
//		d1 = p1.economicEqualityMarkets - p2.economicEqualityMarkets;
//		d2 = p1.diplomaticNationGlobe - p2.diplomaticNationGlobe;
//		d3 = p1.civilAuthorityLiberty - p2.civilAuthorityLiberty;
//		d4 = p1.societalTraditionProgress - p2.societalTraditionProgress;
//		d5 = Mathf.Abs(p1.exposition - p2.exposition);
//		//d5 = 1.0f * p2.exposition / (p1.exposition+1); //teste
//		distance1 = Mathf.Sqrt (d1 * d1 + d2 * d2 + d3 * d3 + d4 * d4);
//		distance2 = Mathf.Sqrt (d1 * d1 + d2 * d2 + d3 * d3 + d4 * d4 + d5 * d5);
//		return (distance1 + distance2 + 0.001f);// + distance*d5*d5;
//	}

	private float GetAttractionFactor(Candidate p1, ElectoralGroup_Data elec){
		float af = (float)(p1.resources.visibility+100) / GetDistance (p1, elec);
		Debug.Log ("Attraction Factor " + af);
		return af;
	}


}
