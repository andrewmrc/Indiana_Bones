using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class MuroOrizzontale : MonoBehaviour
    {
        int x;
        int y;
        
        void Start()
        {
            Grid elementi = FindObjectOfType<Grid>();
            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;
            elementi.scacchiera[x, y].status = 2;
            SpriteRenderer muro = elementi.scacchiera[x, y].GetComponent<SpriteRenderer>();
            muro.sprite = Resources.Load("muroOrizzontale", typeof(Sprite)) as Sprite;
            this.transform.position = new Vector2(x, y);
            elementi.scacchiera[x, y].name = "Muro_Orizzontale";
            elementi.scacchiera[x, y].gameObject.AddComponent<BoxCollider2D>();
            Destroy(this.gameObject);

        }

    }
}
