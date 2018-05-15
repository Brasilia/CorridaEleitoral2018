using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesBHV : MonoBehaviour {

	public Text cashTxt;
	public Text corruptionTxt;
	public Text credibilityTxt;
	public Text visibilityTxt;

	public Slider economicSlider;
	public Slider civilSlider;
	public Slider societalSlider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetResourcesActive(){
		UpdateValues ();
		gameObject.SetActive (true);
	}

	public void UpdateValues(){
		cashTxt.text = GameManager.instance.candidates [0].resources.cash.ToString();
		corruptionTxt.text = GameManager.instance.candidates [0].resources.corruption.ToString();
		credibilityTxt.text = GameManager.instance.candidates [0].resources.credibility.ToString();
		visibilityTxt.text = GameManager.instance.candidates [0].resources.visibility.ToString();

		economicSlider.value = GameManager.instance.candidates [0].alignment.economic.value;
		civilSlider.value = GameManager.instance.candidates [0].alignment.civil.value;
		societalSlider.value = GameManager.instance.candidates [0].alignment.societal.value;
	}
}
