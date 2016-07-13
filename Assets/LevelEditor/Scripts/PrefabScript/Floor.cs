using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Floor : MonoBehaviour
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
			Debug.Log ("Tile Name: " + tileName);
            SpriteRenderer muro = myGrid.scacchiera[x, y].GetComponent<SpriteRenderer>();
			muro.sprite = Resources.Load(tileName, typeof(Sprite)) as Sprite;
            this.transform.position = new Vector2(x, y);
			myGrid.scacchiera[x, y].name = tileName;
            Destroy(this.gameObject);

        }

    }
}
