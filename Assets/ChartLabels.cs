using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChartLabels : MonoBehaviour {

	public GameObject labelPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddCandidate(Candidate cand, Color color) {
		GameObject label = (GameObject)Instantiate (labelPrefab);
		label.transform.SetParent (transform);
		label.transform.localScale = new Vector2 (1.0f, 1.0f);
		label.transform.localPosition = Vector3.zero;
		label.GetComponent<Image> ().color = color;
		label.transform.GetChild (0).GetComponent<Image> ().sprite = cand.image;
	}
}
