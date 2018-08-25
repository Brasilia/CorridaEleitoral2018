using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldGame
{
	[System.Serializable]
	public class Player : MonoBehaviour
	{

		//Valores políticos
		public int economicEqualityMarkets;
		public int diplomaticNationGlobe;
		public int civilAuthorityLiberty;
		public int societalTraditionProgress;
		//Recursos
		public int cash;
		public int corruption;
		public int credibility;
		public int exposition;
		//Intenções de voto
		public float voteIntentions;

		// Informações pessoais
		public new string name;
		public Sprite image;
		public string partido;
		public string slogan;
		// Use this for initialization
		void Start ()
		{
			DontDestroyOnLoad (this.gameObject);
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}

		//Muda as variáveis do jogador, conforme a resposta a um evento
		public void ChangeVariables (EventAnswer answer)
		{
			economicEqualityMarkets += answer.economicEqualityMarkets;
			diplomaticNationGlobe += answer.diplomaticNationGlobe;
			civilAuthorityLiberty += answer.civilAuthorityLiberty;
			societalTraditionProgress += answer.societalTraditionProgress;
			cash += answer.cash;
			corruption += answer.corruption;
			credibility += answer.credibility;
			exposition += answer.exposition;
		}
	}
}