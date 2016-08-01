using UnityEngine;
using System.Collections;

namespace IndianaBones
{

	public class HoleTraps : MonoBehaviour {

		int x;
		int y;
		private Grid myGrid;
		public bool trapDirectionRight;
		public bool trapDirectionLeft;

		public int DannoTrappola = 1;

		// Use this for initialization
		void Start () {
			x = (int)this.transform.position.x;
			y = (int)this.transform.position.y;

			myGrid = FindObjectOfType<Grid>();
			this.transform.position =  myGrid.scacchiera[x, y].transform.position;
		}


		// Update is called once per frame
		void Update () {
			if (trapDirectionRight) {
				if (Player.Self.transform.position == myGrid.scacchiera[x+1,y].transform.position) {
					Player.Self.gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 0, 0, 255);
					StartCoroutine (ResetPlayerColor ());

					Player.Self.currentLife -= DannoTrappola;

					Player.Self.transform.position = myGrid.scacchiera [Player.Self.xOld, Player.Self.yOld].transform.position;

					Player.Self.targetTr = myGrid.scacchiera [Player.Self.xOld, Player.Self.yOld].transform;

					Player.Self.xPosition = Player.Self.xOld;
					Player.Self.yPosition = Player.Self.yOld;

					myGrid.scacchiera[Player.Self.xPosition, Player.Self.yPosition].status = 4;
					myGrid.scacchiera[x + 1, y].status = 2;

					SpriteRenderer buco = myGrid.scacchiera[x+1, y].GetComponent<SpriteRenderer>();
					buco.sprite = Resources.Load("buco", typeof(Sprite)) as Sprite;

				} 
			} else if (trapDirectionLeft) {
				if (Player.Self.transform.position == myGrid.scacchiera[x-1,y].transform.position) {
					Player.Self.gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 0, 0, 255);
					StartCoroutine (ResetPlayerColor ());

					Player.Self.currentLife -= DannoTrappola;

					Player.Self.transform.position = myGrid.scacchiera [Player.Self.xOld, Player.Self.yOld].transform.position;

					Player.Self.targetTr = myGrid.scacchiera [Player.Self.xOld, Player.Self.yOld].transform;

					Player.Self.xPosition = Player.Self.xOld;
					Player.Self.yPosition = Player.Self.yOld;

					myGrid.scacchiera[Player.Self.xPosition, Player.Self.yPosition].status = 4;
					myGrid.scacchiera[x - 1, y].status = 2;

					SpriteRenderer buco = myGrid.scacchiera[x-1, y].GetComponent<SpriteRenderer>();
					buco.sprite = Resources.Load("buco", typeof(Sprite)) as Sprite;

				} 

			}
		}


		IEnumerator ResetPlayerColor()
		{
			yield return new WaitForSeconds(0.3f);
			Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

		}

	}
}