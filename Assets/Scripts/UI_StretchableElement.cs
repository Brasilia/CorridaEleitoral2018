using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RectTransform))]
public class UI_StretchableElement : MonoBehaviour {

	public bool keepAspectRatio = true;

	private RectTransform rectTransform = null;

	void Awake () {
		rectTransform = GetComponent <RectTransform> ();
		float a = rectTransform.rect.height;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FitToParent(RectTransform parent){
		transform.SetParent (parent.transform);
		//parent.anchoredPosition = Vector2.zero;
		transform.localPosition = Vector2.zero;
		// Calculate Scale
		float scaleX = parent.rect.width;
		scaleX /= rectTransform.rect.width;
		float scaleY = parent.rect.height;
		scaleY /= rectTransform.rect.height;
		Vector2 scale = new Vector2 (parent.rect.width / rectTransform.rect.width, parent.rect.height / rectTransform.rect.height);
		if (keepAspectRatio && scale.x != scale.y){
			float fScale = Mathf.Min (scale.x, scale.y);
			scale.x = fScale;
			scale.y = fScale;
		}
		transform.localScale = scale;
		//transform.localScale = new Vector2 (0.50f, 0.50f);
	}

	public void FitToParent(Transform parent){
		transform.SetParent (parent.transform);
		transform.localPosition = Vector2.zero;
		//transform.localScale = Vector2.one;
		transform.localScale = new Vector2 (0.50f, 0.50f);
		//GetComponent<RectTransform> ().sizeDelta = Vector2.zero;
	}
}
