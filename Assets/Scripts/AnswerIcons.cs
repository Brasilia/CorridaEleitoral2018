//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//public class AnswerIcons : MonoBehaviour {
//
//	//Botão em que serão mostrados os ícones
//	public EventAnswerButton parentButton;
//	private EventAnswer answer;
//
//	//Prefab dos ícones
//	public Image iconFramePrefab;
//
//
//	//Valores políticos
//	public Sprite iconEquality;
//	public Sprite iconMarket;
//	public Sprite iconNation;
//	public Sprite iconGlobe;
//	public Sprite iconAuthority;
//	public Sprite iconLiberty;
//	public Sprite iconTradition;
//	public Sprite iconProgress;
//	//Recursos
//	public Sprite iconCash;
//	public Sprite iconCorruption;
//	public Sprite iconCredibility;
//	public Sprite iconExposition;
//
//
//	// Use this for initialization
//	void Start () {
//		answer = parentButton.GetAnswer ();
//		//Insere ícones de posicionamento político
//		InsertPoliticalIcon(answer.economicEqualityMarkets, iconEquality, iconMarket);
//		InsertPoliticalIcon(answer.diplomaticNationGlobe, iconNation, iconGlobe);
//		InsertPoliticalIcon(answer.civilAuthorityLiberty, iconAuthority, iconLiberty);
//		InsertPoliticalIcon(answer.societalTraditionProgress, iconTradition, iconProgress);
//		//Insere ícones de recursos
//		InsertResourceIcon(answer.cash, iconCash);
//		InsertResourceIcon(-answer.corruption, iconCorruption); //passa corruption negativa pois tem relação oposta de benefício
//		InsertResourceIcon(answer.credibility, iconCredibility);
//		InsertResourceIcon(answer.exposition, iconExposition);
//
//	}
//
//	//Insere ícones de recurso à lista de ícones da resposta
//	void InsertResourceIcon (int variation, Sprite resSprite){
//		if (variation != 0){ //Só faz sentido instanciar ícone se for diferente de 0
//			Color frameColor;
//			if (variation < 0){
//				frameColor = Color.red; // Altera a cor da moldura indicando efeito NEGATIVO
//			} else {
//				frameColor = Color.green; // Altera a cor da moldura indicando efeito POSITIVO
//			}
//			int iconCount = Mathf.Abs (variation);
//			for (int i = 0; i < iconCount; i++){
//				Image iconFrame = Instantiate (iconFramePrefab, transform);
//				iconFrame.color = frameColor;
//				iconFrame.transform.GetChild (0).GetComponent<Image> ().sprite = resSprite;
//			}
//		}
//	}
//
//	//Insere ícones de orientação política à lista de ícones da resposta
//	void InsertPoliticalIcon (int variation, Sprite resSpriteLeft, Sprite resSpriteRight){
//		if (variation != 0){ //Só faz sentido instanciar ícone se for diferente de 0
//			Sprite sprite = null;
//			if (variation < 0){
//				sprite = resSpriteLeft;
//			} else {
//				sprite = resSpriteRight;
//			}
//			int iconCount = (int)Mathf.Ceil (Mathf.Abs (variation) / 10.0f);
//			for (int i = 0; i < iconCount; i++){
//				Image iconFrame = Instantiate (iconFramePrefab, transform);
//				iconFrame.transform.GetChild (0).GetComponent<Image> ().sprite = sprite;
//				//Destroy (iconFrame);
//			}
//		}
//	}
//
//
//}
