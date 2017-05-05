using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.VR;


public class PauseScript : MonoBehaviour {

	public GameObject BeanGuy, PauseMenu;
	public Button MainMenuButton;
	public Button ResumeButton;
	public Canvas Instructions;
	public Text pausedText;

	// Use this for initialization
	void Start () {

		MainMenuButton.GetComponent<Button> ();
		MainMenuButton.onClick.AddListener (GoToMainMenu);
		ResumeButton.GetComponent<Button> ();
		ResumeButton.onClick.AddListener (ResumeGame);
	}

	// Update is called once per frame
	void Update () {
		//When Paused we want the canvas with the instuctions to disappear and have something that appears over it saying the game is paused
		Instructions.enabled = false;
		pausedText.GetComponent<Text> ().text = "Paused";
		//We also want the bean guy script to be disabled
		if (BeanGuy.GetComponent<TutorialController>() != null)
			BeanGuy.GetComponent<TutorialController> ().enabled = false;
		if (BeanGuy.GetComponent<SinglePlayerController>() != null)
			BeanGuy.GetComponent<SinglePlayerController> ().enabled = false;
	}

	void GoToMainMenu(){
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
	void ResumeGame(){
		pausedText.GetComponent<Text> ().text = "";
		Instructions.enabled = true;
		if (BeanGuy.GetComponent<TutorialController>() != null)
			BeanGuy.GetComponent<TutorialController> ().enabled = true;
		if (BeanGuy.GetComponent<SinglePlayerController>() != null)
			BeanGuy.GetComponent<SinglePlayerController> ().enabled = true;
		PauseMenu.SetActive (false);
	}
}
