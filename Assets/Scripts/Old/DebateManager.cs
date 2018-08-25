using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace OldGame {
	public class DebateManager : MonoBehaviour {
		//Atributos Select Screen 
		public List<Image> imgCandidate;
		public List<Text> txtCredibility;
		public List<Text> txtCorruption;
		public List<Text> txtVisibility;
		public List<Image> imgEconomic;
		public List<Image> imgCivil;
		public List<Image> imgSocietal;

		public Sprite equalityPrefab;
		public Sprite marketPrefab;
		public Sprite authorityPrefab;
		public Sprite libertyPrefab;
		public Sprite traditionPrefab;
		public Sprite progressPrefab;

		// Atributos pop-up screen
		public Image imgChoosenCandidate;
		public Text txtChoosenCredibility;
		public Text txtChoosenCorruption;
		public Text txtChoosenVisibility;
		public Image imgChoosenEconomic;
		public Image imgChoosenCivil;
		public Image imgChoosenSocietal;
		public Text txtChoosenName;
		public Text txtChoosenPartido;
		public Text txtChoosenSlogan;

		// Atributos Versus Screen
		public enum TURN {QUESTION, ANSWER, REPLY, REJOINDER} // possiveis estados do jogo
		private TURN turn;

		public Image imgEnemy;
		public Slider sliderEconomicEnemy;
		public Slider sliderCivilEnemy;
		public Slider sliderSocietalEnemy;
		public Text txtCredibilityEnemy;
		public Text txtCorruptionEnemy;
		public Text txtVisibilityEnemy;

		public Text txtEnemyActions;
		public Text txtEvidenceTheme;
		public Text txtCandidateSpeech;

		public GameObject selectAction;
		public GameObject selectAxis;
		public GameObject selectSubtheme;

		public Image imgPlayer;
		public Slider sliderEconomicPlayer;
		public Slider sliderCivilPlayer;
		public Slider sliderSocietalPlayer;
		public Text txtCredibilityPlayer;
		public Text txtCorruptionPlayer;
		public Text txtVisibilityPlayer;

		public Button btnAttack;
		public Button btnPosition;
		public Button btnAdvertising;
		public Button btnEnrolar;
		public Button btnProposal;
		public Button btnEnemyPosition;

		// Referências às janelas
		public GameObject selectScreen;
		public GameObject versusScreen;
		public GameObject popUp;

		// Referências aos objetos do jogo
		private GameManagerOld gameManager;
		private Player player;

		private int playerActions;
		private int enemyActions;
		private string theme;
		private string axis;

		private int candidateSelected;

		// Use this for initialization
		void Start () {
			selectScreen.SetActive (true);
			versusScreen.SetActive (false);
			popUp.SetActive (false);
			gameManager = GameObject.Find ("GameManager").GetComponent<GameManagerOld> ();
			player = GameObject.Find ("Player").GetComponent<Player> ();
			candidateSelected = -1;
			UpdateSelectScreen ();
		}

		// Update is called once per frame
		void Update () {

		}

		// Espera 3 segundos
		IEnumerator Wait3Seconds(){
			yield return new WaitForSeconds (3);
		}

		void UpdateSelectScreen(){
			int i;
			for (i = 0; i < gameManager.otherCandidates.Count; i++) {
				if (!gameManager.otherCandidates [i].name.Equals (player.name)) {		// Se o candidato não é o player
					imgCandidate[i].sprite = gameManager.otherCandidates[i].image;
					txtCredibility [i].text = gameManager.otherCandidates [i].credibility.ToString();
					txtCorruption [i].text = gameManager.otherCandidates [i].corruption.ToString();
					txtVisibility [i].text = gameManager.otherCandidates [i].exposition.ToString();

					if (gameManager.otherCandidates [i].economicEqualityMarkets > 0)
						imgEconomic [i].sprite = marketPrefab;
					else
						imgEconomic [i].sprite = equalityPrefab;

					if (gameManager.otherCandidates [i].civilAuthorityLiberty > 0)
						imgCivil [i].sprite = libertyPrefab;
					else
						imgCivil [i].sprite = authorityPrefab;

					if (gameManager.otherCandidates [i].societalTraditionProgress > 0)
						imgSocietal [i].sprite = progressPrefab;
					else
						imgSocietal [i].sprite = traditionPrefab;
				}
			}
		}

		// Verificar maneira de mesclar ambos as funções.

		void UpdateChoosenCandidate(){
			imgChoosenCandidate.sprite = gameManager.otherCandidates[candidateSelected].image;
			txtChoosenCredibility.text = gameManager.otherCandidates [candidateSelected].credibility.ToString();
			txtChoosenCorruption.text = gameManager.otherCandidates [candidateSelected].corruption.ToString();
			txtChoosenVisibility.text = gameManager.otherCandidates [candidateSelected].exposition.ToString();

			if (gameManager.otherCandidates [candidateSelected].economicEqualityMarkets > 0)
				imgChoosenEconomic .sprite = marketPrefab;
			else
				imgChoosenEconomic.sprite = equalityPrefab;

			if (gameManager.otherCandidates [candidateSelected].civilAuthorityLiberty > 0)
				imgChoosenCivil.sprite = libertyPrefab;
			else
				imgChoosenCivil.sprite = authorityPrefab;

			if (gameManager.otherCandidates [candidateSelected].societalTraditionProgress > 0)
				imgChoosenSocietal.sprite = progressPrefab;
			else
				imgChoosenSocietal.sprite = traditionPrefab;

			txtChoosenName.text = gameManager.otherCandidates [candidateSelected].name;
			txtChoosenPartido.text = gameManager.otherCandidates [candidateSelected].partido;
			txtChoosenSlogan.text = gameManager.otherCandidates [candidateSelected].slogan;
		}

		public void OnClickCandidate1(){
			if (!popUp.activeInHierarchy) {
				candidateSelected = 0;
				popUp.SetActive (true);
				UpdateChoosenCandidate ();
			}
		}

		public void OnClickCandidate2(){
			if (!popUp.activeInHierarchy) {
				candidateSelected = 1;
				popUp.SetActive (true);
				UpdateChoosenCandidate ();
			}
		}

		public void OnClickCandidate3(){
			if (!popUp.activeInHierarchy) {
				candidateSelected = 2;
				popUp.SetActive (true);
				UpdateChoosenCandidate ();
			}
		}

		public void OnClickCandidate4(){
			if (!popUp.activeInHierarchy) {
				candidateSelected = 2;
				popUp.SetActive (true);
				UpdateChoosenCandidate ();
			}
		}

		public void OnClickCandidate5(){
			if (!popUp.activeInHierarchy) {
				candidateSelected = 4;
				popUp.SetActive (true);
				UpdateChoosenCandidate ();
			}
		}

		public void OnClickCandidate6(){
			if (!popUp.activeInHierarchy) {
				candidateSelected = 5;
				popUp.SetActive (true);
				UpdateChoosenCandidate ();
			}
		}

		public void OnClickDebate(){
			versusScreen.SetActive (true);
			popUp.SetActive (false);
			selectScreen.SetActive (false);
			StartVersus ();
		}

		public void OnClickClosePopUp(){
			candidateSelected = -1;
			popUp.SetActive (false);
		}

		public void OnClickEconomic(){
			selectAxis.SetActive (false);
			selectSubtheme.SetActive (true);
			axis = "Economic";
		}

		public void OnClickCivil(){
			selectAxis.SetActive (false);
			selectSubtheme.SetActive (true);
			axis = "Civil";
		}

		public void OnClickSocietal(){
			selectAxis.SetActive (false);
			selectSubtheme.SetActive (true);
			axis = "Societal";
		}

		public void OnClickReturn(){
			selectSubtheme.SetActive (false);
			selectAxis.SetActive (true);
		}

		public void OnClickSubtheme1(){
			if (axis.Equals ("Economic"))
				theme = "Subtema econômico 1";
			else if (axis.Equals ("Civil"))
				theme = "Subtema civil 1";
			else
				theme = "Subtema societal 1";

			playerActions--;

			selectAxis.SetActive (false);
			selectAction.SetActive (true);
			selectSubtheme.SetActive (false);
		}

		public void OnClickSubtheme2(){
			if (axis.Equals ("Economic"))
				theme = "Subtema econômico 2";
			else if (axis.Equals ("Civil"))
				theme = "Subtema civil 2";
			else
				theme = "Subtema societal 2";

			playerActions--;

			selectAxis.SetActive (false);
			selectAction.SetActive (true);
			selectSubtheme.SetActive (false);
		}

		public void OnClickSubtheme3(){
			if (axis.Equals ("Economic"))
				theme = "Subtema econômico 3";
			else if (axis.Equals ("Civil"))
				theme = "Subtema civil 3";
			else
				theme = "Subtema societal 3";

			playerActions--;

			selectAxis.SetActive (false);
			selectAction.SetActive (true);
			selectSubtheme.SetActive (false);
		}

		public void OnClickAttack(){
			if (enemyActions == -1) {
				if (gameManager.otherCandidates [candidateSelected].credibility > 0)
					gameManager.otherCandidates [candidateSelected].credibility--;
				if (gameManager.otherCandidates [candidateSelected].exposition > player.exposition)
					player.exposition++;
				playerActions--;
				txtCandidateSpeech.text = "Atacou o inimigo.";
			} else {
				if(player.credibility > 0)
					player.credibility--;
				if (player.exposition > gameManager.otherCandidates[candidateSelected].exposition)
					gameManager.otherCandidates[candidateSelected].exposition++;
				enemyActions--;
				txtCandidateSpeech.text = "Adversário te atacou.";
				StartCoroutine (Wait3Seconds ());
			}

			VerifyTurn ();
		}

		public void OnClickPosition(){
			if (enemyActions == -1) {
				playerActions--;
				txtCandidateSpeech.text = "Se posicionou sobre o tema " + theme + ".";
			} else {
				enemyActions--;
				txtCandidateSpeech.text = "Adversário se posicionou sobre o tema " + theme + ".";
				StartCoroutine (Wait3Seconds ());
			}
			VerifyTurn ();
		}

		public void OnClickAdvertising(){
			if (enemyActions == -1) {
				player.credibility++;
				playerActions--;
				txtCandidateSpeech.text = "Fez propaganda de si mesmo.";
			} else {
				gameManager.otherCandidates[candidateSelected].credibility++;
				enemyActions--;
				txtCandidateSpeech.text = "Adversário fez propaganda de si mesmo.";
				StartCoroutine (Wait3Seconds ());
			}
			VerifyTurn ();
		}

		public void OnClickEnrolar(){
			if (enemyActions == -1) {
				playerActions--;
				txtCandidateSpeech.text = "Enrolou.";
			} else {
				enemyActions--;
				txtCandidateSpeech.text = "Adversário enrolou.";
				StartCoroutine (Wait3Seconds ());
			}
			VerifyTurn ();
		}

		public void OnClickProposal(){
			if (enemyActions == -1) {
				playerActions--;
				txtCandidateSpeech.text = "Fez uma proposta";
			} else {
				enemyActions--;
				txtCandidateSpeech.text = "Adversário fez uma proposta";
				StartCoroutine (Wait3Seconds ());
			}
			VerifyTurn ();
		}

		// "Start()" da VersusScreen
		void StartVersus(){
			Player enemy = gameManager.otherCandidates [candidateSelected];
			imgPlayer.sprite = player.image;
			sliderEconomicPlayer.value = player.economicEqualityMarkets;
			sliderCivilPlayer.value = player.civilAuthorityLiberty;
			sliderSocietalPlayer.value = player.societalTraditionProgress;
			txtCorruptionPlayer.text = player.corruption.ToString ();
			txtCredibilityPlayer.text = player.credibility.ToString ();
			txtVisibilityPlayer.text = player.exposition.ToString ();

			imgEnemy.sprite = enemy.image;
			sliderEconomicEnemy.value = enemy.economicEqualityMarkets;
			sliderCivilEnemy.value = enemy.civilAuthorityLiberty;
			sliderSocietalEnemy.value = enemy.societalTraditionProgress;
			txtCorruptionEnemy.text = enemy.corruption.ToString ();
			txtCredibilityEnemy.text = enemy.ToString ();
			txtVisibilityEnemy.text = enemy.ToString ();

			turn = TURN.QUESTION;
			playerActions = 2;
			enemyActions = -1;

			selectAxis.SetActive (true);
			selectAction.SetActive (false);
			selectSubtheme.SetActive (false);

			theme = "";
			axis = "";
		}

		void UpdateEnabledButtons(){
			btnEnemyPosition.enabled = false;
		}

		// Verifica fim de turno
		void VerifyTurn(){
			// Se o turno do player acabou
			if (playerActions == 0) {
				playerActions = -1;
				if (turn == TURN.QUESTION) {
					turn = TURN.ANSWER;
					enemyActions = 4;
				} else if (turn == TURN.ANSWER) {
					turn = TURN.REPLY;
					enemyActions = 2;
				} else if (turn == TURN.REPLY) {
					turn = TURN.REJOINDER;
					enemyActions = 2;
				} else if (turn == TURN.REJOINDER) {
					UpdateSelectScreen ();
					versusScreen.SetActive (false);
					selectScreen.SetActive (true);
					candidateSelected = -1;
				}
			} else if(enemyActions == 0){	// Se o turno do enemy acabou	
				enemyActions = -1;
				selectAxis.SetActive (true);
				selectAction.SetActive (false);
				selectSubtheme.SetActive (false);
				if (turn == TURN.QUESTION) {
					turn = TURN.ANSWER;
					playerActions = 4;
				} else if (turn == TURN.ANSWER) {
					turn = TURN.REPLY;
					playerActions = 2;
				} else if (turn == TURN.REPLY) {
					turn = TURN.REJOINDER;
					playerActions = 2;
				} else if (turn == TURN.REJOINDER) {
					UpdateSelectScreen ();
					versusScreen.SetActive (false);
					selectScreen.SetActive (true);
					candidateSelected = -1;
				}
			}
		}
	}
}
