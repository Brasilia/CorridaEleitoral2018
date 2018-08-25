//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//public class EventAnswerButton : MonoBehaviour {
//
//	private Event eventPanel;
//	private EventAnswer answer; 
//	private Player player;
//
//	// Use this for initialization - Start é chamado no frame seguinte ao Instantiate
//	void Start () {
//		
//	}
//
//	public EventAnswer GetAnswer(){
//		return answer;
//	}
//
//	//Ajusta os parâmetros da resposta referente ao botão
//	public void SetButton(EventAnswer answer, Event eventPanel){
//		this.eventPanel = eventPanel; //painel de eventos a que pertence o botão
//		player = eventPanel.player;
//		this.answer = answer;
//		GetComponentInChildren<Text> ().text = answer.buttonText;
//
//		//Se o botão exigir recursos indisponíveis, ele deve ser marcado como disabled
//		if (player.cash + answer.cash < 0) {
//			GetComponent<Button> ().interactable = false;
//		}
//	}
//
//	public void OnClick(){
//		player.ChangeVariables (answer); //Modifica as variáveis do player 
//		eventPanel.ClearEvent (); //Solicita limpar o evento, pois o botão já foi pressionado
//		SendMessageUpwards("UpdateDisplayValues"); //Ver melhor forma de avisar o manager do Update do Display
//		Debug.Log ("Clicked");
//	}
//}
