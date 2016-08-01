using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace IndianaBones
{
	public class GameOverButtons : MonoBehaviour {

		//public GameObject eventSystem;
		//public GameObject canvasUI;
		bool pressed;

		void Awake (){
			//canvasUI = GameObject.FindGameObjectWithTag ("CanvasUI");
			//eventSystem = GameObject.Find ("EventSystem");

		}

		// Use this for initialization
		void Start () {
			//eventSystem.GetComponent<EventSystem> ().firstSelectedGameObject = this.transform.GetChild (1).gameObject;
			//this.gameObject.transform.parent.gameObject.SetActive (false);
			//this.gameObject.SetActive (false);

		}
		
		public void ExitToMenu()
		{
			if (!pressed) {
				pressed = true;
				Debug.Log ("TORNA AL MENU");
				ElementsReference.Self.canvasUI.SetActive (true);
				ElementsReference.Self.canvasUI.GetComponent<CanvasGroup> ().alpha = 0;
				SceneManager.LoadScene ("Menu");
			}
		}

		public void reloadScene ()
		{
			if (!pressed) {
				pressed = true;
				Debug.Log ("CONTINUA PARTITA");
				Scene thisScene = SceneManager.GetActiveScene ();
				Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn = true;
				SceneManager.LoadScene (thisScene.name);
			}
		}
	}
}