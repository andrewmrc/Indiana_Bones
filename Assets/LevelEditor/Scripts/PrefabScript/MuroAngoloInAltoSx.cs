using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class MuroAngoloInAltoSx : MonoBehaviour
    {
        int x;
        int y;
        
        void Start()
        {
            Griglia elementi = FindObjectOfType<Griglia>();
            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;
            elementi.scacchiera[x, y].status = 2;
            SpriteRenderer muro = elementi.scacchiera[x, y].GetComponent<SpriteRenderer>();
            muro.sprite = Resources.Load("muroAngoloInAltoSx", typeof(Sprite)) as Sprite;
            this.transform.position = new Vector2(x, y);
            elementi.scacchiera[x, y].name = "muro";
            elementi.scacchiera[x, y].gameObject.AddComponent<BoxCollider2D>();
            Destroy(this.gameObject);

        }

    }
}
