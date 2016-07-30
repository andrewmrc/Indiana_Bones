using UnityEngine;
using System.Collections;
using IndianaBones;

public class CanvasUI : MonoBehaviour {

	public static CanvasUI Instance;


	void OnLevelWasLoaded(){
		this.gameObject.SetActive (true);
		Debug.Log ("CANVAS UI ATTIVATO");
	}


	void Awake () {

		//Metodo per passare l'oggetto da una scena all'altra
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}


		/*
		//Metodo per passare l'oggetto da una scena all'altra
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			if (Player.Self.isDead) {
				Player.Self.isDead = false;
				Destroy (Instance);
				Debug.Log ("Il vecchio CanvasUI è stato distrutto");
				Instance = this;
			} else {
				Debug.Log ("Questo CanvasUI è stato salvato");

				Destroy (gameObject);
			}
		}*/

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
		

	public void CanvasActivation (bool active) {
		if (active) {
			this.gameObject.SetActive (false);
		} else {
			this.gameObject.SetActive (true);
		}
	}
}
