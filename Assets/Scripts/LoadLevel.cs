using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace IndianaBones
{

	public class LoadLevel : MonoBehaviour {

		public string levelToLoad;
		Grid myGrid;
		int xPosition;
		int yPosition;

		// Use this for initialization
		void Start () {
			myGrid = FindObjectOfType<Grid>();
			xPosition = (int)this.transform.position.x;
			yPosition = (int)this.transform.position.y;	
		}
		
		// Update is called once per frame
		void Update () {

			if (Player.Self.transform.position == myGrid.scacchiera [xPosition, yPosition].transform.position) {
				Player.Self.gameObject.GetComponent<TurnHandler> ().itsMyTurn = true;
				SceneManager.LoadScene (levelToLoad);
			}

		}


	}

}