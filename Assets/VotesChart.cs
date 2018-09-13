using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotesChart : MonoBehaviour {

	public GraphControl chart;
	private AudioSource audioSource;

	void Awake () {
		audioSource = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activate (AudioClip clip) {
		gameObject.SetActive (true);
		audioSource.clip = clip;
		audioSource.Play ();
	}

	public void Deactivate () {
		gameObject.GetComponent<TransitionScreen> ().RunAndUnsubscribeTransition ();
	}
}
