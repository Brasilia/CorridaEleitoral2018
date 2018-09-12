using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip audioAccept;
	public AudioClip audioDecline;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)){
			GameManager.instance.QuitGame ();
		}
	}

	public void Accept () {
		audioSource.clip = audioAccept;
		audioSource.Play ();
		GameManager.instance.QuitGame ();
	}

	public void Decline () {
		audioSource.clip = audioDecline;
		audioSource.Play ();
		gameObject.SetActive (false);
	}


}
