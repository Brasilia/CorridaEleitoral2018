using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignProposalBHV : EventBHV {
	//Database -----------------
	//Eixos
	public Alignment debateBoost;

	public Slider economical;
	public Slider civil;
	public Slider societal;
	//Execution ----------------


//	public void LoadData(CampaignProposal_Data card){
//		base.LoadData (card);
//		debateBoost = card.debateBoost;
//	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Load (CampaignProposal_Data cp) {
		base.Load (cp);
		economical.value = cp.actionAccept.alignment.economic.value;
		civil.value = cp.actionAccept.alignment.civil.value;
		societal.value = cp.actionAccept.alignment.societal.value;
		debateBoost = cp.debateBoost;
	}
}
