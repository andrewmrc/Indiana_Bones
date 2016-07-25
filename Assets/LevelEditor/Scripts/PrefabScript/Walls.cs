using UnityEngine;
using System.Collections;

namespace IndianaBones
{

	public class Walls : MonoBehaviour
	{
		int x;
		int y;
		string tileName;

		void Start()
		{
			Grid myGrid = FindObjectOfType<Grid>();
			x = (int)this.transform.position.x;
			y = (int)this.transform.position.y;
			tileName = this.gameObject.GetComponent<SpriteRenderer>().sprite.name;
			myGrid.scacchiera[x, y].status = 2;
			myGrid.scacchiera[x, y].gameObject.AddComponent<BoxCollider2D>();

			Debug.Log ("Tile Name: " + tileName);
			SpriteRenderer wall = myGrid.scacchiera[x, y].GetComponent<SpriteRenderer>();
			wall.sprite = gameObject.GetComponent<SpriteRenderer> ().sprite;
			this.transform.position = new Vector2(x, y);
			myGrid.scacchiera[x, y].name = tileName;
			myGrid.scacchiera [x, y].tag = "Walls";
			Destroy(this.gameObject);

		}

	}
}
