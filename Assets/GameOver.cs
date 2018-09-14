using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	private AudioSource audioSource = null;
	public AudioClip clipWin;
	public AudioClip clipLose;
	private Candidate player;
	private Candidate winner;
	public Image winnerImage;
	public AudioSource mainGameAudioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		player = GameManager.instance.candidates [0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Setup () {
		winner = GameManager.instance.leadingCandidate;
		if (winner == player) {
			audioSource.clip = clipWin;
		} else {
			audioSource.clip = clipLose;
		}
		winnerImage.sprite = winner.image;
	}

	void Play() {
		mainGameAudioSource.mute = true;
		audioSource.Play ();
	}
		
}
