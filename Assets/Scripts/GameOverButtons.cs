﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverButtons : MonoBehaviour {

	public GameObject eventSystem;

	// Use this for initialization
	void Start () {
		eventSystem = GameObject.Find ("EventSystem");
		eventSystem.GetComponent<EventSystem> ().firstSelectedGameObject = this.transform.GetChild (1).gameObject;
		this.gameObject.transform.parent.gameObject.SetActive (false);
		//this.gameObject.SetActive (false);

	}
	
	public void ExitToMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void reloadScene ()
	{
		Scene thisScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (thisScene.name);
	}
}