using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour {

	private List<GameObject> cards = new List<GameObject>();
	//public List <GameObject> panels;
	public GameObject pivotCard;
	public List<GameObject> cardsToTest;

	public RectTransform scrollPanel;	// ScrollPanel
	public RectTransform center;	// CenterToCompare
	public RectTransform prefabCardPanel;	// Prefab do Card Panel. Usado para instanciar de acordo com a quantidade de cartas.

	private float[] distance;		// array de distâncias de cada panel
	private bool dragging = false;
	public int chooseCount;
	public List<int> chosenList = new List<int>();
	private int selected; 
	public List<RectTransform> panels;
	private float offsetButtons = 700f;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		//SetCarouselActive (cardsToTest);
	}
	
	// Update is called once per frame
	void Update () {
		if (panels.Count > 0) {
			int i;
			float minDistance;

			for (i = 0; i < panels.Count; i++) 	// Distância de cada panel em relação ao centro
				distance [i] = Mathf.Abs (center.transform.position.x - panels [i].transform.position.x);

			minDistance = Mathf.Min (distance);
			for (i = 0; i < panels.Count; i++) {		// Recupera o índice do panel selecionado
				if (minDistance == distance [i]) {
					selected = i;
					//print ("selected = " + i);
					break;
				}
			}

			if (!dragging) 	// Se não está mais puxando, faz a interpolação para a posição certa
				LerpToSelectedPanel ((int)-offsetButtons * selected);
		}
	}

	// Ativa o widget.
	public void SetCarouselActive(List<GameObject> cards, int count = 1){
		this.cards.Clear ();
		this.cards = cards;
		chooseCount = count;
		Debug.Log ("Chamando Organize()");
		Organize ();
		gameObject.SetActive (true);
	}

	private void Organize(){
		float deltaX = 0;

		distance = new float[cards.Count];
		Debug.Log ("Staff count: "+cards.Count);
		foreach (GameObject card in cards) {		// Instancia cada card a aparecer no carousel
			Debug.Log("carta "+ card);
			RectTransform panel;
			panel = Instantiate (prefabCardPanel, scrollPanel);
			card.transform.SetParent (panel.transform);
			panel.transform.localPosition = new Vector2 (0 + deltaX, 0);
			card.transform.localPosition = Vector2.zero;
			deltaX += offsetButtons;
			panels.Add (panel);
		}
	}



	// Suaviza o panel selecionado para a posição certa.
	void LerpToSelectedPanel(int position){
		float newX = Mathf.Lerp (scrollPanel.anchoredPosition.x, position, Time.deltaTime * 10f);	// Suaviza o panel pra posição certa
		Vector2 newPosition = new Vector2(newX, scrollPanel.anchoredPosition.y);	// Nova posição do panel
		scrollPanel.anchoredPosition = newPosition;		// Seta a posição

	}

	public void BeginDrag(){
		Debug.Log ("BeginDrag");
		dragging = true;
	}

	public void EndDrag(){
		dragging = false;
		Debug.Log ("EndDrag");
	}

	public void OnClick(){
		
		// chama função seguinte retornando selected
		chosenList.Add(selected);
		cards.RemoveAt (selected);
		RectTransform removedPanel = panels [selected];
		Destroy (panels [selected].gameObject);
		panels.RemoveAt (selected);
		Debug.Log ("Click!");
		chooseCount--;
		if(chooseCount == 0){
			gameObject.SetActive (false);
			GameManager.instance.ReturnControl ();
		}
	}

	public void OnDrag(){
		// center: (19.74, 327.89)
		// right: (-636, 327.89)
		// left: (673, 327.89)
		// double centerRightMiddlePoint = 655.74, centerLeftMiddlePoint = 653.26;
		//double centerRightMiddlePoint = 327.87, centerLeftMiddlePoint = 326.63;
		float x, y;
		x = Input.mousePosition.x;

		if (x > 673)
			x = 673;
		if (x < -673)
			x = -673;
		y = pivotCard.transform.position.y;
		pivotCard.transform.position = new Vector2(x, y);
		//transform.SetAsLastSibling ();
	}
}
