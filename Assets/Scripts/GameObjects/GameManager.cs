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
			QuestionAsked ();
			state = STATE.DebateReply;
			Debug.Log ("DebateReply");
			ReplyQuestion ();
			break;
		case STATE.DebateReply:
			RejoinderQuestion ();
			state = STATE.DebateRejoinder;
			ShowRejoinder ();
			break;
		case STATE.DebateRejoinder:
			// ?? Simulação dos outros candidatos debatendo
			state = STATE.DebateResults;
			ShowResults ();
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

		SetEventConsequences(chosenAction);

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
		SetEventConsequences (prop.actionAccept);
	}


	private void ChooseOpponent(){
		List<GameObject> candidates = new List<GameObject> ();
		foreach(Candidate cand in this.candidates){
			GameObject candCard = (GameObject)Instantiate (candidateCardPrefab);
			Debug.Log (cand);
			Debug.Log (candCard.GetComponent<CandidateBHV> ());
			candCard.GetComponent<CandidateBHV> ().Load (cand); //Carregar atributos da carta
			candidates.Add (candCard.gameObject);
		}
		uiChoiceTable.SetActiveSelectScreen (candidates);
	}

	private void OpponentChosen(){
		opponentIndex = uiChoiceTable.candidateSelected;
		uiChoiceTable.gameObject.SetActive (false);
	}

	private void AskQuestion(){
		//Sorteia 3 debate questions
		List<GameObject> questions = new List<GameObject>();
		for (int i = 0; i < 3; i++){
			// FIXME - tirar repetição
			int index = Random.Range (0, debateQuestions.Count);
			GameObject questionCard = (GameObject)Instantiate (debateQuestionPrefab);
			questionCard.GetComponent<EventBHV> ().Load (debateQuestions [index]);
			questions.Add (questionCard);
		}
		//poe no carrossel
		uiCarousel.SetCarouselActive (questions, 1);
	}

	private void QuestionAsked(){
		// IA do oponente escolhe resposta
		// Gera consequencias da resposta
		// Cria evento pro player de acordo com a resposta
		// Chama uiBoolSlider

	}

	private void ReplyQuestion(){
		// FIXME - puxamos o conteúdo que deveria estar em QuestionAsked?
		DebateQuestion_Data question = (DebateQuestion_Data)( uiCarousel.cards [uiCarousel.chosenList [0]].GetComponent<EventBHV>().cardData ); // FIXME - EventBHV deve ser DebateQuestionBHV
		GameObject ev = Instantiate(eventPrefab);
		ev.GetComponent<EventBHV> ().Load (question.actionAccept.nextEvent); // FIXME - IA vai escolher se aceita ou rejeita
		uiBoolSlider.SetActiveBoolAction (ev);
	}

	private void RejoinderQuestion(){
		// IA do oponente escolhe resposta
		// Gera consequencias da resposta

	}

	private void ShowRejoinder(){
		// Cria evento pro player de acordo com a resposta - player só vê a resposta, joga pra qualquer um dos lados, sem consequencia
	}

	private void ShowResults(){
		// Mostra resultados gerais do ciclo de debate
	}


	// Incrementa alinhamento e recursos do player com valores do staff
	private void SetEventConsequences(EventAction_Data eventChosen){
		// Incrementa recursos
		candidates [0].resources.cash += eventChosen.resources.cash;
		candidates [0].resources.corruption += eventChosen.resources.corruption;
		candidates [0].resources.credibility += eventChosen.resources.credibility;
		candidates [0].resources.visibility += eventChosen.resources.visibility;
		
		// Incrementa alinhamento
		
		// Economic
		candidates[0].alignment.economic.value += eventChosen.alignment.economic.value;
		candidates[0].alignment.economic.bolsaFamilia += eventChosen.alignment.economic.bolsaFamilia;
		candidates[0].alignment.economic.salarioMinimo += eventChosen.alignment.economic.salarioMinimo;
		candidates[0].alignment.economic.impostoDeRenda += eventChosen.alignment.economic.impostoDeRenda;
		candidates[0].alignment.economic.privatizacao += eventChosen.alignment.economic.privatizacao;
		candidates[0].alignment.economic.previdencia += eventChosen.alignment.economic.previdencia;
		
		// Civil
		candidates[0].alignment.civil.value += eventChosen.alignment.civil.value;
		candidates[0].alignment.civil.servicoMilitarObrigatorio += eventChosen.alignment.civil.servicoMilitarObrigatorio;
		candidates[0].alignment.civil.escolasMilitares += eventChosen.alignment.civil.escolasMilitares;
		
		// Societal
		candidates[0].alignment.societal.value += eventChosen.alignment.societal.value;
		candidates[0].alignment.societal.ensinoReligiosoEscolas += eventChosen.alignment.societal.ensinoReligiosoEscolas;
		candidates[0].alignment.societal.legalizacaoAborto += eventChosen.alignment.societal.legalizacaoAborto;
		candidates[0].alignment.societal.casamentoGay += eventChosen.alignment.societal.casamentoGay;
		candidates[0].alignment.societal.legalizacaoDrogas += eventChosen.alignment.societal.legalizacaoDrogas;
		
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
}
