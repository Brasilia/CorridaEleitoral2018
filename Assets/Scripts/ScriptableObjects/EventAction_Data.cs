using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventAction_Data {
	public string description;
	//public Person_Data target;
	public Resources resources;
	public Alignment alignment;
	public Event_Data nextEvent;
	//SpecialConsequence...
	public enum SpecialConsequence {
		ADD_CORRUPTION, //talvez não seja necessário isto para a corrupção: deve bastar adicionar uma carta genérica de "corruption" a todos os adversários, na quantidade da corrupção total menos a corrupção conhecida
        GET_ATTACKED,
		GET_BOOSTED,
		COUNT
	}
	public List<SpecialConsequence> specialConsequences = new List<SpecialConsequence>();

	protected void GetAttacked(Candidate cand) {
		cand.resources.credibility -= cand.resources.corruption / 2;
	}

	protected void GetBoosted(Candidate cand) {
		Alignment a1 = cand.alignment;
		Alignment a2 = this.alignment;
		int result = a1.economic.bolsaFamilia * a2.economic.bolsaFamilia +
		             a1.economic.impostoDeRenda * a2.economic.impostoDeRenda +
		             a1.economic.previdencia * a2.economic.previdencia +
		             a1.economic.privatizacao * a2.economic.privatizacao +
		             a1.economic.salarioMinimo * a2.economic.salarioMinimo +
		             a1.civil.escolasMilitares * a2.civil.escolasMilitares +
		             a1.civil.servicoMilitarObrigatorio * a2.civil.escolasMilitares +
		             a1.societal.casamentoGay * a2.societal.casamentoGay +
		             a1.societal.ensinoReligiosoEscolas * a2.societal.ensinoReligiosoEscolas +
		             a1.societal.legalizacaoAborto * a2.societal.legalizacaoAborto +
		             a1.societal.legalizacaoDrogas * a2.societal.legalizacaoDrogas;
		if (result != 0) {
			cand.resources.credibility += 5;
		}
	}

	public void RunSpecialConsequences (Candidate cand) {
		Debug.Log ("Running Special Consequences to " + cand.personName);
		if (specialConsequences.Contains(SpecialConsequence.GET_ATTACKED)){
			GetAttacked (cand);
			Debug.Log ("ATTACK to " + cand.personName);
		}
		if (specialConsequences.Contains(SpecialConsequence.GET_BOOSTED)) {
			GetBoosted (cand);
			Debug.Log ("BOOST to " + cand.personName);
		}
	}
}
