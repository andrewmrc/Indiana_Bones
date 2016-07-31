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

					Player.Self.currentLife += healthQuantity;
					Player.Self.currentMana -= manaCostRestore;
					if (Player.Self.currentLife > Player.Self.startingLife) {
						Player.Self.currentLife = Player.Self.startingLife;
					}

					if (Player.Self.currentMana < 0) {
						Player.Self.currentMana = 0;
					}
				} else {
					Debug.Log ("La vita è già al massimo!");
				}
			} else {
				Debug.Log ("Non basta il mana per l'attivazione");
			}
		}


		//Escape Rope Ability
		public void EscapeRope()
		{
			if (Player.Self.currentMana >= manaCostEscape) {
				Debug.Log ("Escape Rope ability activated");

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

			} else {
				Debug.Log ("Non basta il mana per l'attivazione");
			}
		}


		//Night Fever Ability
		public void Fever()
		{
			if (Player.Self.currentMana >= manaCostFever) {
				Debug.Log ("Fever ability activated");

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

			} else {
				Debug.Log ("Non basta il mana per l'attivazione");
			}

		}


	}
}