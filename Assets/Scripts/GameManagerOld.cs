using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerOld : MonoBehaviour {
	public Player player;
	public List<Player> otherCandidates;
	public List<ElectorGroup> electors;
	//public MainScreen mainScreen;

	public void Start(){
		DontDestroyOnLoad (this.gameObject);
		//mainScreen = GameObject.Find ("Panel Main Screen").GetComponent<MainScreen>();
	}

	public void UpdateIntentions(){
		otherCandidates.Add (player);
		//Reseta intenções de voto
		foreach(Player cand in otherCandidates){
			cand.voteIntentions = 0;
		}
		//Recalcula intenções
		foreach (ElectorGroup elec in electors){
			//Calcula intenções, partindo deste grupo de eleitores
			List<float> attractionFactors = new List<float>();
			float af;
			float totalAttraction = 0.0f;
			foreach(Player cand in otherCandidates){
				af = GetAttractionFactor (cand, elec);
				attractionFactors.Add (af);
				totalAttraction += af;
			}
			foreach (Player	cand in otherCandidates){
				float partialIntentions = 
					(float)elec.weight * attractionFactors[otherCandidates.IndexOf(cand)] / totalAttraction;
				Debug.Log ("total Attraction " + totalAttraction);
				cand.voteIntentions += partialIntentions; 
			}
		}
		otherCandidates.Remove (player);
		//mainScreen.UpdateVoteIntentionsDisplay ();
	}

	private float GetDistance (Player p1, Player p2){
		float distance1, distance2, d1, d2, d3, d4, d5;
		d1 = Mathf.Abs(p1.economicEqualityMarkets - p2.economicEqualityMarkets);
		d2 = Mathf.Abs(p1.diplomaticNationGlobe - p2.diplomaticNationGlobe);
		d3 = Mathf.Abs(p1.civilAuthorityLiberty - p2.civilAuthorityLiberty);
		d4 = Mathf.Abs(p1.societalTraditionProgress - p2.societalTraditionProgress);
		d5 = Mathf.Abs(p1.exposition - p2.exposition);
		distance1 = d1 + d2 + d3 + d4;
		distance2 = distance1 + d5*d5 / (p1.exposition*p1.exposition+200);// + Mathf.Sqrt(p2.exposition/(p1.exposition+100));
		return (distance2 + 0.001f);
	}

	//Euclidiana... não parece interessante
	private float GetEuclidianDistance(Player p1, Player p2){
		//Distância nas 5 dimensões
		float distance1, distance2, d1, d2, d3, d4, d5;
		d1 = p1.economicEqualityMarkets - p2.economicEqualityMarkets;
		d2 = p1.diplomaticNationGlobe - p2.diplomaticNationGlobe;
		d3 = p1.civilAuthorityLiberty - p2.civilAuthorityLiberty;
		d4 = p1.societalTraditionProgress - p2.societalTraditionProgress;
		d5 = Mathf.Abs(p1.exposition - p2.exposition);
		//d5 = 1.0f * p2.exposition / (p1.exposition+1); //teste
		distance1 = Mathf.Sqrt (d1 * d1 + d2 * d2 + d3 * d3 + d4 * d4);
		distance2 = Mathf.Sqrt (d1 * d1 + d2 * d2 + d3 * d3 + d4 * d4 + d5 * d5);
		return (distance1 + distance2 + 0.001f);// + distance*d5*d5;
	}

	private float GetAttractionFactor(Player p1, Player elec){
		float af = (float)(p1.credibility+100) / GetDistance (p1, elec);
		Debug.Log ("Attraction Factor " + af);
		return af;
	}
}
