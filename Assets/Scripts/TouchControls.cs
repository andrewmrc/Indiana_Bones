using UnityEngine;
using System.Collections;

namespace IndianaBones
{
	public class TouchControls : MonoBehaviour {

		public bool moveUp;
		public bool moveDown;
		public bool moveRight;
		public bool moveLeft;

		void OnLevelWasLoaded(){
			StopMove ();
			Debug.Log ("STOP MOVE");
		}

		// Use this for initialization
		void Awake () {
			//StopMove ();
		}
		
		// Update is called once per frame
		void Update () {
			//Gestisce turno player
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				if (moveUp) {
					Player.Self.MovePlayerUp ();
				}
				if (moveDown) {
					Player.Self.MovePlayerDown ();
				}
				if (moveRight) {
					Player.Self.MovePlayerRight ();
				}
				if (moveLeft) {
					Player.Self.MovePlayerLeft ();
				}
			} 

			if(Player.Self.isAttacking || !Player.Self.GetComponent<Player>().enabled) {
				StopMove ();
			}
		}


		public void ActivatePassTurnAttack () {
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				Player.Self.PassTurnAttack ();
			}
		}


		public void SelectionCross () {
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				Player.Self.CheckForCross ();
			}
		}


		public void ActivateDistAttack () {
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				Player.Self.AttackFromDistance ();
			}
		}


		public void ActivateAttackUp () {
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				Player.Self.AttackUp ();
			}
		}


		public void ActivateAttackDown () {
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				Player.Self.AttackDown ();
			}
		}


		public void ActivateAttackRight () {
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				Player.Self.AttackRight ();
			}
		}


		public void ActivateAttackLeft () {
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				Player.Self.AttackLeft ();
			}
		}


		public void MoveUp () {
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				moveUp = true;
			}
		}

		public void MoveDown () {
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				moveDown = true;
			}
		}

		public void MoveRight () {
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				moveRight = true;
			}
		}

		public void MoveLeft () {
			if (Player.Self.currentLife > 0 && Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn && Player.Self.isAttacking == false) {
				moveLeft = true;
			}
		}

		public void StopMove () {
			moveUp = false;
			moveDown = false;
			moveRight = false;
			moveLeft = false;
		}

	}
}
