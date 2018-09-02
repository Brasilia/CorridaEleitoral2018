using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[CreateAssetMenu]//(fileName = "Candidate", menuName = "Card/Candidate", order = 1)]
public class GameConfig : ScriptableObject {
	#if UNITY_EDITOR
		public SceneAsset sceneMenu;
		public SceneAsset scenePlay;
	#endif
	public string sceneMenuName;
	public string scenePlayName;

	public int gameTurns;
	public int candidatesCount;
	public int eventsPerTurn;
	public int staffCount;
	public int proposalWheelCount;
	public int debateQuestionWheelCount;


	public void Update(){
		#if UNITY_EDITOR
			sceneMenuName = sceneMenu.name;
			scenePlayName = scenePlay.name;
		#endif
	}
}
