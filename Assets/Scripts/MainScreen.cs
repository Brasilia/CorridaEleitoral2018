using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour {

	public GameManager gm; //tirar daqui

	public Player player;
	//Sliders
	public Slider sliderEconomic;
	public Slider sliderDiplomatic;
	public Slider sliderCivil;
	public Slider sliderSocietal;
	//Resource numbers texts
	public Text moneyText;
	public Text corruptionText;
	public Text credibilityText;
	public Text expositionText;
	//Vote intentions
	public Text voteIntentionsText;

	// Use this for initialization
	void Start () {
		UpdateDisplayValues ();
	}

	// Update is called once per frame
	void Update () {

	}

	//Atualiza os valores das variáveis do jogador na tela principal
	public void UpdateDisplayValues(){
		//Sliders - Puxa ou empurra? (Negativo ou Positivo?)
		sliderEconomic.value = player.economicEqualityMarkets;
		print ("slider");
		sliderDiplomatic.value = player.diplomaticNationGlobe;
		sliderCivil.value = player.civilAuthorityLiberty;
		sliderSocietal.value = player.societalTraditionProgress;
		//Resources
		moneyText.text = player.cash.ToString();
		corruptionText.text = player.corruption.ToString();
		credibilityText.text = player.credibility.ToString();
		expositionText.text = player.exposition.ToString();
		Debug.Log ("Display values updated");
		gm.UpdateIntentions ();
		UpdateVoteIntentionsDisplay ();
	}

	public void UpdateVoteIntentionsDisplay(){
		voteIntentionsText.text = player.voteIntentions.ToString("00.0") + "%";
	}

	public void OnClickDebate(){
		//gm.mainScreen = null;
		SceneManager.LoadScene("debate");
	}
}
