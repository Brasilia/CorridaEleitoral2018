using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour {

	public Player player;
	public List<EventData> eventList; //Setar por referência no editor
	public Text eventText;
	public Transform options;
	public EventAnswerButton eventAnswerButtonPrefab;

	private EventData currentEvent;

	// Use this for initialization
	void Start () {
		LoadNewEvent ();
	}

	public void LoadNewEvent(){
		int size = eventList.Count;
		if (size > 0) {
			//Sorteia evento na lista
			int index = Random.Range (0, eventList.Count);
			currentEvent = eventList [index];
			eventText.text = currentEvent.contentText;
			//Para cada resposta, cria um botão correspondente
			foreach (EventAnswer answer in currentEvent.answers){
				EventAnswerButton button = Instantiate (eventAnswerButtonPrefab, options);
				button.SetButton (answer, this);
			}
			//Remove evento da lista (botões e texto já foram setados)
			eventList.Remove (currentEvent);
		} else {
			eventText.text = "Não há mais eventos disponíveis.";
			options.gameObject.SetActive (false);
		}
	}

	public void ClearEvent(){
		//Apaga todos os botões
		foreach (Transform child in options) {
			Destroy (child.gameObject);
		}
		//Carrega novo evento
		LoadNewEvent ();
	}
}
