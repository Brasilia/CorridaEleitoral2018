using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour {

	private List<GameObject> cards;
	//public List <GameObject> panels;
	public GameObject pivotCard;
	public List<GameObject> cardsToTest;

	public RectTransform scrollPanel;	// ScrollPanel
	public RectTransform center;	// CenterToCompare
	public RectTransform prefabCardPanel;	// Prefab do Card Panel. Usado para instanciar de acordo com a quantidade de cartas.

	private float[] distance;		// array de distâncias de cada panel
	private bool dragging = false;
	private int selected;
	public List<RectTransform> panels;
	private float offsetButtons = 700f;

	private GameObject gameManager;

	// Use this for initialization
	void Start () {
		int i;
		SetCarouselActive (cardsToTest);
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
					print ("selected = " + i);
					break;
				}
			}

			if (!dragging) 	// Se não está mais puxando, faz a interpolação para a posição certa
				LerpToSelectedPanel ((int)-offsetButtons * selected);
		}
	}

	public void SetCarouselActive(List<GameObject> cards){
		int i;
		float deltaX = 0;
		RectTransform panel;

		distance = new float[cards.Count];
		this.cards = cards;
		foreach (GameObject card in this.cards) {		// Instancia cada card a aparecer no carousel
			panel = Instantiate (prefabCardPanel, scrollPanel);
			card.transform.SetParent (panel.transform);
			panel.transform.position = new Vector2 (panel.transform.position.x + deltaX, transform.position.y);
			deltaX += offsetButtons;
			panels.Add (panel);
		}
		gameObject.SetActive (true);
	}

	void LerpToSelectedPanel(int position){
		float newX = Mathf.Lerp (scrollPanel.anchoredPosition.x, position, Time.deltaTime * 10f);	// Suaviza o panel pra posição certa
		Vector2 newPosition = new Vector2(newX, scrollPanel.anchoredPosition.y);	// Nova posição do panel
		scrollPanel.anchoredPosition = newPosition;		// Seta a posição

	}

	public void BeginDrag(){
		dragging = true;
	}

	public void EndDrag(){
		dragging = false;
	}

	public void OnClick(){
		cards.Clear ();
		gameObject.SetActive (false);
		// chama função seguinte retornando selected
	}

	public void OnDrag(){
		// center: (19.74, 327.89)
		// right: (-636, 327.89)
		// left: (673, 327.89)
		// double centerRightMiddlePoint = 655.74, centerLeftMiddlePoint = 653.26;
		double centerRightMiddlePoint = 327.87, centerLeftMiddlePoint = 326.63;
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
