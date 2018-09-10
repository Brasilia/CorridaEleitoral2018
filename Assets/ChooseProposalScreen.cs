using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseProposalScreen : MonoBehaviour {

	// Usar delegates e criar um listener para este script saber que o game manager mudou de estado
	// será necessário criar uma função no game manager para fazer as mudanças de estado

	public Carousel carousel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!carousel.gameObject.activeInHierarchy){
			gameObject.SetActive (false);
		}
	}
}
