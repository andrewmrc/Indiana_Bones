using UnityEngine;
using UnityEngine.EventSystems;
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
		//Deactivate the player
		player = GameObject.FindGameObjectWithTag ("Player");
		Player.Self.GetComponent<Player> ().enabled = false;

		//Disable Canvas UI interaction
		canvasUI = GameObject.FindGameObjectWithTag ("CanvasUI");
		//canvasUI.SetActive(false);
		canvasUI.GetComponent<CanvasGroup> ().alpha = 0;
		canvasUI.GetComponent<CanvasGroup> ().interactable = false;
		canvasUI.GetComponent<CanvasGroup> ().blocksRaycasts = false;

		//Set the event trigger
		EventTrigger trigger = gameObject.AddComponent<EventTrigger>() as EventTrigger;
		EventTrigger.Entry entry = new EventTrigger.Entry( );
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener( ( data ) => { OnPointerDownDelegate( (PointerEventData)data ); } );
		trigger.triggers.Add( entry );

		//Time.timeScale = 0f;
		textVisualized = this.transform.GetChild (0).GetChild(0).gameObject;
		textVisualized.GetComponent<Text> ().text = dialogues [count];
		textVisualized.GetComponent<Text> ().color = textColors [count];
		indianaChar.GetComponent<Image> ().sprite = indianaExpression [count];
		npcChar.GetComponent<Image> ().sprite = npcExpression [count];
		npcChar.GetComponent<Image> ().SetNativeSize ();
        indianaChar.GetComponent<Image>().SetNativeSize();


    }


    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown ("space")) {
			HandleDialogue ();
		}

	}


	public void HandleDialogue () {
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
			//canvasUI.SetActive(true);
			canvasUI.GetComponent<CanvasGroup> ().alpha = 1;
			canvasUI.GetComponent<CanvasGroup> ().interactable = true;
			canvasUI.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			//player.gameObject.GetComponent<Player> ().enabled = true;
			Player.Self.GetComponent<Player> ().enabled = true;
			if (DialoguesManager.Self.dialoguesActivated [20]) {
				GameController.Self.StartEndGameCoroutine ();
			}
		} 
	}


	//Go ahead with the dialogue touching the screen
	public void OnPointerDownDelegate( PointerEventData data )
	{
		HandleDialogue ();
	}

}
