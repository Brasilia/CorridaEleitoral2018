using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu]//(fileName = "Candidate", menuName = "Card/Candidate", order = 1)]
public class GameConfig : ScriptableObject {
	public SceneAsset sceneMenu;
	public SceneAsset scenePlay;

	public int gameTurns;
	public int candidatesCount;
	public int eventsPerTurn;
	public int staffCount;
	public int proposalWheelCount;
	public int debateQuestionWheelCount;
}
