using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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
		DebateEnd,
		//DebateResults
	} // possiveis estados do jogo

	public STATE state {get; private set;}

	public static GameManager instance = null;

	public GameConfig gameConfig;
	public GameObject eventSystem; // para desativar clicks enquanto finaliza o jogo

	//Referência para os widgets
	public Carousel uiCarousel;
	public CardTable uiChoiceTable;
	public BoolAction uiBoolSlider;
	public ResourcesBHV uiResources;
	public TransitionScreen votesChart;

	//Referência para telas e animações de transição
	public Text fpsDisplay;
	public GameObject quitMenu;
	//public GameObject debateStart;
	public TransitionScreen debateQuestion;
	public TransitionScreen gameOver;
	public GameObject chooseStaffScreen;
	public GameObject chooseProposalScreen;

	//Candidatos - sempre em memória principal; leitura e escrita
	public List<Candidate> candidates = new List<Candidate>();	
	public Candidate leadingCandidate;
	//Data: apenas leitura
	public List<Candidate_Data> availableCandidates;	// Lista de scriptable objects de candidatos
	public List<ElectoralGroup_Data> electorGroups; // Lista de scriptable objects de grupos eleitorais (para cálculo de votos)
	public List<Event_Data> eventsData;
	public List<CampaignProposal_Data> campProposals;
	public List<DebateQuestion_Data> debateQuestions;

	//Gerenciamento do ciclo de eventos
	private int countEvents;
	public int eventsPerCicle = 2;
    public int MainCicles = 4;


	// Gerenciamento do fluxo de jogo
	public int countCicles = 0;
	public int countDebateTurns = 0;
	private bool isFirstDebateTurn;
	private int indexAux;
	private int credibilityBonus = 5;	// FIXME - bônus de credibilidade de boost

	//Referência para os prefabs de cartas
	public GameObject candidateCardPrefab;
	public GameObject staffCardPrefab;
	public GameObject eventPrefab;
	public GameObject campaignProposalPrefab;
	public GameObject debateQuestionPrefab;

	//Atributos para controle do debate
	public int opponentIndex {get; private set;} // índice do oponente do jogador no debate
	public int firstPlayer {get; private set;} // índice do primeiro jogador a debater
	private DebateQuestion_Data currentQuestion;
	//private DebateQuestion_Data chosenQuestion;
	private List<int> questionsIndex = new List<int>();





