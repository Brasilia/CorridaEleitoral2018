using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public GameConfig gameConfig;
	private AsyncOperation sceneLoading = null;
	public Slider loadingProgress;
	public List<Button> buttonsToDeactivate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)){
			ExitGame ();
		}
		if (sceneLoading != null) {
			loadingProgress.value = sceneLoading.progress + 0.1f; // 0.1 gambiarra
		}
	}

	public void StartGame(){
		sceneLoading = SceneManager.LoadSceneAsync (gameConfig.scenePlayName);
		foreach (Button b in buttonsToDeactivate) {
			b.interactable = false;
			b.gameObject.SetActive (false);
		}
		loadingProgress.gameObject.SetActive (true);
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
