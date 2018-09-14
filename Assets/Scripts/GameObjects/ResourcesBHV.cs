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

	public Image candidateImage;

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
		Candidate player = GameManager.instance.candidates [0];
		// Resources
		cashTxt.text = player.resources.cash.ToString();
		corruptionTxt.text = player.resources.corruption.ToString();
		credibilityTxt.text = player.resources.credibility.ToString();
		visibilityTxt.text = player.resources.visibility.ToString();
		// Alignment
		economicSlider.value = player.alignment.economic.value;
		civilSlider.value = player.alignment.civil.value;
		societalSlider.value = player.alignment.societal.value;
		// Image
		candidateImage.sprite = player.image;
	}
}