//	public STATE State {
//		get {
//			return state;
//		}
//		set { 
//			state = value; 
//		}
//	}

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		//DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		//Application.targetFrameRate = 30; //TODO colocar no início (menu inicial)
		state = STATE.ChooseCandidate;
		ChooseCandidate ();
		countEvents = -1;
		//votesGraph.SetGraphActive ();
		/*
		countCicles = 0;
		countDebateTurns = 0;*/
		//isFirstDebateTurn = true;
		//firstPlayer = -1;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)){
			quitMenu.SetActive (true);
		}
		fpsDisplay.text = (1.0f / Time.deltaTime).ToString ("00") + " fps";
	}

	public void QuitGame(){
		eventSystem.SetActive (false);
		Invoke ("TerminateGame", 0.5f);
	}

	private void TerminateGame() {
		Application.Quit ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
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
			if (countEvents == -1){
				countEvents = 0;
				Debug.Log ("First Event");
				ChooseEvent (eventsData.GetRange (0, 1)); // Pega o primeiro evento na lista (tutorial)
				eventsData.RemoveAt (0); // Remove o primeiro, para evitar repetição
			} else {
				ChooseEvent (eventsData);
			}
			break;
		case STATE.Event:
			Debug.Log ("countEvents: " + countEvents);
			List<Event_Data> ev = EventAnswerChosen ();
			if (ev == null) {
				countEvents++;
				ev = eventsData;
			}
			if (countEvents >= eventsPerCicle){
				countEvents = 0;
				state = STATE.Proposal;
				Debug.Log ("ChooseProposal");
				ChooseProposal ();
			} else {
				ChooseEvent (ev);
			}
			break;
		case STATE.Proposal:
			ProposalChosen ();
			state = STATE.ChooseOpponent;
			Debug.Log ("ChooseOpponent");
			uiResources.gameObject.SetActive (false);
			debateQuestion.SetAndMakeTransition (ChooseOpponent, "screen_debate_start");
			break;
		case STATE.ChooseOpponent:
			OpponentChosen();
			state = STATE.DebateQuestion;
			Debug.Log ("AskQuestion");
			debateQuestion.SetAndMakeTransition (AskQuestion, "Screen Debate");
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
			Debug.Log ("DebateRejoinder");
			state = STATE.DebateEnd;
			RejoinderQuestion ();
			break;
		case STATE.DebateEnd:
			Debug.Log ("DebateEnd");
			DebateEnd ();
			break;

			/*
		case STATE.DebateRejoinder:
			RejoinderQuestion ();
			ShowRejoinder ();
			// ?? Simulação dos outros candidatos debatendo
			state = STATE.DebateResults;
			ShowResults ();
			break;
			*/
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
			UpdateIntentionsLists ();
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
		chooseStaffScreen.SetActive (true);
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
		indexAux = Random.Range (0, events.Count); //params are min(inclusive), max(exclusive)
		GameObject ev = Instantiate(eventPrefab);
		ev.GetComponent<EventBHV> ().Load (events[indexAux]); //Carregar atributos da carta - de índice rand
		events.RemoveAt (indexAux);
		uiBoolSlider.SetActiveBoolAction (ev);
	}

	private List<Event_Data> EventAnswerChosen(){
		Event_Data evData = (Event_Data)(uiBoolSlider.card.GetComponent<EventBHV> ().cardData);
		EventAction_Data chosenAction;
		if(uiBoolSlider.choice == false){
			chosenAction = evData.actionDecline;
		} else {
			chosenAction = evData.actionAccept;
		}
		SetEventConsequences(chosenAction, 0);
		//eventsData.RemoveAt (indexAux);
		if(chosenAction.nextEvent != null){
			List<Event_Data> evs = new List<Event_Data> ();
			evs.Add (chosenAction.nextEvent);
			return (evs);
		} else {
			return null;
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
		chooseProposalScreen.SetActive (true);
	}

	private void ProposalChosen(){
		CampaignProposal_Data prop = (CampaignProposal_Data)( uiCarousel.cards [uiCarousel.chosenList [0]].GetComponent<CampaignProposalBHV>().cardData );
		Debug.Log (uiCarousel.chosenList.Count);
		Debug.Log ("prop: " + prop);
		SetEventConsequences (prop.actionAccept, 0);
		candidates [0].debateBoosts.Add (prop.debateBoost);
		campProposals.Remove (prop);
	}


	private void ChooseOpponent(){
		//debateStart.SetActive (true);
		if (countDebateTurns == 0) {	// Se é a primeira jogada do debate
			countDebateTurns++;
			firstPlayer = Random.Range (0, 2);
			if (firstPlayer == 0) {	// Jogador inicia a jogada
				PlayerChooseOpponent();
			} else { 	// IA inicia a jogada => sorteia o oponente
				IAChooseOpponent();
			}
		} else { // Se é a segunda rodada do debate => inverte quem pergunta
			countDebateTurns++;
			if (firstPlayer == 0) {	// Agora a IA pergunta
				firstPlayer++;
				IAChooseOpponent();
			} else {				// Agora o player pergunta
				firstPlayer--;
				PlayerChooseOpponent();
			}
		}
	}

	private void PlayerChooseOpponent(){
		List<GameObject> candidates = new List<GameObject> ();
		for (int i = 1; i < this.candidates.Count; i++) {
			GameObject candCard = (GameObject)Instantiate (candidateCardPrefab);
			Debug.Log (this.candidates [i]);
			Debug.Log (candCard.GetComponent<CandidateBHV> ());
			candCard.GetComponent<CandidateBHV> ().Load (this.candidates [i]); //Carregar atributos da carta
			candidates.Add (candCard.gameObject);
		}/*
			foreach (Candidate cand in this.candidates) {
				GameObject candCard = (GameObject)Instantiate (candidateCardPrefab);
				Debug.Log (cand);
				Debug.Log (candCard.GetComponent<CandidateBHV> ());
				candCard.GetComponent<CandidateBHV> ().Load (cand); //Carregar atributos da carta
				candidates.Add (candCard.gameObject);
			}*/
		uiChoiceTable.SetActiveSelectScreen (candidates);
	}

	private void IAChooseOpponent(){
		opponentIndex = Random.Range (1, candidates.Count);
		Invoke ("ReturnControl", 0.1f); // necessário usar Invoke para não conflitar com o SetActive(false) da animação
	}

	private void OpponentChosen(){
		if (firstPlayer == 0) {
			opponentIndex = uiChoiceTable.candidateSelected + 1;
			uiChoiceTable.gameObject.SetActive (false);
			Debug.Log ("Opponent Chosen: " + opponentIndex);
		} 
	}

	private void AskQuestion(){
		if (firstPlayer == 0) {	// Se o player faz a pergunta
			//Sorteia 3 debate questions
			List<GameObject> questions = new List<GameObject>();
            //debateQuestions = debateQuestions.OrderBy(a => Random.Range(0f, 1000f)).ToList();
			for (int i = 0; i < 3; i++) {
				// FIXME - tirar repetição
				int index = Random.Range (0, debateQuestions.Count);
                while (questionsIndex.Contains(index) && debateQuestions.Count >=3)
                {
                    index = Random.Range(0, debateQuestions.Count);
                }
				questionsIndex.Add (index);
				GameObject questionCard = (GameObject)Instantiate (debateQuestionPrefab);
				questionCard.GetComponent<EventBHV> ().Load (debateQuestions [index]);
				questions.Add (questionCard);
			}
			Debug.Log ("Player faz primeira pergunta");
			//poe no carrossel
			uiCarousel.SetCarouselActive (questions, 1);
		} else {	// Se a IA faz a pergunta
			// Gera o card com a pergunta
			indexAux = Random.Range(0, debateQuestions.Count);
			currentQuestion = debateQuestions [indexAux];
			GameObject questionCard = (GameObject)Instantiate (debateQuestionPrefab);
			questionCard.GetComponent<EventBHV> ().Load (currentQuestion);
            debateQuestions.RemoveAt(indexAux);
            Debug.Log ("IA fez primeira pergunta.");
			// Envia o card para o Bool Action.
			uiBoolSlider.SetActiveBoolAction(questionCard);
		}
	}

	private void QuestionAsked(){
		if (firstPlayer == 0) {		// Se o player perguntou
			indexAux = uiCarousel.chosenList[0];
			currentQuestion = debateQuestions[questionsIndex[indexAux]];	// Pergunta escolhida no carousel.
            debateQuestions.RemoveAt(questionsIndex[indexAux]);
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
		// FIXME
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
		ReturnControl ();
	}

	private void DebateEnd(){
		Debug.Log ("Count Cicles = " + countCicles);
		DebateSimulation ();
		if (countDebateTurns == 1) {	// Se volta pro início do debate
			Debug.Log("Volta pro início");
			state = STATE.ChooseOpponent;
			uiResources.gameObject.SetActive (false);
			ChooseOpponent();
			//ReturnControl ();
		} else {	// Se volta pros eventos (ou fim)
			countCicles++;
			if (countCicles < MainCicles) {	// Se o loop do jogo ainda não acabou
				Debug.Log("Volta pros eventos");
				countDebateTurns = 0;
				state = STATE.Event;
				// Grafico
				UpdateIntentionsLists ();
				votesChart.SetAndMakeTransition (StartNewCicle, "chartCicleEnd");
			} else {
				// Grafico
				UpdateIntentionsLists ();
				votesChart.SetAndMakeTransition (EndGame, "chartCicleEnd");
				// end game
			}
		} 
	}

	private void StartNewCicle() {
		uiResources.SetResourcesActive ();
		ChooseEvent (eventsData);
	}

	private void EndGame() {
		float maxVotes = 0;
		foreach (Candidate cand in candidates) {
			if (cand.voteIntention > maxVotes) {
				maxVotes = cand.voteIntention;
				leadingCandidate = cand;
			}
		}
		if (leadingCandidate == candidates[0]) { // player won
			gameOver.SetAndMakeTransition (BackToMainMenu, "EndGame");
		} else { // player lost
			gameOver.SetAndMakeTransition (BackToMainMenu, "EndGame");
		}
	}

	private void BackToMainMenu() {
		SceneManager.LoadSceneAsync (gameConfig.sceneMenuName);
	}





	// Função principal de simulação de outros confrontos
	private void DebateSimulation(){
		ArrayList debatedCandidates = new ArrayList ();
		int oppa, oppb, lim;

		debatedCandidates.Add (opponentIndex);

		// Se o número de candidatos for par, a simulação é feita em n-1 candidatos.
		// Se o número de candidatos for ímpar, a simulação é feita em n-2 candidatos.
		if ((candidates.Count % 2) == 0)
			lim = 1;
		else
			lim = 2;

		while(debatedCandidates.Count < (candidates.Count - lim)){		// Enquanto não simulou todos os embates
			// Sorteia oponente A
			do{
				oppa = Random.Range(1, candidates.Count);
			} while(!debatedCandidates.Contains(oppa));
			debatedCandidates.Add (oppa);
			// Sorteia oponente B
			do{
				oppb = Random.Range(1, candidates.Count);
			} while(!debatedCandidates.Contains(oppa));
			debatedCandidates.Add (oppb);

			currentQuestion = debateQuestions[Random.Range (0, debateQuestions.Count)];

			opponentIndex = oppa;
			IAChooseAnswer ();
			opponentIndex = oppb;
			IAChooseAnswer ();
			opponentIndex = oppa;
			IAChooseAnswer ();
			opponentIndex = oppb;
			IAChooseAnswer ();
		}
	}

	/*
	private void ShowRejoinder(){
		// Cria evento pro player de acordo com a resposta - player só vê a resposta, joga pra qualquer um dos lados, sem consequencia
	}

	/*
	private void ShowResults(){
		// Mostra resultados gerais do ciclo de debate
	}
	*/
	private void GetPlayerAnswer(){
		if (uiBoolSlider.choice){ // chose action accept
			SetEventConsequences(currentQuestion.actionAccept, 0);
			currentQuestion = (DebateQuestion_Data)currentQuestion.actionAccept.nextEvent;
			Debug.Log ("Player aceitou.");// Consequências de positivo
		}
		else
        { // chose action decline
            SetEventConsequences(currentQuestion.actionDecline, 0);
			currentQuestion = (DebateQuestion_Data)currentQuestion.actionDecline.nextEvent;
			Debug.Log ("Player recusou.");// Consequências de negativo
		}
	}

	// Exclusivo para o evento de réplica
	private void GetPlayerReply(){
		if (uiBoolSlider.choice){	// Faz propaganda
			if(candidates[0].HasBoosts(currentQuestion.actionAccept))	// Boost
				candidates[0].resources.credibility += credibilityBonus;
			SetEventConsequences(currentQuestion.actionAccept, 0);
			currentQuestion = (DebateQuestion_Data)currentQuestion.actionAccept.nextEvent;
			Debug.Log ("Player aceitou. (Ataque?)");// Consequências de positivo
		}
		else{	// Faz ataque
			// FIXME - o quanto de credibilidade deve ser diminuído com base na corrupção dele?
			candidates[opponentIndex].resources.credibility -= (candidates[opponentIndex].resources.corruption)/2;
			//
			SetEventConsequences(currentQuestion.actionDecline, 0);
			currentQuestion = (DebateQuestion_Data)currentQuestion.actionDecline.nextEvent;
			Debug.Log ("Player recusou. (Ataque?)");// Consequências de negativo
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









	// Métodos de cálculo de inteções de votos...
	public void UpdateIntentionsLists() {
		UpdateIntentions ();
		foreach (Candidate cand in candidates) {
			cand.voteIntentions.Add (cand.voteIntention);
		}
	}

	public void UpdateIntentions(){
		//Reseta intenções de voto
		foreach(Candidate cand in candidates){
			cand.voteIntention = 0;
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
				//Debug.Log ("total Attraction " + totalAttraction);
				cand.voteIntention += partialIntentions; 
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
		//Debug.Log ("Attraction Factor " + af);
		return af*af;
	}


}
