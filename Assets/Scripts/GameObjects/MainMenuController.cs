using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public GameConfig gameConfig;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)){
			ExitGame ();
		}
	}

	public void StartGame(){
		SceneManager.LoadScene (gameConfig.scenePlayName);
	}

	public void HelpMenu(){
		
	}

	public void ExitGame() {
		Application.Quit ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
