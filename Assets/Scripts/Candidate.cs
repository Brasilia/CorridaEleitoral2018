using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Candidate{

	public string description;
	public Sprite image;
	//Name
	public string personName;

	//Trait...

	//Recursos
	public Resources resources;

	//Eventos associados ao personagem
	public List<Event_Data> events;

	//Eixos
	public Alignment alignment;

	//Propostas
	//public List<CampaignProposal_Data> campaignProposals = new List<CampaignProposal_Data>();

	public List<Staff_Data> avaiableStaff;
	public List<Staff_Data> hiredStaff = new List<Staff_Data>();

	//Execution ----------------
	public List<CandidateBHV> candidates; //Base de conhecimento sobre os outros candidatos
	public float voteIntentions;
	public List<Alignment> debateBoosts = new List<Alignment>();

	public Candidate(Candidate_Data card){
		description = card.description;
		image = card.image;
		personName = card.personName;
		resources = card.resources;
		events = card.events;
		alignment = card.alignment;
		avaiableStaff = card.avaiableStaff;
	}

	public bool HasBoosts(EventAction_Data eventChosen){
		foreach (Alignment boost in debateBoosts) {
			//Civil
			if ((boost.civil.escolasMilitares > 0) && (eventChosen.alignment.civil.escolasMilitares > 0))
				return true;
			if ((boost.civil.servicoMilitarObrigatorio > 0) && (eventChosen.alignment.civil.servicoMilitarObrigatorio > 0))
				return true;
			//Economic
			if ((boost.economic.bolsaFamilia > 0) && (eventChosen.alignment.economic.bolsaFamilia > 0))
				return true;
			if ((boost.economic.impostoDeRenda > 0) && (eventChosen.alignment.economic.impostoDeRenda > 0))
				return true;
			if ((boost.economic.previdencia > 0) && (eventChosen.alignment.economic.previdencia > 0))
				return true;
			if ((boost.economic.privatizacao > 0) && (eventChosen.alignment.economic.privatizacao > 0))
				return true;
			if ((boost.economic.salarioMinimo > 0) && (eventChosen.alignment.economic.salarioMinimo > 0))
				return true;
			//Societal
			if ((boost.societal.casamentoGay > 0) && (eventChosen.alignment.societal.casamentoGay > 0))
				return true;
			if ((boost.societal.ensinoReligiosoEscolas > 0) && (eventChosen.alignment.societal.ensinoReligiosoEscolas > 0))
				return true;
			if ((boost.societal.legalizacaoAborto > 0) && (eventChosen.alignment.societal.legalizacaoAborto > 0))
				return true;
			if ((boost.societal.legalizacaoDrogas > 0) && (eventChosen.alignment.societal.legalizacaoDrogas > 0))
				return true;
		}

		return false;
	}
}
