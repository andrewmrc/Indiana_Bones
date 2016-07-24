using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace IndianaBones
{
	public class SpikesTrap : MonoBehaviour {

		public int xPosition;
		public int yPosition;

		private Grid elementi;

		public int trapDamage = 1;

		public int nTrapTurn = 1;
		public int nTrapTurnStay = 1;

		public int playerTurnsCount;

		public bool counted;
		public bool itsDangerous;


		void Start()
		{
			elementi = FindObjectOfType<Grid>();

			xPosition = (int)this.transform.position.x;
			yPosition = (int)this.transform.position.y;

			this.transform.position =  elementi.scacchiera[xPosition, yPosition].transform.position;


		}


		public void AttackHandler()
		{
			//Formula calcolo attacco 
			//il risultato si sottrae alla vita del player


			Player.Self.currentLife -= trapDamage;

			Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
			StartCoroutine(ResetPlayerColor());
			//GameController.Self.PassTurn();

		}


		IEnumerator ResetPlayerColor()
		{
			yield return new WaitForSeconds(0.3f);
			Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
		}
			

		void Update()
		{
			if (Player.Self.GetComponent<TurnHandler> ().itsMyTurn == true && counted == false) {
				counted = true;
				playerTurnsCount++;
			} 

			if (Player.Self.GetComponent<TurnHandler> ().itsMyTurn == false) {
				counted = false;
			}


			if (playerTurnsCount == nTrapTurn) {
				itsDangerous = true;
				SpriteRenderer spuntoni = GetComponent<SpriteRenderer>();
				spuntoni.sprite = Resources.Load("Spuntoni_on", typeof(Sprite)) as Sprite;

			} else if (playerTurnsCount == nTrapTurn + nTrapTurnStay) {
				StartCoroutine (DeactivateTrap());

			}


			if (Player.Self.transform.position == elementi.scacchiera[xPosition, yPosition].transform.position && itsDangerous)
			{
				playerTurnsCount += nTrapTurnStay;
				itsDangerous = false;
				AttackHandler();
			}

		}


		IEnumerator DeactivateTrap (){
			yield return new WaitForSeconds (0.3f);
			SpriteRenderer spuntoni = GetComponent<SpriteRenderer>();
			spuntoni.sprite = Resources.Load("Spuntoni_off", typeof(Sprite)) as Sprite;
			itsDangerous = false;
			playerTurnsCount = 0;
		}


	}

}