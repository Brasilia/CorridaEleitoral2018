using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameConfig))]
public class GameConfigEditor : Editor {

	public override void OnInspectorGUI(){
		DrawDefaultInspector ();
		GameConfig config = (GameConfig)target;
		if (GUILayout.Button ("Update")){
			config.Update ();
		}
	}
		
}
