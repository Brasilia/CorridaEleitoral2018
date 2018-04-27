using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestFunc : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		GetMethod (Method1, 10, "oi");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public int Method1(int x, string s){
		Debug.Log ("Method1 " + x + " " + s);
		return 0;
	}

	public int Method2(int x, string s){
		Debug.Log ("Method2 " + x + " " + s);
		return 0;
	}

	public void GetMethod(System.Func<string, int, int> method, int i, string s){
		method (s, i);
		
	}
}
