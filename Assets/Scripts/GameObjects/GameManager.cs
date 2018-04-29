//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class GameManager : MonoBehaviour {
//
//	public static GameManager instance = null;
//
//	public List<Candidate_Data> availableCandidates;
//	public List<CandidateBHV> candidates;
//	public List<Event_Data> events;
//	public List<CampaignProposal_Data> campProposals;
//	public List<DebateQuestion_Data> debateQuestions;
//	public int countEvents;
//	public int eventsPerCicle;
//
//	void Awake(){
//		if (instance == null)
//			instance = this;
//		else if (instance != this)
//			Destroy (gameObject);
//		DontDestroyOnLoad (gameObject);
//	}
//
// candidates => CandidateBHV
// 
//
//
//
//	// Use this for initialization
//	void Start () {
//		//Chama carrossel de escolha de candidato
//		//Ativa carrossel e passa a lista de cartas (BHV-availableCandidates) //para
//		//Update: if (answer == null) - opção 1
//		//Passa para o carrossel a função do GM por parâmetro
//		//Carrossel retorna a escolha do jogador
//		CandidateBHV choice = null; // =
//		candidates.Add(choice);
//		//Seta os adversários
//		//candidates.Add(others)
//		//Passa staff para o próximo carrossel
//		//candidates[0].avaiableStaff; (BHV)
//		//Recebe do carrossel o staff
//		//Repete n vezes - n staff
//
//		//Seta false o carrossel
//		//Seta true o evento
//		//Escolhe aleatório entre events - seta a referência no MBHV
//		//faz isso n vezes
//
//		//Setar false o evento
//		//Setar true o carrossel
//		//Passa as propostas de campanha para o carrossel (campProposals)
//		//Carrossel retorna a carta escolhida (CardBHV)
//		//Modifica alignment e recursos do candidato
//		//Dá o boost para o candidato - candidate[0].debateBoosts.Add(boost)
//		//Seta false o carrossel
//
//		//Opcional: faz ciclos de eventos e propostas
//
//
//		//DEBATE:
//		//Repete 3 x:
//		//Seta true na quadro de cartas
//		//Passa os adversários para o quadro
//		//Quadro retorna candidato escolhido; tirar esse candidato da lista de escolhas (quadro cuida disso)
//		//Seta quadro false
//		//Seta carrossel true
//		//Carrossel recebe lista de perguntas
//		//Carrossel retorna a pergunta escolhida (DebateQuestionBHV)
//		//Adversário faz a escolha entre as duas possíveis actions
//		//Seta carrossel false
//		//Seta evento true
//		//Apresenta para o jogador o evento subsquente do DebateQuestion: debateQuestions[rand].nextEvent2nd
//		//Adversário escolhe a resposta
//		//Apresenta resposta para o jogador: debateQuestions[rand].nextEvent3rd
//
//		//Simula escolha dos outros candidatos
//
//		//Para cada adversário que escolheu o player:
//		//Adversário escolhe a pergunta
//		//Apresenta pergunta para o jogador (event)
//		//Jogador escolhe e evento retorna debateQuestions[rand].nextEvent2nd para o adversário
//		//Adversário escolhe a resposta
//		//Apresenta debateQuestions[rand].nextEvent3rd para jogador
//		//Jogador escolhe
//		//Evento tem suas consequências sobre o jogador
//		//Mostra resultados do debate
//
//		//Ao final dos ciclos de debate:
//		//Mostrar gráfico de intenções de voto
//
//
//		//Repete tudo n vezes.
//
//
//
//
//
//
//
//	}
//
//	public void ChooseCandidate(){
//		StartCarrossel ((List<Card>) candidatos, EndCandidateChoice());
//	}
//	//GameManager.instance.EndCandidateChoice(card)
//
//	public void EndCandidateChoice(CardBHV card){
//		//atribui o candidato
//		StartCarrossel((List<CardBHV>)staff, EndStaffChoice());
//	}
//
//	public EndStaffChoice(CardBHV card){
//		//Atribui staff
//		CloseCarrossel();
//		Event.StartEvent(EventBHV event, SendNewEvent()); //mantém um contador que incrementa a cada chamada
//	}
//
//	public SendNewEvent(){
//		countEvents++;
//		if (countEvents < eventsPerCicle){
//			Event.StartEvent(EventBHV event, SendNewEvent());
//		} else {
//			//chama proposta de campanha
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//}
