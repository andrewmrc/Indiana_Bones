using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IndianaBones
{
	public class AbilityHandler : MonoBehaviour {

		// Singleton Implementation
		protected static AbilityHandler _self;
		public static AbilityHandler Self
		{
			get
			{
				if (_self == null)
					_self = FindObjectOfType(typeof(AbilityHandler)) as AbilityHandler;
				return _self;
			}
		}

		[Header("Restore Ability")]
		[Space(10)]

		public int manaCostRestore = 1;
		public int healthQuantity = 1;


		[Header("Fever Ability")]
		[Space(10)]
		public int manaCostFever = 1;


		[Header("Escape Rope Ability")]
		[Space(10)]
		public int manaCostEscape = 1;

		int playerXPos;
		int playerYPos;



		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}


		//Restore Health Ability
		public void Restore()
		{
			if (Player.Self.currentMana >= manaCostRestore) {
				if(Player.Self.currentLife < Player.Self.startingLife){
					Debug.Log ("Restore ability activated");
					Player.Self.StartCoroutine("FeedbackHealth");
					Player.Self.currentLife += healthQuantity;
					Player.Self.currentMana -= manaCostRestore;
					if (Player.Self.currentLife > Player.Self.startingLife) {
						Player.Self.currentLife = Player.Self.startingLife;
					}

					if (Player.Self.currentMana < 0) {
						Player.Self.currentMana = 0;
					}
					GameController.Self.PassTurn ();
					Player.Self.ResetPlayerVar ();
				} else {
					Debug.Log ("La vita è già al massimo!");
				}
			} else {
				Debug.Log ("Non basta il mana per l'attivazione");
				//Inserire feedback

			}
		}


		//Escape Rope Ability
		public void EscapeRope()
		{
			if (Player.Self.currentMana >= manaCostEscape) {
				Debug.Log ("Escape Rope ability activated");

				Player.Self.currentMana -= manaCostEscape;
				if (Player.Self.currentMana < 0) {
					Player.Self.currentMana = 0;
				}
				GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
				/*
				playerXPos = (int)Player.Self.transform.position.x;
				playerYPos = (int)Player.Self.transform.position.y;
				Player.Self.elementi.scacchiera [playerXPos, playerYPos].status = 0;
				Player.Self.transform.position = GameController.Self.startPoint.transform.position;
				Player.Self.targetTr = GameController.Self.startPoint.transform; //Player.Self.elementi.scacchiera [(int)GameController.Self.startPoint.transform.position.x, (int)GameController.Self.startPoint.transform.position.y].transform;
				Player.Self.xOld = (int)GameController.Self.startPoint.transform.position.x;
				Player.Self.xOld = (int)GameController.Self.startPoint.transform.position.y;
				Player.Self.xPosition = (int)GameController.Self.startPoint.transform.position.x;
				Player.Self.yPosition = (int)GameController.Self.startPoint.transform.position.y;
				Player.Self.elementi.scacchiera [Player.Self.xPosition, Player.Self.yPosition].status = 4;
				*/
				if (Player.Self.fromLevelInf) {
					Debug.Log ("fromlevelinf");
					//xPosition = (int)this.transform.position.x;
					//yPosition = (int)this.transform.position.y;
					this.transform.position = GameController.Self.startPoint.transform.position;
					Player.Self.targetTr = GameController.Self.startPoint.transform;
					//elementi.scacchiera [xPosition, yPosition].status = 0;
					Player.Self.xOld = (int)GameController.Self.startPoint.transform.position.x;
					Player.Self.xOld = (int)GameController.Self.startPoint.transform.position.y;
					Player.Self.xPosition = (int)GameController.Self.startPoint.transform.position.x;
					Player.Self.yPosition = (int)GameController.Self.startPoint.transform.position.y;
					Player.Self.elementi.scacchiera [Player.Self.xPosition, Player.Self.yPosition].status = 4;
				} else if (Player.Self.fromLevelSup) {
					Debug.Log ("fromlevelsup");

					//xPosition = (int)this.transform.position.x;
					//yPosition = (int)this.transform.position.y;
					this.transform.position = GameController.Self.endPoint.transform.position;
					Player.Self.targetTr = GameController.Self.endPoint.transform; 
					//elementi.scacchiera [xPosition, yPosition].status = 0;
					Player.Self.xOld = (int)GameController.Self.endPoint.transform.position.x;
					Player.Self.xOld = (int)GameController.Self.endPoint.transform.position.y;
					Player.Self.xPosition = (int)GameController.Self.endPoint.transform.position.x;
					Player.Self.yPosition = (int)GameController.Self.endPoint.transform.position.y;
					Player.Self.elementi.scacchiera [Player.Self.xPosition, Player.Self.yPosition].status = 4;
				} else {
					Debug.Log ("DefaultPosition");
				}

				camera.GetComponent<CameraFollow> ().SetCameraPosition ();
				GameController.Self.PassTurn ();
				Player.Self.ResetPlayerVar ();
			} else {
				Debug.Log ("Non basta il mana per l'attivazione");
				//Inserire feedback

			}
		}


		//Night Fever Ability
		public void Fever()
		{
			if (Player.Self.currentMana >= manaCostFever) {
				Player.Self.currentMana -= manaCostFever;
				if (Player.Self.currentMana < 0) {
					Player.Self.currentMana = 0;
				}
				Debug.Log ("Fever ability activated");
				Player.Self.FeverAttack ();
				GameController.Self.PassTurn ();
				Player.Self.ResetPlayerVar ();
			} else {
				Debug.Log ("Non basta il mana per l'attivazione");
				//Inserire feedback

			}

		}


	}
}