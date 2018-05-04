using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public enum STATE {ChooseCandidate, ChooseStaff, Event, Proposal, ChooseOpponent, DebateQuestion, DebateReply, DebateRejoinder, DebateResults} // possiveis estados do jogo

	private STATE state;

	public static GameManager instance = null;

	public GameObject carousel;
	public GameObject chooseOptions;
	public GameObject chooseBool;

	public List<Candidate_Data> availableCandidates;	// Lista de scriptable objects de candidatos
	public List<Candidate> candidates = new List<Candidate>();				//
	public List<Event_Data> events;
	public List<CampaignProposal_Data> campProposals;
	public List<DebateQuestion_Data> debateQuestions;
	public int countEvents;
	public int eventsPerCicle;

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
		chooseOptions.GetComponent<CandidateSelection> ().UpdateSelectScreen (availableCandidates);
	}

	public void CandidateChoosen(int index){
		chooseOptions.SetActive (false);
		int i;
		Candidate cand = null;
		cand = new Candidate (availableCandidates[index]);
		candidates.Add (cand);
		for (i = 0; i < availableCandidates.Count; i++) {
			if (i != index) {
				cand = new Candidate(availableCandidates [i]);
				candidates.Add (cand);
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
