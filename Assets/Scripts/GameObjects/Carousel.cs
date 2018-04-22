using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour {

	private List<Card> cards;
	public List <GameObject> panels;
	public GameObject pivotCard;

	private int selected = 1;

	// Use this for initialization
	void Start () {
		foreach (GameObject p in panels) 
			cards.Add (p.GetComponent<Card> ());
	}
	
	// Update is called once per frame
	void Update () {
		
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
