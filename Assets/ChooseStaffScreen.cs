using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseStaffScreen : MonoBehaviour {

	public Text counterText;
	public Carousel carousel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int cur = carousel.chosenList.Count;
		int max = carousel.chooseCount + cur;
		if (carousel.chooseCount == 0){
			gameObject.SetActive (false);
		}
		counterText.text = cur + "/" + max;
	}
}
