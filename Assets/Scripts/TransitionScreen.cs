using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreen : MonoBehaviour {

	public delegate void Action();
	private Action MakeTransition = null;


	// Use this for initialization
	void Start () {
//		GetComponent<Animator> ().Play ("Screen Debate");
	}
	
	// Update is called once per frame
	void Update () {
//		if (GameManager.instance.state == GameManager.STATE.DebateQuestion) {
//			Debug.Log ("Should Play");
//			GetComponent<Animator> ().Play ("Screen Debate");
//
//		}
	}

	public void SetAndMakeTransition (Action a) {
		Debug.Log ("Should Play");
		gameObject.SetActive (true);
		MakeTransition += a;
	}

	private void RunAndUnsubscribeTransition (){
		if (MakeTransition != null){
			MakeTransition ();
			MakeTransition = null;
		}
		gameObject.SetActive (false);
	}

}
