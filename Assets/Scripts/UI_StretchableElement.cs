using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StretchableElement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FitToParent(RectTransform parent){
		transform.SetParent (parent.transform);
		parent.anchoredPosition = Vector2.zero;
		transform.localPosition = Vector2.zero;
		transform.localScale = Vector2.one;
		GetComponent<RectTransform> ().sizeDelta = Vector2.zero;
	}

	public void FitToParent(Transform parent){
		transform.SetParent (parent.transform);
		transform.localPosition = Vector2.zero;
		transform.localScale = Vector2.one;
		GetComponent<RectTransform> ().sizeDelta = Vector2.zero;
	}
}
