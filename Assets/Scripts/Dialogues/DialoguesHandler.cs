using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using IndianaBones;

public class DialoguesHandler : MonoBehaviour {

	public List<string> dialogues = new List<string>();
	public List<Sprite> indianaExpression = new List<Sprite>();
	public List<Sprite> npcExpression = new List<Sprite>();
	public List<Color32> textColors = new List<Color32>();

	public GameObject textVisualized;
	public GameObject indianaChar;
	public GameObject npcChar;

	public int count = 0;

	public GameObject player;
	public GameObject canvasUI;

	void Awake (){

	}


	// Use this for initialization
	void Start () {
		//Time.timeScale = 0f;
		textVisualized = this.transform.GetChild (0).GetChild(0).gameObject;
		textVisualized.GetComponent<Text> ().text = dialogues [count];
		textVisualized.GetComponent<Text> ().color = textColors [count];
		indianaChar.GetComponent<Image> ().sprite = indianaExpression [count];
		npcChar.GetComponent<Image> ().sprite = npcExpression [count];
		npcChar.GetComponent<Image> ().SetNativeSize ();
        indianaChar.GetComponent<Image>().SetNativeSize();

		player = GameObject.FindGameObjectWithTag ("Player");
		canvasUI = GameObject.FindGameObjectWithTag ("CanvasUI");
		canvasUI.SetActive(false);
		Player.Self.GetComponent<Player> ().enabled = false;
		//player.gameObject.GetComponent<Player> ().enabled = false;
    }


    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown ("space")) {
			if (count < dialogues.Count-1) { 
				Debug.Log ("Stamp dialogo!");
				count++;
				textVisualized.GetComponent<Text> ().text = dialogues [count];
				textVisualized.GetComponent<Text> ().color = textColors [count];
				indianaChar.GetComponent<Image> ().sprite = indianaExpression [count];
				npcChar.GetComponent<Image> ().sprite = npcExpression [count];
			}else {
				Debug.Log ("Chiudi dialogo!");
				this.gameObject.SetActive (false);
				canvasUI.SetActive(true);
				//player.gameObject.GetComponent<Player> ().enabled = true;
				Player.Self.GetComponent<Player> ().enabled = true;
				if (DialoguesManager.Self.dialoguesActivated [20]) {
					GameController.Self.StartEndGameCoroutine ();
				}
			} 
		}

	}


}
