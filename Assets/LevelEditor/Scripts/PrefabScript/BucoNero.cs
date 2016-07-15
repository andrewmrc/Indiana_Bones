using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class BucoNero : MonoBehaviour
    {
        int x;
        int y;

        void Start()
        {
            Grid elementi = FindObjectOfType<Grid>();
            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;
            elementi.scacchiera[x, y].status = -1;
            elementi.scacchiera[x, y].name = "buconero";
            SpriteRenderer nero = elementi.scacchiera[x, y].GetComponent<SpriteRenderer>();
            nero.sprite = Resources.Load("nera", typeof(Sprite)) as Sprite;
            Destroy(this.gameObject);
            


        }

    }
}
