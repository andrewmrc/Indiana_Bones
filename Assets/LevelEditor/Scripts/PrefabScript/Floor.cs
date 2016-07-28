using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Floor : MonoBehaviour
    {
        int x;
        int y;
        int z;
        string tileName;

        void Start()
        {
            Grid myGrid = FindObjectOfType<Grid>();
            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;
            z = (int)this.transform.rotation.eulerAngles.z;
            Debug.Log("la z di " + this.gameObject.name + " è: " +z);
			tileName = this.gameObject.GetComponent<SpriteRenderer>().sprite.name;
			Debug.Log ("Tile Name: " + tileName);
            SpriteRenderer pavimento = myGrid.scacchiera[x, y].GetComponent<SpriteRenderer>();
			pavimento.sprite = Resources.Load(tileName, typeof(Sprite)) as Sprite;
            this.transform.position = new Vector2(x, y);
            myGrid.scacchiera[x, y].transform.rotation = Quaternion.Euler(0, 0, z);
            myGrid.scacchiera[x, y].name = tileName;
            Destroy(this.gameObject);

        }

    }
}
